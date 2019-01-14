using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                WebHost.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config
                            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                            .AddJsonFile("appsettings.json", true, true)
                            .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();
                    })
                    .ConfigureServices(s =>
                    {
                        s.AddOcelot();
                        s.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                    })
                    .Configure(a =>
                    {
                        a.UseOcelot().Wait();
                    }).Build().Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        //{
        //    return WebHost.CreateDefaultBuilder(args)
        //        .UseKestrel()
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .ConfigureAppConfiguration((hostingContext, config) =>
        //        {
        //            config
        //                .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
        //                .AddJsonFile("appsettings.json", true, true)
        //                .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true,
        //                    true)
        //                .AddJsonFile("ocelot.json")
        //                .AddEnvironmentVariables();
        //        })
        //        .UseStartup<Startup>();
    }
}
