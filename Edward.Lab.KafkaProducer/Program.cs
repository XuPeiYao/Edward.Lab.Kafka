using Confluent.Kafka;

using System;
using System.Threading;

namespace Edward.Lab.KafkaProducer
{
    class Program
    {
        const string TOPIC_NAME = "my-test-topic";

        static void Main(string[] args)
        {
            var conf = new ProducerConfig { BootstrapServers = "kafka:9092" };

            using (var p = new ProducerBuilder<Null, string>(conf).Build())
            {
                int i = 0;
                while (true)
                {
                    p.Produce(TOPIC_NAME, new Message<Null, string> { Value = "MESSAGE-" + i++.ToString() });
                    Console.WriteLine("Sended Message " + i);
                    Thread.Sleep(1000);
                }

                // wait for up to 10 seconds for any inflight messages to be delivered.
                p.Flush(TimeSpan.FromSeconds(10));
            }

            Console.Read();
        }
    }
}
