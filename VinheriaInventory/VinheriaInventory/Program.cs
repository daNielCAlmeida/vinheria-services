using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var stock = new Dictionary<string, int>();

app.MapPost("/add", (string item, int quantity) =>
{
    if (stock.ContainsKey(item))
        stock[item] += quantity;
    else
        stock[item] = quantity;

    return Results.Ok($"{quantity} unidades de {item} adicionadas.");
});

app.MapPost("/remove", (string item, int quantity) =>
{
    if (!stock.ContainsKey(item) || stock[item] < quantity)
        return Results.BadRequest("Quantidade insuficiente ou item inexistente.");

    stock[item] -= quantity;
    return Results.Ok($"{quantity} unidades de {item} removidas.");
});

app.MapGet("/inventory", () => stock);

app.Run();
