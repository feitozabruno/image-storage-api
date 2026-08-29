using ImageStorage.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ImageOptimizerService>();
builder.Services.AddHttpClient<VercelBlobService>();
builder.Services.AddScoped<ImagesService>();

var app = builder.Build();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();