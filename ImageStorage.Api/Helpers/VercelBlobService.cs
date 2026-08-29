using System.Net.Http.Headers;
using System.Text.Json;

namespace ImageStorage.Api.Helpers;

public class VercelBlobService(HttpClient httpClient, ImageOptimizerService imageOptimizerService)
{
    public async Task<JsonElement> UploadAsync(IFormFile file)
    {
        var outputStream = await imageOptimizerService.Process(file);

        long timestampMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var fileName = $"img-{timestampMs}.webp";
        var pathname = $"images/{fileName}";

        var request = new HttpRequestMessage(HttpMethod.Put,
            $"https://blob.vercel-storage.com/{pathname}");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "");

        request.Content = new ByteArrayContent(outputStream.ToArray());
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

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "");

        request.Content = JsonContent.Create(new { urls = new[] { blobUrl } });

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}