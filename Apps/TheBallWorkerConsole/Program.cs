﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace TheBall.CORE.TheBallWorkerConsole
{
    class Program
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        static void Main(string[] args)
        {
            try
            {
                AsyncContext.Run(() => MainAsync(args));
            }
            catch (Exception exception)
            {
                var errorFile = Path.Combine(AssemblyDirectory, "ConsoleErrorLog.txt");
                File.WriteAllText(errorFile, exception.ToString());
                throw;
            }
        }

        static async void MainAsync(string[] args)
        {
            var configFullPath = args.Length > 0 ? args[0] : null;
            if (configFullPath == null)
                throw new ArgumentNullException(nameof(args), "Config full path cannot be null (first  argument)");

            var clientHandle = args.Length > 1 ? args[1] : null;

            var workerConfig = await WorkerConsoleConfig.GetConfig(configFullPath);

            var pipeStream = clientHandle != null
                ? new AnonymousPipeClientStream(PipeDirection.In, clientHandle)
                : null;
            var reader = pipeStream != null ? new StreamReader(pipeStream) : null;
            try
            {
                const int ConcurrentTasks = 3;
                Task[] activeTasks = new Task[ConcurrentTasks];
                int nextFreeIX = 0;

                string startupLogPath = Path.Combine(AssemblyDirectory, "ConsoleStartupLog.txt");
                var startupMessage = "Starting up process (UTC): " + DateTime.UtcNow.ToString();
                File.WriteAllText(startupLogPath, startupMessage);

                var pipeMessageAwaitable = reader != null ? reader.ReadToEndAsync() : null;

                List<Task> awaitList = new List<Task>();
                if(pipeMessageAwaitable != null)
                    awaitList.Add(pipeMessageAwaitable);

                while (true)
                {

                    await Task.WhenAny(awaitList);
                    if (pipeMessageAwaitable != null && pipeMessageAwaitable.IsCompleted)
                    {
                        var pipeMessage = pipeMessageAwaitable.Result;
                        var shutdownLogPath = Path.Combine(AssemblyDirectory, "ConsoleShutdownLog.txt");
                        File.WriteAllText(shutdownLogPath,
                            "Quitting for message (UTC): " + pipeMessage + " " + DateTime.UtcNow.ToString());
                        break;
                    }
                    //await Task.WhenAny(activeTasks);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                    pipeStream.Dispose();
                }
            }
        }
    }
}
