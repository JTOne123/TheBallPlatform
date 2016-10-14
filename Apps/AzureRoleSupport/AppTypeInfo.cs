namespace TheBall.Infra.AzureRoleSupport
{
    public class AppTypeInfo
    {
        public readonly string AppType;
        public readonly string AppPackageName;
        public readonly string AppExecutablePath;
        public readonly string AppConfigPath;
        public AppManager CurrentManager;

        public AppTypeInfo(string appType, string appPackageName, string appExecutablePath, string appConfigPath)
        {
            AppType = appType;
            AppPackageName = appPackageName;
            AppExecutablePath = appExecutablePath;
            AppConfigPath = appConfigPath;
        }
    }
}