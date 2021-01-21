using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using AddinEngine;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (InDocker)
            {
                AddinManager.AddinFolderPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/Addins";
            }
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Global.CreateHostBuilder(args)
                // Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel((hostContext, opt) =>
                    {
                        if (hostContext.HostingEnvironment.IsDevelopment())
                        {
                            opt.Listen(IPAddress.Any,
                                5051, 
                                listenOptions =>  { }
                            );
                        }
                    });

                });

        private static bool InDocker => Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
    }
}
