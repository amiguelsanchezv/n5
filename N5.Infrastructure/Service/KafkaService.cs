using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using N5.Application;
using N5.Domain;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace N5.Infrastructure
{
    public class KafkaService : IKafkaService
    {
        private readonly IConfiguration _configuration;
        private IProducer<Null, string> _client;
        private readonly ILogger<KafkaService> _logger;
        private readonly object _lock = new object();

        public KafkaService(IConfiguration configuration, ILogger<KafkaService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private IProducer<Null, string> GetClient()
        {
            if (_client == null)
            {
                lock (_lock)
                {
                    if (_client == null)
                    {
                        var kafkaServer = _configuration.GetSection("Kafka:Host").Value;
                        _logger?.LogInformation($"Kafka Host from configuration: {kafkaServer ?? "NULL"}");
                        
                        if (string.IsNullOrEmpty(kafkaServer))
                        {
                            kafkaServer = "localhost:9092";
                            _logger?.LogWarning("Kafka Host not configured, using default: localhost:9092");
                        }
                        
                        _logger?.LogInformation($"Using Kafka BootstrapServers: {kafkaServer}");
                        var config = new ProducerConfig { BootstrapServers = kafkaServer };
                        _client = new ProducerBuilder<Null, string>(config).Build();
                    }
                }
            }
            return _client;
        }

        public async Task WriteKafka(string operation)
        {
            try
            {
                var client = GetClient();
                var kafkaTopic = _configuration.GetSection("Kafka:Topic").Value;
                OperationRegistry operationDTO = new OperationRegistry()
                {
                    Id = Guid.NewGuid().ToString(),
                    Operation = operation
                };
                var message = new Message<Null, string> { Value = JsonSerializer.Serialize(operationDTO) };
                await client.ProduceAsync(kafkaTopic, message);
            }
            catch (Exception e)
            {
                throw new Exception("Error sending to KAFKA.", e.InnerException);
            }
        }
    }
}
