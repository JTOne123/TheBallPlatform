using System.Linq;

namespace TheBall.CORE
{
    public static class SystemSupport
    {
        public static readonly string[] ReservedDomainNames = new string[] {"TheBall.CORE", "TheBall.Payments", "TheBall.Interface"};
        public const string SystemOwnerRoot = "sys/AAA";
        public static readonly IContainerOwner SystemOwner;

        static SystemSupport()
        {
            SystemOwner = new VirtualOwner("sys", "AAA");
        }

        public static string[] FilterAwayReservedFolders(string[] directories)
        {
            return directories.Where(dir => ReservedDomainNames.Any(resDom => dir.Contains(resDom) == false)).ToArray();
        }
    }
}