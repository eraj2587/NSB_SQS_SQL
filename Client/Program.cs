using Amazon;
using Amazon.S3;
using Amazon.SQS;
using Infrastructure;
using NServiceBus;
using NServiceBus.Persistence;
using NServiceBus.Transport.SQLServer;
using Shared;
using Shared.Command;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static SendAndProcessEndpoint<BaseEndpointConfig> _endpoint;
        static void Main(string[] args)
        {
            _endpoint = new SendAndProcessEndpoint<BaseEndpointConfig>(new ClientConfig());
            AsyncMain().GetAwaiter().GetResult();
        }

        private static async Task AsyncMain()
        {
            _endpoint.Initialize();
            await _endpoint.StartEndpoint();
            Console.Title = "NSB.Client";
            try
            {
                await SendOrder()
                    .ConfigureAwait(false);
            }
            finally
            {
                _endpoint.StopEndpoint();
            }
        }

        static async Task SendOrder()
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Press enter number of messages to be sent");
                Console.ForegroundColor = ConsoleColor.White;
                var number =Convert.ToInt32(Console.ReadLine());
                int a = 0;
                while (a < number) {
                    var id = Guid.NewGuid();

                    var placeOrder = new PlaceOrder
                    {
                        Product = "New shoes",
                        Id = id
                    };
                    await _endpoint.SendMessage(placeOrder)
                        .ConfigureAwait(false);
                    if(a%2==0)
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Sent a PlaceOrder message with id: {0} : {1}",a+1,id);
                   a++;
               }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
