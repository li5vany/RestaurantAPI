using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Restaurant.API.BasicResponses;
using Restaurant.API.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Restaurant.API.Middlewares
{
    public class ErrorWrappingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorWrappingMiddleware> _logger;

        public ErrorWrappingMiddleware(RequestDelegate next, ILogger<ErrorWrappingMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private string Message { get; set; }

        public async Task Invoke(HttpContext context)
        {
            Message = "";
            try
            {
                await _next.Invoke(context);
            }
            catch (NotAllowedException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Message = ex.Message;
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                Message = String.IsNullOrWhiteSpace(ex.Entity) ? ex.Message : ex.Entity + ex.Message;
            }
            catch (InvalidDataException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Message = String.IsNullOrWhiteSpace(ex.Field) ? ex.Message : ex.Field + ex.Message;
            }
            catch (AlreadyExistsException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Message = ex.Message;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                Message = ex.Message;
            }

            if (!context.Response.HasStarted && context.Response.StatusCode != 204)
            {
                context.Response.ContentType = "application/json";

                var response = new ApiResponse(context.Response.StatusCode, Message ?? "");

                var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                await context.Response.WriteAsync(json);
            }
        }
    }
}