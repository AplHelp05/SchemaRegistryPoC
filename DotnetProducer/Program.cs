// See https://aka.ms/new-console-template for more information

using System;
using DotnetProducer.ProducerBll;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotnetProducer
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
                collection.AddHostedService<Producer>();
            });
    }
}

