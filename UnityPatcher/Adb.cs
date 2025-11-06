using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityPatcher;
namespace UnityPatcher
{
    // @ got this adb stuff from pubert original version
    internal class Adb
    {
        public static List<string> GetPackageList()
        {
            string result = CmdUtility.DoCmdResult("/c adb shell pm list packages");
            string[] blacklist = new string[5] { "oculus", "meta", "google", "android", "samsung" };
            return (from x in result.Split('\n')
                    where x.Contains("package:")
                    select x.Replace("package:", "") into x
                    where !blacklist.Any((string b) => x.Contains(b))
                    select x).ToList();
        }
        public static string GetBaseApk(string packageName)
        {
            string prompt = "adb shell pm path " + packageName;
            return CmdUtility.DoCmdResult("/c" + prompt).Split('\n').First((string x) => x.Trim().EndsWith("/base.apk")).Replace("package:", "");
        }
        public static void PullFile(string path, string newLocation = null, string newFileName = null)
        {
            string outputFile = Path.Combine(AppContext.BaseDirectory, "TOPATCH.apk");
            if (newFileName != null)
            {
                outputFile = newFileName;
            }
            if (File.Exists(outputFile)) File.Delete(outputFile);
            string prompt = $"/c adb pull {path} {outputFile}";
            CmdUtility.DoCmd(prompt);
        }
    }
}
