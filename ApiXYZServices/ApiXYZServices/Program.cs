using ApiXYZServices;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ApiXYZServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                 .UseContentRoot(Directory.GetCurrentDirectory())
               .UseIISIntegration()
               .UseStartup<Startup>()
            .ConfigureKestrel(serverOptions =>
            {
                serverOptions.ConfigureHttpsDefaults(configureOptions =>
                {
                    configureOptions.SslProtocols = System.Security.Authentication.SslProtocols.Tls13;
                });
            });



    }
}
