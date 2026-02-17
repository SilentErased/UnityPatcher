using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnityPatcher
{
    internal class LibraryEntry
    {
        public string Name { get; set; }
        public string DownloadUrl { get; set; }
        public long Size { get; set; }

        public string SizeFormatted => Size < 1024
            ? $"{Size} B"
            : Size < 1024 * 1024
                ? $"{Size / 1024.0:F1} KB"
                : $"{Size / (1024.0 * 1024):F2} MB";
    }

    internal class LibraryManager
    {
        private const string RepoContentsBase = "https://api.github.com/repos/SilentErased/Libraries-Archive/contents";

        private static HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("UnityPatcher");
            return client;
        }

        public static async Task<List<string>> GetUnityVersions()
        {
            using HttpClient client = CreateClient();
            string json = await client.GetStringAsync(RepoContentsBase);
            using JsonDocument doc = JsonDocument.Parse(json);

            var versions = new List<string>();
            foreach (var item in doc.RootElement.EnumerateArray())
            {
                if (item.GetProperty("type").GetString() == "dir")
                    versions.Add(item.GetProperty("name").GetString());
            }
            return versions;
        }

        public static async Task<List<LibraryEntry>> GetLibrariesForVersion(string unityVersion)
        {
            using HttpClient client = CreateClient();
            string json = await client.GetStringAsync($"{RepoContentsBase}/{unityVersion}");
            using JsonDocument doc = JsonDocument.Parse(json);

            var libraries = new List<LibraryEntry>();
            foreach (var item in doc.RootElement.EnumerateArray())
            {
                string type = item.GetProperty("type").GetString();
                string name = item.GetProperty("name").GetString();
                if (type == "file" && name.EndsWith(".so"))
                {
                    libraries.Add(new LibraryEntry
                    {
                        Name = name,
                        DownloadUrl = item.GetProperty("download_url").GetString(),
                        Size = item.TryGetProperty("size", out var sizeElement) ? sizeElement.GetInt64() : 0
                    });
                }
            }
            return libraries;
        }

        public static async Task<byte[]> DownloadLibrary(string downloadUrl)
        {
            using HttpClient client = CreateClient();
            return await client.GetByteArrayAsync(downloadUrl);
        }
    }
}
