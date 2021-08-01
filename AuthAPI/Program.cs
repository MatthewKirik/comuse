using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AuthAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    webBuilder.UseKestrel(opts =>
                    {
                        opts.Listen(IPAddress.Loopback, port: 5002);
                        //opts.ListenAnyIP(5003);
                        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                        var port = int.Parse(Environment.GetEnvironmentVariable("PORT") ?? "5003");
                        if (environment == Environments.Development)
                            opts.ListenAnyIP(port);
                        else
                            opts.ListenAnyIP(port, opts => opts.UseHttps());
                    });
                });
    }
}
