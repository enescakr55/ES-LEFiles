global using FastEndpoints;
using LEFiles.Models.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

List<JWTConfig> jwts = new List<JWTConfig>();
Configuration.GetSection("JwtConfiguration").Bind(jwts);
for(var i=0;i<jwts.Count;i++){
  var tokenInfo = jwts[i];
  RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
  rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(tokenInfo.PublicKey), out _);

  addAuth.AddJwtBearer(tokenInfo.TokenName, (bearer) => {
    bearer.RequireHttpsMetadata = false;
    bearer.SaveToken = true;
    bearer.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = false,
      ValidateLifetime = true,
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
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseFastEndpoints();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//app.MapControllers();

app.Run();
