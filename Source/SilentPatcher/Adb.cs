using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnityPatcher
{
    internal class Adb
    {
        public static List<string> GetPackageList()
        {
            string result = CmdUtility.DoCmdResult("/c adb shell pm list packages");
            string[] blacklist = { "oculus", "meta", "google", "android", "samsung" };
            return (from line in result.Split('\n')
                    where line.Contains("package:")
                    select line.Replace("package:", "").Trim() into packageName
                    where !blacklist.Any(b => packageName.Contains(b))
                    select packageName).ToList();
        }

        public static string GetBaseApk(string packageName)
        {
            string result = CmdUtility.DoCmdResult("/c adb shell pm path " + packageName);
            return result.Split('\n').FirstOrDefault(line => line.Trim().EndsWith("/base.apk"))?.Replace("package:", "").Trim();
        }

        public static void PullFile(string remotePath, string outputFilePath = null)
        {
            string destination = outputFilePath ?? Path.Combine(AppContext.BaseDirectory, "TOPATCH.apk");
            if (File.Exists(destination)) File.Delete(destination);
            CmdUtility.DoCmd($"/c adb pull {remotePath} \"{destination}\"");
        }

        public static List<string> GetInstalledMods(string packageName)
        {
            string remotePath = $"/sdcard/Android/data/{packageName.Trim()}/nativemods/";
            string result = CmdUtility.DoCmdResult($"/c adb shell ls \"{remotePath}\"");
            var mods = new List<string>();
            foreach (string line in result.Split('\n'))
            {
                string trimmed = line.Trim();
                if (!string.IsNullOrWhiteSpace(trimmed) && trimmed.EndsWith(".so"))
                    mods.Add(trimmed);
            }
            return mods;
        }

        public static void PushMod(string localFilePath, string packageName)
        {
            string fileName = Path.GetFileName(localFilePath);
            string remotePath = $"/sdcard/Android/data/{packageName.Trim()}/nativemods/{fileName}";
            CmdUtility.DoCmd($"/c adb push \"{localFilePath}\" \"{remotePath}\"");
        }

        public static void RemoveMod(string modFileName, string packageName)
        {
            string remotePath = $"/sdcard/Android/data/{packageName.Trim()}/nativemods/{modFileName}";
            CmdUtility.DoCmd($"/c adb shell rm \"{remotePath}\"");
        }

        public static void CreateNativeModsFolder(string packageName)
        {
            string remotePath = $"/sdcard/Android/data/{packageName.Trim()}/nativemods";
            CmdUtility.DoCmd($"/c adb shell mkdir -p \"{remotePath}\"");
        }
    }
}
