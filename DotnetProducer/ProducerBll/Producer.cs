using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Avro;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using schema;

namespace DotnetProducer.ProducerBll
{
    public class Producer: IHostedService
    {
        private readonly ILogger<Producer> _logger;
        private readonly CachedSchemaRegistryClient _registry;
        private readonly ProducerConfig _producerConfig;
        public Producer(ILogger<Producer> logger)
        {
            _logger = logger;
            var config = new[] {new KeyValuePair<string,string>("schema.registry.url", "http://localhost:8081")};
            _registry = new CachedSchemaRegistryClient(config);
            _producerConfig = new ProducerConfig()
            {
                BootstrapServers = "localhost:9093"
            };
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var p = new payment()
            {
                departmentId = "1234d",
                paymentFee = new AvroDecimal(23.900m)
            };
            
            var serializerValue = new AvroSerializer<payment>(_registry);
            
            var producer = new ProducerBuilder<Null, payment>(_producerConfig)
                .SetValueSerializer(serializerValue).Build();

            await ProducerAsync(producer, p, cancellationToken);
            _logger.LogInformation("Message sucessfully sent");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task ProducerAsync(IProducer<Null, payment> producer, payment paymentValue, CancellationToken ct)
        {
            try
            {
                var result = await producer.ProduceAsync("Payment", new Message<Null, payment>()
                {
                    Value = paymentValue
                }, ct);
            }
            catch (ProduceException<Null, payment> e)
            {
                var errorMessage = e.Message;
                _logger.LogError(errorMessage);
            }
            producer.Flush(TimeSpan.FromSeconds(10));
        }
    }
}