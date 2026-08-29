using System.Text.Json;
using ImageStorage.Api.DTOs;

namespace ImageStorage.Api.Helpers;

public class ImagesService(ImageOptimizerService imageOptimizerService, VercelBlobService vercelBlobService)
{
    public async Task<JsonElement> UploadAsync(CreateImageDto dto)
    {
        var fileName = $"images/img-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}.webp";
        byte[] optimizedImage = await imageOptimizerService.Process(dto);
        return await vercelBlobService.UploadAsync(fileName, optimizedImage);
    }

    public async Task DeleteAsync(string blobUrl)
    {
        await vercelBlobService.DeleteAsync(blobUrl);
    }
}