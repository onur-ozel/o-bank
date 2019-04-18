using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Customer.API.Infrastructure.Contexts;
using Customer.API.Infrastructure.EventBuses;
using Customer.API.Infrastructure.Utils;
using Customer.API.Models;
using Microsoft.AspNetCore.Http;
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

    public class ErrorHandlingMiddleware {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware (RequestDelegate next) {
            this.next = next;
        }

        public async Task Invoke (HttpContext context /* other dependencies */ ) {
            try {
                await next (context);
            } catch (Exception ex) {
                await HandleExceptionAsync (context, ex);
            }
        }

        private static Task HandleExceptionAsync (HttpContext context, Exception ex) {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            Error a = new Error ();

            a.Title = "Server error";
            a.Message = ex.Message;
            a.StackTrace = ex.StackTrace;

            // if (ex is MyNotFoundException) code = HttpStatusCode.NotFound;
            // else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            // else if (ex is MyException) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject (a);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync (result);
        }
    }
}