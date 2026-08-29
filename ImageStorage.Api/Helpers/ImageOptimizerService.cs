using ImageStorage.Api.DTOs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace ImageStorage.Api.Helpers;

public class ImageOptimizerService()
{
    public async Task<byte[]> Process(CreateImageDto dto)
    {
        Stream stream = dto.File.OpenReadStream();

        Image image = await Image.LoadAsync(stream);

        MemoryStream outputStream = new MemoryStream();

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(dto.Width ?? 800, dto.Height ?? 0),
            Mode = ResizeMode.Max
        }));

        await image.SaveAsync(outputStream, new WebpEncoder { Quality = dto.Quality ?? 80 });

        return outputStream.ToArray();
    }
}

