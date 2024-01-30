using Infrastructures;
using WebAPI.Middlewares;
using WebAPI;
using Application.Commons;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using static Org.BouncyCastle.Math.EC.ECCurve;

var builder = WebApplication.CreateBuilder(args);

// parse the configuration in appsettings
var configuration = builder.Configuration.Get<AppConfiguration>() ?? new AppConfiguration();
builder.Services.AddInfrastructuresService(configuration.DatabaseConnection);
builder.Services.AddWebAPIService();
builder.Services.AddSingleton(configuration);


// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration.JWTSection.Issuer,
            ValidAudience = configuration.JWTSection.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JWTSection.SecretKey)),
        };
    });

builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

/*
    register with singleton life time
    now we can use dependency injection for AppConfiguratio
*/
builder.Services.AddSingleton(configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();
app.UseMiddleware<ConfirmationTokenMiddleware>();
app.MapHealthChecks("/healthchecks");
app.UseHttpsRedirection();

// todo authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// this line tell intergrasion test
// https://stackoverflow.com/questions/69991983/deps-file-missing-for-dotnet-6-integration-tests
public partial class Program { }