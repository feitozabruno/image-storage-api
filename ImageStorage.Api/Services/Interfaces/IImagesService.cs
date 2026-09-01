using System.Text.Json;
using ImageStorage.Api.DTOs;

namespace ImageStorage.Api.Services.Interfaces;

public interface IImagesService
{
    Task<JsonElement> UploadAsync(CreateImageDto dto);
    Task DeleteAsync(string blobUrl);
}