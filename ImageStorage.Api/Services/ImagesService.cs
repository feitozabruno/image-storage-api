using System.Text.Json;
using ImageStorage.Api.DTOs;
using ImageStorage.Api.Services.Interfaces;

namespace ImageStorage.Api.Services;

public class ImagesService(IImageOptimizerService imageOptimizerService, IVercelBlobService vercelBlobService) : IImagesService
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