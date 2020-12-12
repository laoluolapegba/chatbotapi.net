using System;
using RabbitMQ.Client;
using MomoOrchestrator.Core;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Text;

namespace ConsumerCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for Messages...");

            Uri uri = new Uri("amqp://queuser:queuser@209.97.189.137:5672");
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = uri;
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            // accept only one unack-ed message at a time
            // uint prefetchSize, ushort prefetchCount, bool global
            channel.BasicQos(0, 1, false);
            MessageReceiver messageReceiver = new MessageReceiver(channel);
            channel.BasicConsume("bot_in_ministatement", false, messageReceiver);

            Console.ReadLine();
            /*ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = uri;
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "bot_in_balanceenq",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var deserialized = JsonConvert.DeserializeObject<InquiryRequest>(message);
                    Console.WriteLine(" [x] Received Msg with transId {0}  from msisdn {1} at {2}", deserialized.transactionId, deserialized.msisdn, deserialized.transactionDate);
                };
                channel.BasicConsume(queue: "bot_in_balanceenq",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

            */
        }
    }
}
