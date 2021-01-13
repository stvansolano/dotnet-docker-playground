using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Threading;

namespace Dotnet_Backend
{
    // dotnet run --urls="http://+:5000;https://+:5001"
    public class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Program>();
            })
            .ConfigureServices(services =>
            {
                services.AddHostedService<PublisherBackgroundService>();
            });
            
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
    
    internal class PublisherBackgroundService : BackgroundService
    {
        public PublisherBackgroundService(ILogger<PublisherBackgroundService> logger, IConfiguration configuration)
        {
            Logger = logger;
            Configuration = configuration;

            Logger.LogInformation($"{nameof(PublisherBackgroundService)} Service initialized.");

        }

        public ILogger Logger { get; }
        public IConfiguration Configuration { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)  
        {
            stoppingToken.ThrowIfCancellationRequested();
            Logger.LogInformation($"{nameof(PublisherBackgroundService)} Service is stopping.");

            using ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis"));
            IDatabase db = redis.GetDatabase();

            var channel = redis.GetSubscriber().Subscribe("myCounter");
            channel.OnMessage(message =>
            {
                Console.WriteLine((string)message.Message);
            });
            
            await Task.Delay(Timeout.Infinite, stoppingToken);

            Logger.LogDebug($"{nameof(PublisherBackgroundService)} is stopping.");
        } 

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation($"{nameof(PublisherBackgroundService)} Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
