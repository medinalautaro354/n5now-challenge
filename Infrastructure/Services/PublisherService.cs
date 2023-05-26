using Application.Common.Services;
using Application.Dtos;
using Confluent.Kafka;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.ConfigurationServices;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IProducer<Null, string> _producer;
        private readonly ILogger<IPublisherService> _logger;
        private readonly KafkaConfiguration _kafkaConfiguration;

        public PublisherService(ILogger<IPublisherService> logger, KafkaConfiguration kafkaConfiguration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _kafkaConfiguration = kafkaConfiguration ?? throw new ArgumentNullException(nameof(kafkaConfiguration));

            var config = new ProducerConfig()
            {
                BootstrapServers = _kafkaConfiguration.BootstrapServer
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task Publish(PublishDto publishDto, CancellationToken cancellationToken)
        {
            try
            {
                var value = JsonSerializer.Serialize(publishDto, new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() }
                });

                await _producer.ProduceAsync(_kafkaConfiguration.PermissionsTopic, new Message<Null, string>()
                {
                    Value = value
                }, cancellationToken);

                _logger.LogInformation(
                    "Event successfully published in the topic {topicName}. Event message {eventMessage}",
                    _kafkaConfiguration.PermissionsTopic, value);
            }
            catch (Exception e)
            {
                _logger.LogError("Unknown error when trying to publish in kafka. Error {errorMessage}", e.Message);
                throw;
            }
        }
    }
}