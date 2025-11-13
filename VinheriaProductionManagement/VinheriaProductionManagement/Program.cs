using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var productions = new List<object>();

app.MapPost("/harvest", (string type, int quantity) =>
{
    productions.Add(new { type, quantity, date = DateTime.Now });
    return Results.Ok("Colheita registrada");
});

app.MapGet("/harvest", () => productions);

app.Run();
