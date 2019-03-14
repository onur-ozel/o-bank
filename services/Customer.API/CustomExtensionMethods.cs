using System;
using System.Reflection;
using Customer.API.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

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

        public static IServiceCollection AddSwagger (this IServiceCollection services) {
            services.AddSwaggerGen (options => {
                options.SwaggerDoc ("v1", new Info {
                    Title = "o-bank - Customer HTTP API",
                    Version = "v1",
                    Description = "The Cutomer Microservice HTTP API. This is a Data-Driven/CRUD microservice is implemented in .NET Core Web API",
                    TermsOfService = "Terms Of Service",
                    Contact = new Contact
                    {
                        Name = "Onur ÖZEL",
                        Email = "onurozel41@gmail.com",
                        Url = "https://github.com/onur-ozel"
                    }
                });
                
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
