using System.Text.Json;

namespace ImageStorage.Api.Services.Interfaces;

public interface IVercelBlobService
{
    Task<JsonElement> UploadAsync(string fileName, byte[] content);
    Task DeleteAsync(string blobUrl);
}