using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/quality", () =>
{
    var rnd = new Random();
    return Results.Ok(new
    {
        temperature = rnd.Next(16, 22),
        humidity = rnd.Next(40, 60),
        quality = "Boa"
    });
});

app.Run();
