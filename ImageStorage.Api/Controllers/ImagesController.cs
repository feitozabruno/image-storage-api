using ImageStorage.Api.Helpers;
using Microsoft.AspNetCore.Mvc;


namespace ImageStorage.Api.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ImagesController(VercelBlobService vercelBlobService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("Nenhum arquivo enviado");
        var response = await vercelBlobService.UploadAsync(file);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteImageDto dto)
    {
        await vercelBlobService.DeleteAsync(dto.BlobUrl);
        return NoContent();
    }
}

public record DeleteImageDto
{
    public string BlobUrl { get; set; } = string.Empty;
}