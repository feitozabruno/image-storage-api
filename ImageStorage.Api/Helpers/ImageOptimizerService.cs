using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace ImageStorage.Api.Helpers;

public class ImageOptimizerService()
{
    public async Task<MemoryStream> Process(IFormFile file)
    {
        using var stream = file.OpenReadStream();

        using var image = await Image.LoadAsync(stream);

        using var outputStream = new MemoryStream();

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(800, 0),
            Mode = ResizeMode.Max
        }));

        await image.SaveAsync(outputStream, new WebpEncoder { Quality = 80 });

        return outputStream;
    }
}

