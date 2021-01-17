using Confluent.Kafka;

using System;
using System.Threading;

namespace Edward.Lab.KafkaConsumer
{
    class Program
    {
        const string TOPIC_NAME = "my-test-topic";
        static void Main(string[] args)
        {
            var conf = new ConsumerConfig
            {
                GroupId = nameof(Edward.Lab.KafkaConsumer),
                BootstrapServers = "kafka:9092",
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                consumer.Subscribe(TOPIC_NAME);

                while (true)
                {
                    try
                    {
                        var cr = consumer.Consume(cts.Token);

                        if (cr == null) continue;

                        Console.WriteLine($"Message: {cr.Message.Value}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e);
                    }
                }
            }

            Console.Read();
        }
    }
}
