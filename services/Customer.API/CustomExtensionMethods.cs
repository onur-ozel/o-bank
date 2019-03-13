using System;
using System.Reflection;
using Customer.API.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.API {
    public static class CustomExtensionMethods {
        public static IServiceCollection AddCustomMVC (this IServiceCollection services, IConfiguration configuration) {
            services.AddMvc ()
                .SetCompatibilityVersion (CompatibilityVersion.Version_2_2)
                .AddControllersAsServices ();

            services.AddCors (options => {
                options.AddPolicy ("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed ((host) => true)
                    .AllowAnyMethod ()
                    .AllowAnyHeader ()
                    .AllowCredentials ());
            });

            return services;
        }

        public static IServiceCollection AddCustomDbContext (this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<CustomerContext> (options => {
                options.UseSqlServer (configuration["ConnectionString"],
                    sqlServerOptionsAction : sqlOptions => {
                        sqlOptions.EnableRetryOnFailure (maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds (30), errorNumbersToAdd: null);
                    });

                options.ConfigureWarnings (warnings => warnings.Throw (RelationalEventId.QueryClientEvaluationWarning));
            }, ServiceLifetime.Transient);

            return services;
        }

        // public static IServiceCollection AddSwagger (this IServiceCollection services) {
        //     services.AddSwaggerGen (options => {
        //         options.DescribeAllEnumsAsStrings ();
        //         options.SwaggerDoc ("v1", new Swashbuckle.AspNetCore.Swagger.Info {
        //             Title = "eShopOnContainers - Catalog HTTP API",
        //                 Version = "v1",
        //                 Description = "The Catalog Microservice HTTP API. This is a Data-Driven/CRUD microservice sample",
        //                 TermsOfService = "Terms Of Service"
        //         });
        //     });

        //     return services;

        // }
    }
}