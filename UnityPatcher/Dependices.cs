using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnityPatcher
{
    internal class Dependices
    {
        public async static void apktool()
        {
            string zzzzzzzzzzzzz = "https://api.github.com/repos/iBotPeaches/Apktool/releases/latest";
            string apktooljar = Path.Combine(Directory.GetCurrentDirectory(), "apktool.jar");

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("request");
            string json = await client.GetStringAsync(zzzzzzzzzzzzz);
            using JsonDocument doc = JsonDocument.Parse(json);
            string url = null;

            foreach (var asset in doc.RootElement.GetProperty("assets").EnumerateArray())
            {
                string name = asset.GetProperty("name").GetString();
                if (name.EndsWith(".jar"))
                {
                    url = asset.GetProperty("browser_download_url").GetString();
                    break;
                }
            }
            byte[] data = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(apktooljar, data);
        }
        public async static void uberapksigner()
        {
            string zzzzzzzzzzzzz = "https://api.github.com/repos/patrickfav/uber-apk-signer/releases/latest";
            string uberapksignerjar = Path.Combine(Directory.GetCurrentDirectory(), "uber-apk-signer.jar");

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("request");
            string json = await client.GetStringAsync(zzzzzzzzzzzzz);
            using JsonDocument doc = JsonDocument.Parse(json);
            string url = null;

            foreach (var asset in doc.RootElement.GetProperty("assets").EnumerateArray())
            {
                string name = asset.GetProperty("name").GetString();
                if (name.EndsWith(".jar"))
                {
                    url = asset.GetProperty("browser_download_url").GetString();
                    break;
                }
            }
            byte[] data = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(uberapksignerjar, data);
        }
    }
}
