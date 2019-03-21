﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Customer.API {
    public class Program {
        public static void Main (string[] args) {
            CreateWebHostBuilder (args).Build ().Run ();
        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .ConfigureAppConfiguration ((hostingContext, config) => {
                string runningEnvironment = Environment.GetEnvironmentVariable("RUNNING_ENVIRONMENT") ?? "Development";

                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings." + runningEnvironment + ".json", true);
            })
            .UseStartup<Startup> ();
    }
}
