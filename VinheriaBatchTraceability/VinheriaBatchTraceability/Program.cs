using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var batches = new List<object>();

app.MapPost("/batch", (string id, string origin) =>
{
    batches.Add(new { id, origin, created = DateTime.UtcNow });
    return Results.Ok("Lote registrado");
});

app.MapGet("/batch", () => batches);

app.Run();
