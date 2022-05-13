using System;
using System.Threading;
using Confluent.Kafka;

namespace Kafka.Consumer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string topic = args[0];
            string groupId = args[1];

            var conf = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = "localhost:9091,localhost:9092,localhost:9093",
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            Console.WriteLine("Iniciando consumidor...");
            Console.WriteLine($"Tópico: {topic}");
            Console.WriteLine($"Grupo: {groupId}");

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                c.Subscribe(topic);

                var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);
                            Console.WriteLine($"Consumed: '{cr.Message.Value}'");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    c.Close();
                }
            }
        }
    }
}
