using System.Reflection;
using FluentValidation.AspNetCore;
using GitHub.Implementations;
using GitHub.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moderato.Application.PipelineBehaviors;
using Moderato.Application.Queries;
using Moderato.Application.Services;
using Moderato.Infrastructure.Github;
using Moderato.Infrastructure.Services.GitUsers;

namespace Moderato.Api.Extensions
{
    public static class ServicesRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(typeof(GetRepositorySummary).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient<IGitUsersService, GitUsersService>();
            services.AddHttpClient<IGitClient, GitHubClient>();
            services.Decorate<IGitClient, CachedGitHubClient>();
            services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()))
                .AddNewtonsoftJson();
            if (configuration["UseInMemCache"] == "True")
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = configuration.GetConnectionString("redis") ?? "localhost";
                    options.InstanceName = "Moderato.Cache";
                });
            }

            return services;
        }
    }
}