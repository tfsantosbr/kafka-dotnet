using System;
using System.Net;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace KafkaProducer.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:29092"
            };

            string topic = args[0];
            string message = args[1];

            using var producer = new ProducerBuilder<Null, string>(config).Build();

            var result = await producer.ProduceAsync(
                topic,
                new Message<Null, string>
                { Value = message });

            Console.WriteLine($"[Mensagem Enviada] \n" +
                $"Topico: {topic} \n" +
                $"Mensagem: {message} \n" +
                $"Status: {result.Status}");
        }
    }
}