global using FastEndpoints;
global using IResult = LEFiles.Core.Models.Results.Abstract.IResult;
using Global.CoreProject.Middlewares;
using LEFiles.DataAccess;
using LEFiles.Models.Configuration;
using LEFiles.Services.Contracts.Authentication;
using LEFiles.Services.Contracts.Clients;
using LEFiles.Services.Service.Authentication;
using LEFiles.Services.Service.Clients;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Cryptography;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
.AddEnvironmentVariables()
.Build();
var Configuration = builder.Configuration;
// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
  option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
  option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please enter a valid token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "Bearer"
  });
  option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddSignalR();
var addAuth =builder.Services.AddAuthentication();
builder.Services.AddTransient<AppDbContext>();
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();
builder.Services.AddTransient<IAuthenticationService, BasicAuthenticationService>();
builder.Services.AddTransient<IClientService, ClientService>();
List<JWTConfig> jwts = new List<JWTConfig>();
Configuration.GetSection("JwtConfiguration").Bind(jwts);
builder.Services.AddSingleton<List<JWTConfig>>(jwts);
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: "AllowedCorsOrigins",
      builder =>
      {
        builder
                      .SetIsOriginAllowed(origin => true)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
      });
});
for (var i=0;i<jwts.Count;i++){
  var tokenInfo = jwts[i];
  RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
  rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(tokenInfo.PublicKey), out _);
  JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
  addAuth.AddJwtBearer(tokenInfo.TokenName, (bearer) => {
    bearer.RequireHttpsMetadata = false;
    bearer.SaveToken = true;

    bearer.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = false,
      ValidateAudience = false,
      ValidateLifetime = false,
      ValidateIssuerSigningKey = true,
      ValidIssuer = tokenInfo.Issuer,
      IssuerSigningKey = new RsaSecurityKey(rsa),
      ClockSkew = TimeSpan.FromSeconds(60)
    };
    bearer.Events = new JwtBearerEvents
    {
      OnMessageReceived = context => {
        var accessToken = context.Request.Query["access_token"];
        var path = context.HttpContext.Request.Path;
        if(!string.IsNullOrEmpty(accessToken)){
          if(path.StartsWithSegments("/hub")){
            context.Token = accessToken;
          }
        }
        return Task.CompletedTask;
      }
    };
  });
}
var schemes = jwts.Select(a => new string(a.TokenName)).ToArray();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services
    .AddAuthorization(options =>
    {
      options.DefaultPolicy = new AuthorizationPolicyBuilder()
          .RequireAuthenticatedUser()
          .AddAuthenticationSchemes(schemes)
          .Build();
    });
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseCors("AllowedCorsOrigins");
app.UseFastEndpoints();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
//app.MapControllers();

app.Run();
