using ImageStorage.Api.Services;
using ImageStorage.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IImageOptimizerService, ImageOptimizerService>();
builder.Services.AddHttpClient<IVercelBlobService, VercelBlobService>();
builder.Services.AddScoped<IImagesService, ImagesService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();