﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using AaltoGlobalImpact.OIP;
using JsonFx.Json;
using JsonFx.Serialization;
using Microsoft.WindowsAzure.StorageClient;
using TheBall.Index;

namespace AzureSupport
{
    public static class TypeSupport
    {
        public static Type GetTypeByName(string fullName)
        {
            // TODO: Reflect proper loading based on fulltype, right now fetching from this
            Assembly currAsm = Assembly.GetExecutingAssembly();
            Type type = currAsm.GetType(fullName);
            return type;
        }
    }

    public static class JSONSupport
    {
        public static ExpandoObject GetJsonFromStream(TextReader input)
        {
            var reader = new JsonReader();
            dynamic jsonObject = reader.Read(input);
            return jsonObject;
        }

        public static ExpandoObject GetJsonFromStream(string input)
        {
            var reader = new JsonReader();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var strongType = reader.Read<NodeSummaryContainer>(input);
            watch.Stop();
            var elapsed1 = watch.ElapsedMilliseconds;
            watch.Restart();
            dynamic jsonObject = reader.Read(input);
            watch.Stop();
            var elapsed2 = watch.ElapsedMilliseconds;
            return jsonObject;
        }

        public static T GetObjectFromData<T>(byte[] data)
        {
            using (var memStream = new MemoryStream(data))
            {
                return GetObjectFromStream<T>(memStream);
            }
        }

        public static T GetObjectFromStream<T>(Stream stream)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

            T result = (T) serializer.ReadObject(stream);
            watch.Stop();
            var elapsed = watch.ElapsedMilliseconds;
            return result;
        }

        public static void SerializeToJSONStream(object obj, Stream outputStream)
        {
            var writer = new JsonWriter();
            using (StreamWriter textWriter = new StreamWriter(outputStream))
            {
                writer.Write(obj, textWriter);
            }
        }

        public static string SerializeToJSONString(object obj)
        {
            var writer = new JsonWriter();
            return writer.Write(obj);
        }

        public static T GetObjectFromString<T>(string jsonString)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var reader = new JsonReader();
            var result = reader.Read<T>(jsonString);
            watch.Stop();
            var elapsed = watch.ElapsedMilliseconds;
            return result;
        }
    }
}
