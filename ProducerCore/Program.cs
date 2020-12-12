using System;
using System.Text;
using RabbitMQ.Client;
using MomoOrchestrator.Core;
using Newtonsoft.Json;

namespace ProducerCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter RequestType: 1=Inq, 2=MiniStatement, 3=AirtimeTopup, 4=P2pTransfer");
            ConsoleKeyInfo input = Console.ReadKey();
            if( char.IsDigit(input.KeyChar))
            {
                Console.WriteLine("Producing Messages...");
                Uri uri = new Uri("amqp://queuser:queuser@209.97.189.137:5672");
                ConnectionFactory factory = new ConnectionFactory();
                factory.Uri = uri;


                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {

                    int option = int.Parse(input.KeyChar.ToString());
                    switch (option)
                    {
                        case 1:
                            var inqmsg = new InquiryRequest
                            {
                                messageType = 200,
                                msisdn = "22584212244",
                                numoftransactions = 0,
                                serviceId = "1001",
                                transactionDate = DateTime.Now,
                                transactionId = Guid.NewGuid().ToString(),
                                conversationID = "935cf4443225415ca92a7462be93048"
                            };
                            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(inqmsg));
                            channel.BasicPublish(exchange: "momo_exchange",
                                                 routingKey: "balanceenq",
                                                 basicProperties: null,
                                                 body: body);
                            Console.WriteLine(" [x] Sent {0}", inqmsg);
                            break;
                        case 2:
                            var minimsg = new MiniStatementRequest
                            {
                                messageType = 200,
                                msisdn = "22584212244",
                                numoftransactions = 3,
                                serviceId = "1002",
                                transactionDate = DateTime.Now,
                                transactionId = Guid.NewGuid().ToString(),
                                conversationID = "935cf4443225415ca92a7462be93048"
                            };
                            var minibody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(minimsg));
                            channel.BasicPublish(exchange: "momo_exchange",
                                                 routingKey: "ministatement",
                                                 basicProperties: null,
                                                 body: minibody);
                            Console.WriteLine(" [x] Sent {0}", minimsg);
                            break;
                        case 3:
                            var topupmsg = new AirtimeTopupRequest
                            {
                                messageType = 200,
                                msisdn = "22546017993",
                                serviceId = "1003",
                                amount = 100,
                                transactionDate = DateTime.Now,
                                transactionId = "41211212121",
                                receiverMsisdn = "22556999124"
                            };
                            var airtbody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(topupmsg));
                            channel.BasicPublish(exchange: "momo_exchange",
                                                 routingKey: "payment",
                                                 basicProperties: null,
                                                 body: airtbody);
                            Console.WriteLine(" [x] Sent {0}", topupmsg);
                            break;
                        case 4:
                            var p2pmsg = new AirtimeTopupRequest
                            {
                                messageType = 200,
                                msisdn = "22546017993",
                                serviceId = "1004",
                                amount = 100,
                                transactionDate = DateTime.Now,
                                transactionId = Guid.NewGuid().ToString(),
                                receiverMsisdn = "22556999124"
                            };
                            var p2pbody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(p2pmsg));
                            channel.BasicPublish(exchange: "momo_exchange",
                                                 routingKey: "p2p",
                                                 basicProperties: null,
                                                 body: p2pbody);
                            Console.WriteLine(" [x] Sent {0}", p2pmsg);
                            break;
                        default:
                            break;
                    }



                    //channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                    //var message = GetMessage(args);
                    //var body = Encoding.UTF8.GetBytes(message);
                    //channel.BasicPublish(exchange: "logs",
                    //                     routingKey: "",
                    //                     basicProperties: null,
                    //                     body: body);
                    //Console.WriteLine(" [x] Sent {0}", message);




                }
            }
            else
            {
                Console.WriteLine("Invalid input...");
            }
            

            

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
