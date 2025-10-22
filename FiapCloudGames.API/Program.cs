using System.Text;
using FiapCloudGames.API.Services;
using FiapGames.Core.Helper;
using FiapGames.Infra.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDataContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
    if (connectionString != null)
    {
        options.UseSqlServer(connectionString);
    }
    else
    {
        throw new InvalidOperationException("The SQL Server connection string is not configured.");
    }
}, ServiceLifetime.Scoped);

// Injecao de dependenciasbuilder.Services.AddDbContext<AppContext>(options =>

builder.Services.ApplicationServices();


var jwtSection = builder.Configuration.GetSection("Jwt");
var secret = JwtAuthenticationGenerate.GenerateSecret(32);

if (string.IsNullOrEmpty(secret))
{
    throw new InvalidOperationException("Jwt:Secret não configurado.");
}
if (builder.Environment.IsDevelopment())
{
    jwtSection = builder.Configuration.GetSection("Jwt");
    if (string.IsNullOrEmpty(jwtSection["Secret"]))
    {
        secret = JwtAuthenticationGenerate.GenerateSecret(32);
        Console.WriteLine("JWT secret gerado (copie e persista com dotnet user-secrets ou variável de ambiente):");
        Console.WriteLine(secret);
    }
}

var key = Encoding.UTF8.GetBytes(secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = builder.Environment.IsProduction();
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = !string.IsNullOrEmpty(jwtSection["Issuer"]),
        ValidIssuer = jwtSection["Issuer"],
        ValidateAudience = !string.IsNullOrEmpty(jwtSection["Audience"]),
        ValidAudience = jwtSection["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1)
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
