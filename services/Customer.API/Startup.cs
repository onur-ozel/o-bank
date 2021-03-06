﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Customer.API.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Customer.API {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddCustomMVC (Configuration)
                .AddEventBus (Configuration)
                .AddCustomDbContext (Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            app.UseStaticFiles (new StaticFileOptions {
                ServeUnknownFileTypes = true,
                    FileProvider = new PhysicalFileProvider (
                        Path.Combine (Directory.GetCurrentDirectory (), "Resources")
                    ),
                    RequestPath = "/Resources"
            });

            app.UseSwaggerUI (c => {
                c.RoutePrefix = "customer/swagger";
                c.EnableFilter ();
                c.SwaggerEndpoint ("/Resources/swagger/Customer.API.v1.yaml", "Customer.API.v1");
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseCors ("CorsPolicy");
            app.UseMvc ();
        }
    }
}