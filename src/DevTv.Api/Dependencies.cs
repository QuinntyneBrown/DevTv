using DevTv.Core.Data;
using DevTv.Domain.Features.Videos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using BuildingBlocks.AspNetCore;
using DevTv.Core.Handlers;
using BuildingBlocks.EventStore;

namespace DevTv.Api
{
    public static class Dependencies
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Dev TV",
                    Description = "Devleloper Video Streaming",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Quinntyne Brown",
                        Email = "quinntynebrown@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });

                options.CustomSchemaIds(x => x.FullName);
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(isOriginAllowed: _ => true)
                .AllowCredentials()));

            services.AddEventStore();

            services.AddHttpContextAccessor();
            
            services.AddTransient<IDevTvDbContext, DevTvDbContext>();

            services.AddMediatR(typeof(CreateVideo), typeof(EventStoreChangedHandler));

            services.AddValidation(typeof(CreateVideo));
            
            services.AddDbContext<DevTvDbContext>(options =>
            {
                options
                .LogTo(Console.WriteLine)
                .UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"],
                    builder => builder.MigrationsAssembly("DevTv.Api")
                        .EnableRetryOnFailure())
                .EnableSensitiveDataLogging();
            });

            services.AddDbContext<EventStore>((options =>
            {
                options
                .LogTo(Console.WriteLine)
                .UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"],
                    builder => builder
                    .MigrationsAssembly("DevTv.Api")
                        .EnableRetryOnFailure())
                .EnableSensitiveDataLogging();
            }));

            services.AddControllers();
        }
    }
}
