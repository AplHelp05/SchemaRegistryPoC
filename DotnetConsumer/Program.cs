// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;
using DotnetConsumer.ConsumerBll;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotnetConsumer
{
    class Program
    {

        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((context, collection) =>
            {
                collection.AddHostedService<Consumer>();
            });
    }
}