using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using GitHub.Implementations;
using GitHub.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moderato.Api.Middleware;
using Moderato.Application.PipelineBehaviors;
using Moderato.Application.Queries;
using Moderato.Application.Services;
using Moderato.Infrastructure.Github;
using Moderato.Infrastructure.Services.GitUsers;

namespace Moderato.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(GetRepositorySummary).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient<IGitUsersService, GitUsersService>();
            services.AddHttpClient<IGitClient, GitHubClient>();
            services.Decorate<IGitClient, CachedGitHubClient>();
            services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()))
                .AddNewtonsoftJson();
            services.AddDistributedMemoryCache();
            // if (Environment.IsDevelopment())
            // {
            //     services.AddDistributedMemoryCache();
            // }
            // else
            // {
            //     services.AddStackExchangeRedisCache(options =>
            //     {
            //         options.Configuration = Configuration.GetConnectionString("redis") ?? "localhost";
            //         options.InstanceName = "Moderato.Cache";
            //     });
            // }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}