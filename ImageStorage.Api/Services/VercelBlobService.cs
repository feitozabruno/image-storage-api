using System.Net.Http.Headers;
using System.Text.Json;
using ImageStorage.Api.Services.Interfaces;

namespace ImageStorage.Api.Services;

public class VercelBlobService(HttpClient httpClient, IConfiguration config) : IVercelBlobService
{
    private readonly string _token = config["VERCEL_BLOB_TOKEN:Secret"]!;

    public async Task<JsonElement> UploadAsync(string fileName, byte[] content)
    {
        var request = new HttpRequestMessage(HttpMethod.Put,
            $"https://blob.vercel-storage.com/{fileName}");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        request.Content = new ByteArrayContent(content);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("image/webp");

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        return json;
    }

    public async Task DeleteAsync(string blobUrl)
    {
        var request = new HttpRequestMessage(HttpMethod.Post,
            "https://blob.vercel-storage.com/delete");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        request.Content = JsonContent.Create(new { urls = new[] { blobUrl } });

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}