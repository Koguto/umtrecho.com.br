using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Site;
using System;

namespace Site
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
                    // Especificando a classe Startup
                    webBuilder.UseStartup<Startup>();

#if !DEBUG
                // Porta para Release / produção (Railway)
                var port = Environment.GetEnvironmentVariable("PORT") ?? "80";
                webBuilder.UseUrls($"http://0.0.0.0:{port}");
    
#endif

                });
    }
}
