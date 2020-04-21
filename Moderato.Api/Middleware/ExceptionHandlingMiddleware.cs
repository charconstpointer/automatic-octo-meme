using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using GitHub.Exceptions;
using Microsoft.AspNetCore.Http;
using Moderato.Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace Moderato.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = exception switch
            {
                UserNotFoundException _ => (int) HttpStatusCode.NotFound,
                UserNotAuthorizedException _ => (int) HttpStatusCode.Unauthorized,
                InsufficientRepositoryCount _ => (int) HttpStatusCode.BadRequest,
                InfrastructureException _ => (int) HttpStatusCode.InternalServerError,
                ValidationException _ => (int) HttpStatusCode.BadRequest,
                _ => 500
            };

            context.Response.ContentType = "application/json";

            var response = new {exception.Message};
            var json = JsonConvert.SerializeObject(response);
            return context.Response.WriteAsync(json);
        }
    }
}