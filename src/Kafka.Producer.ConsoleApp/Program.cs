using System;
using System.Net;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Kafka.Producer.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "host.docker.internal:9092"
            };

            string topic = args[0];
            string input = null;

            Console.WriteLine("Iniciando produtor de mensagens...");
            Console.WriteLine($"Tópico: {topic}");
            Console.WriteLine("Digite a mensagem a ser enviada:");

            using var producer = new ProducerBuilder<Null, string>(config).Build();

            while (true)
            {
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    await SendMessage(producer, topic, input);
                }
            }
        }

        private static async Task SendMessage(IProducer<Null, string> producer, string topic, string message)
        {
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