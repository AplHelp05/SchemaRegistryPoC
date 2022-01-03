using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using schema;

namespace DotnetConsumer.ConsumerBll
{
    public class Consumer: IHostedService
    {
        private readonly ILogger<Consumer> _logger;
        private readonly CachedSchemaRegistryClient _registry;
        private readonly ConsumerConfig _consumerConfig; 
        
        public Consumer(ILogger<Consumer> logger)
        {
            _logger = logger;
            var config = new[] {new KeyValuePair<string,string>("schema.registry.url", "http://localhost:8081")};
            _registry = new CachedSchemaRegistryClient(config);
            _consumerConfig = new ConsumerConfig
            { 
                GroupId = "poc-104",
                BootstrapServers = "localhost:9093"
            };
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var deserializer = new AvroDeserializer<payment>(_registry);
            var consumerBuilder = new ConsumerBuilder<Null, payment>(_consumerConfig)
                .SetValueDeserializer(deserializer.AsSyncOverAsync())
                .SetErrorHandler((_, error) => _logger.LogError(error.ToString()));
            await ConsumerAsync(consumerBuilder, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        
        private async Task ConsumerAsync(ConsumerBuilder<Null, payment> consumerBuilder, CancellationToken ct)
        {
            var topic = "Payment";

            
            using (var consumer = consumerBuilder.Build())
            {
                consumer.Subscribe(topic);

                while (true)
                {
                    var result = consumer.Consume().Message.Value;
                    
                    _logger.LogInformation($"departmentId {result.departmentId} " +
                                           $"paymentFee {result.paymentFee}");
                }
            }
        }
    }
}