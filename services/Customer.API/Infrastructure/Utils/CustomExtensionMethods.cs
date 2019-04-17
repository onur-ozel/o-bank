using System;
using System.IO;
using System.Reflection;
using Customer.API.Infrastructure.Contexts;
using Customer.API.Infrastructure.EventBuses;
using Customer.API.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Customer.API.Utils {
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

            }, ServiceLifetime.Transient);

            return services;
        }

        public static IServiceCollection AddEventBus (this IServiceCollection services, IConfiguration configuration) {
            services.AddSingleton<ICustomerEventBusService, CustomerKafkaEventBusService> (sp => {
                return new CustomerKafkaEventBusService ();
            });

            return services;
        }
    }
}