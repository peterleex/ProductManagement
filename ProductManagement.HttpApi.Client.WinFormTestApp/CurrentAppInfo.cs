using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public class CurrentAppInfo
    {
        public static string FileVer => FileVerInfo.FileVersion!;

        public static FileVersionInfo FileVerInfo
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                return FileVersionInfo.GetVersionInfo(assembly.Location);
            }
        }

        public static int CompareVersion(string appVerOnServer)
        {
            var currentAppVer = new Version(FileVer);
            return currentAppVer.CompareTo(new Version(appVerOnServer));
        }
    }
}
