using ImageStorage.Api.DTOs;

namespace ImageStorage.Api.Services.Interfaces;

public interface IImageOptimizerService
{
    Task<byte[]> Process(CreateImageDto dto);
}