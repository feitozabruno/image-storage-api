namespace ImageStorage.Api.DTOs;

public record CreateImageDto
{
    public required IFormFile File { get; set; }
    public int? Quality { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
}

public record DeleteImageDto
{
    public string BlobUrl { get; set; } = string.Empty;
}