using Confluent.Kafka;

namespace Kafka.Consumer.WebApi.Consumers;

public class ApacheKafkaConsumerService : BackgroundService
{
    private readonly string _topic = "meutopico";
    private readonly string _groupId = "consumer-webapi";
    private readonly string _bootstrapServers = "host.docker.internal:9092";
    private readonly ILogger<ApacheKafkaConsumerService> _logger;

    public ApacheKafkaConsumerService(ILogger<ApacheKafkaConsumerService> logger)
    {
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        new Thread(() => StartConsumerLoop(cancellationToken)).Start();

        return Task.CompletedTask;
    }

    private void StartConsumerLoop(CancellationToken cancellationToken)
    {
        var consumerConfig = new ConsumerConfig
        {
            GroupId = _groupId,
            BootstrapServers = _bootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        try
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();

            consumer.Subscribe(_topic);

            _logger.LogInformation($"Consumer started | Topic: {_topic} | Group: {_groupId}");

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = consumer.Consume(cancellationToken);

                    _logger.LogInformation($"Consumed: '{result.Message.Value}'");
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Consumer is stopping...");

                consumer.Close();
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        _logger.LogInformation("Consumer is stopped...");
    }
}