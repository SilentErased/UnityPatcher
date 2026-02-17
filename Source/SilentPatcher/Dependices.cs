using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnityPatcher
{
    internal class Dependices
    {
        public static async void DownloadApktool()
        {
            string apiUrl = "https://api.github.com/repos/iBotPeaches/Apktool/releases/latest";
            string outputPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "apktool.jar");

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("UnityPatcher");

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

            byte[] data = await client.GetByteArrayAsync(downloadUrl);
            await File.WriteAllBytesAsync(outputPath, data);
        }

        public static async void DownloadUberApkSigner()
        {
            string apiUrl = "https://api.github.com/repos/patrickfav/uber-apk-signer/releases/latest";
            string outputPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "uber-apk-signer.jar");

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("UnityPatcher");

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

            byte[] data = await client.GetByteArrayAsync(downloadUrl);
            await File.WriteAllBytesAsync(outputPath, data);
        }
    }
}
