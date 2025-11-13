using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

const string SECRET = "vinheria_secret_key";

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/token", () =>
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(SECRET);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[] { new Claim("user", "vinheria_admin") }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return Results.Ok(new { access_token = tokenHandler.WriteToken(token), token_type = "bearer" });
});

app.MapGet("/verify", (string token) =>
{
    var handler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(SECRET);

    try
    {
        handler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        }, out SecurityToken validatedToken);

        return Results.Ok(new { status = "valid" });
    }
    catch
    {
        return Results.Unauthorized();
    }
});

app.Run();
