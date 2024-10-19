
using FluentValidation;
using LEFiles.Core.Models.Results.Concrete;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Global.CoreProject.Middlewares
{

  public class GlobalExceptionHandlerMiddleware : IMiddleware
  {

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      try
      {
        await next(context);
      }
      catch (Exception exception)
      {
        await HandleExceptionAsync(context, exception);
      }
    }
    public async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      context.Response.ContentType = "application/json";
      var response = context.Response;
      var statusCode = HttpStatusCode.InternalServerError;
      LEFiles.Core.Models.Results.Abstract.IResult result;
      switch (exception)
      {
        case HttpRequestException ex:
          result = new ErrorResult(ex.Message);
          statusCode = ex.StatusCode ?? HttpStatusCode.InternalServerError;
          break;
        case ValidationException ex:
          if (!response.HasStarted)
          {
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;
            await response.WriteAsync(JsonSerializer.Serialize(ex, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));

          }
          return;
        case Exception ex:
          result = new ErrorResult(ex.Message == "" ? "An Error Occurred" : ex.Message);
          break;

        default:
          result = new ErrorResult("An Error Occurred");
          break;
      }
      if (!response.HasStarted)
      {
        response.ContentType = "application/json";
        response.StatusCode = (int)statusCode;
        await response.WriteAsync(JsonSerializer.Serialize(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
      }

    }

  }
}
