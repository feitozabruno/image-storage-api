using ImageStorage.Api.DTOs;
using ImageStorage.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ImageStorage.Api.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ImagesController(ImagesService imagesService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Upload(CreateImageDto dto)
    {
        if (dto.File == null || dto.File.Length == 0) return BadRequest("Nenhum arquivo enviado");
        var response = await imagesService.UploadAsync(dto);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteImageDto dto)
    {
        await imagesService.DeleteAsync(dto.BlobUrl);
        return NoContent();
    }
}
