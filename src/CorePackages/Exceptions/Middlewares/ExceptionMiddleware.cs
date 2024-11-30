﻿using Microsoft.AspNetCore.Http;
using Packages.Exceptions.Handlers;
using Packages.Loggings;
using Packages.Loggings.SeriLog;
using System.Text.Json;

namespace Packages.Exceptions.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpExpcetionHandler _httpExceptionHandler;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly LoggerServiceBase _loggerService;

        public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor, LoggerServiceBase loggerService)
        {
            _httpExceptionHandler = new HttpExpcetionHandler();
            _next = next;
            _contextAccessor = contextAccessor;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await LogException(context, exception);
                await HandleExpcetionAsync(context.Response, exception);
            }
        }
        private Task LogException(HttpContext context, Exception exception)
        {
            List<LogParameter> logParameters = new()
        {
            new LogParameter{Type=context.GetType().Name, Value=exception.ToString()}
        };

            LogDetailWithException logDetail = new()
            {
                ExceptionMessage = exception.Message,
                MethodName = _next.Method.Name,
                Parameters = logParameters,
                User = _contextAccessor.HttpContext?.User.Identity?.Name ?? "?"
            };

            _loggerService.Error(JsonSerializer.Serialize(logDetail));

            return Task.CompletedTask;
        }
        private Task HandleExpcetionAsync(HttpResponse response, Exception exception)
        {
            response.ContentType = "application/json";
            _httpExceptionHandler.Response = response;
            return _httpExceptionHandler.HandleExceptionAsync(exception);
        }
    }
}
