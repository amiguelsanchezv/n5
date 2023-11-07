using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Microsoft.Extensions.Configuration;
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
        private readonly IProducer<Null, string> _client;

        public KafkaService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = CreateInstance().Build();
        }
        private ProducerBuilder<Null, string> CreateInstance()
        {
            var kafkaServer = _configuration.GetSection("Kafka:Host").Value;
            var schemaRegistry = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = kafkaServer });
            var config = new ProducerConfig { BootstrapServers = kafkaServer };
            var builder = new ProducerBuilder<Null, string>(config);
            return builder;
        }

        public async Task WriteKafka(string operation)
        {
            try
            {
                var kafkaTopic = _configuration.GetSection("Kafka:Topic").Value;
                OperationRegistry operationDTO = new OperationRegistry()
                {
                    Id = Guid.NewGuid().ToString(),
                    Operation = operation
                };
                var message = new Message<Null, string> { Value = JsonSerializer.Serialize(operationDTO) };
                await _client.ProduceAsync(kafkaTopic, message);
            }
            catch (Exception e)
            {
                throw new Exception("Error enviando a KAFKA.", e.InnerException);
            }
        }
    }
}
