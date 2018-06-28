using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.SQS;
using Infrastructure;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NServiceBus;
using NServiceBus.Persistence;
using NServiceBus.Transport.SQLServer;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static SendAndProcessEndpoint<BaseEndpointConfig> _endpoint;
        static void Main(string[] args)
        {
            _endpoint = new SendAndProcessEndpoint<BaseEndpointConfig>(new ServerConfig());
            AsyncMain().GetAwaiter().GetResult();
            //AsyncMain1().GetAwaiter().GetResult();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        private static async Task AsyncMain()
        {
            _endpoint.Initialize();
            Console.Title = "NSB.Server";
           
            await _endpoint.StartEndpoint();
            Console.ReadKey();
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            _endpoint.StopEndpoint();
        }

        static async Task AsyncMain1()
        {
            Console.Title = "Samples.PubSub.Publisher";
            var endpointConfiguration = new EndpointConfiguration("Samples.PubSub.Publisher");
            //endpointConfiguration.UsePersistence<LearningPersistence>();
            //endpointConfiguration.UseTransport<LearningTransport>();

            //Persistence
            var persistence = endpointConfiguration.UsePersistence<NHibernatePersistence>();
            var nhConfig = new NHibernate.Cfg.Configuration();
            nhConfig.SetProperty(NHibernate.Cfg.Environment.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider");
            nhConfig.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, "NHibernate.Driver.Sql2008ClientDriver");
            nhConfig.SetProperty(NHibernate.Cfg.Environment.Dialect, "NHibernate.Dialect.MsSql2008Dialect");
            nhConfig.SetProperty(NHibernate.Cfg.Environment.ConnectionString, ConfigurationManager.ConnectionStrings["NSB_AWS.NHibernatePersistence"].ConnectionString);
            nhConfig.SetProperty(NHibernate.Cfg.Environment.DefaultSchema, "nsb");
            persistence.UseConfiguration(nhConfig);
            //Transport
            var transport = endpointConfiguration.UseTransport<SqlServerTransport>()
                            .ConnectionString(ConfigurationManager.ConnectionStrings["NSB_AWS.SqlServerTransport"].ConnectionString);
            transport.DefaultSchema("nsb");

            var routing = transport.Routing();
            routing.RegisterPublisher(typeof(OrderPlaced), "Samples.PubSub.Publisher");

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            await Start(endpointInstance)
                .ConfigureAwait(false);
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }

        static async Task Start(IEndpointInstance endpointInstance)
        {
            Console.WriteLine("Press '1' to publish the OrderReceived event");
            Console.WriteLine("Press any other key to exit");

            #region PublishLoop

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                var orderReceivedId = Guid.NewGuid();
                if (key.Key == ConsoleKey.D1)
                {
                    var orderReceived = new OrderPlaced
                    {
                        OrderId = orderReceivedId
                    };
                    await endpointInstance.Publish(orderReceived)
                        .ConfigureAwait(false);
                    Console.WriteLine($"Published OrderReceived Event with Id {orderReceivedId}.");
                }
                else
                {
                    return;
                }
            }

            #endregion
        }
    }
 }
