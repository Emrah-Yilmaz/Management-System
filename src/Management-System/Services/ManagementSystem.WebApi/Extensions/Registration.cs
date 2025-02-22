﻿using FluentValidation.AspNetCore;
using ManagementSystem.Application.Features.Queries.WorkTask;
using System.Reflection;

namespace ManagementSystem.WebApi.Extensions
{
    public static class Registration
    {
        public static void AddWebApiRegistration(this IServiceCollection services)
        {
            var assemblies = new Assembly[]
            {
                typeof(Program).Assembly,
                typeof(Domain.Initializer).Assembly,
            };
            services.AddAutoMapper(config =>
            {
                config.AllowNullCollections = true;
            },assemblies);
        }

        public static IApplicationBuilder UseLowercaseUrls(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                // Küçük harfe dönüştürme
                context.Request.Path = context?.Request?.Path.Value?.ToLower();
                await next();
            });
        }
    }
}
