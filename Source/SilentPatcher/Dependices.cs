using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnityPatcher
{
    internal class Dependices
    {
        public static async void DownloadApktool()
        {
            await DownloadAssetFromGithub("iBotPeaches/Apktool", "apktool.jar");
        }

        public static async void DownloadUberApkSigner()
        {
            await DownloadAssetFromGithub("patrickfav/uber-apk-signer", "uber-apk-signer.jar");
        }

        public static async Task DownloadIl2CppDumper()
        {
            string apiUrl = "https://api.github.com/repos/Perfare/Il2CppDumper/releases/latest";
            string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "dumper.zip");
            string extractPath = Path.Combine(Directory.GetCurrentDirectory(), "Il2CppDumper");

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("UnityPatcher");

            try
            {
                string json = await client.GetStringAsync(apiUrl);
                using JsonDocument doc = JsonDocument.Parse(json);
                string downloadUrl = null;

                foreach (var asset in doc.RootElement.GetProperty("assets").EnumerateArray())
                {
                    string name = asset.GetProperty("name").GetString();
                    if (name.Contains("Il2CppDumper") && name.EndsWith(".zip") && !name.Contains("GUI"))
                    {
                        downloadUrl = asset.GetProperty("browser_download_url").GetString();
                        break;
                    }
                }

                if (downloadUrl != null)
                {
                    byte[] data = await client.GetByteArrayAsync(downloadUrl);
                    await File.WriteAllBytesAsync(zipPath, data);

                    if (Directory.Exists(extractPath)) Directory.Delete(extractPath, true);
                    ZipFile.ExtractToDirectory(zipPath, extractPath);
                    File.Delete(zipPath);
                }
            }
            catch { }
        }

        private static async Task DownloadAssetFromGithub(string repo, string outputName)
        {
            string apiUrl = $"https://api.github.com/repos/{repo}/releases/latest";
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), outputName);

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("UnityPatcher");

            try
            {
                string json = await client.GetStringAsync(apiUrl);
                using JsonDocument doc = JsonDocument.Parse(json);
                string downloadUrl = null;

                foreach (var asset in doc.RootElement.GetProperty("assets").EnumerateArray())
                {
                    string name = asset.GetProperty("name").GetString();
                    if (name.EndsWith(".jar"))
                    {
                        downloadUrl = asset.GetProperty("browser_download_url").GetString();
                        break;
                    }
                }

                if (downloadUrl != null)
                {
                    byte[] data = await client.GetByteArrayAsync(downloadUrl);
                    await File.WriteAllBytesAsync(outputPath, data);
                }
            }
            catch { }
        }
    }
}