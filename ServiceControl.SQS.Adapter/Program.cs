using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using NServiceBus;
using ServiceControl.TransportAdapter;
using Amazon;
using Amazon.SQS;

namespace ServiceControl.SQS.Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
            Console.ReadKey();
        }

        public static async Task AsyncMain()
        {
            Console.Title = "Samples.ServiceControl.SqsTransportAdapter.Adapter";
            #region AdapterTransport

            var transportAdapterConfig = new TransportAdapterConfig<SqsTransport, MsmqTransport>("ServiceControl.SQS.Adapter");
            transportAdapterConfig.EndpointSideAuditQueue = "audit";
            transportAdapterConfig.EndpointSideErrorQueue = "error";
            transportAdapterConfig.EndpointSideControlQueue = "Particular.ServiceControl";

            #endregion

            #region EndpointSideConfig

            transportAdapterConfig.CustomizeEndpointTransport(transport =>
            {
               var s3Configuration=transport.S3("nsb-poc", "name/order");

                transport.ClientFactory(() => new AmazonSQSClient(new AmazonSQSConfig
                {
                    RegionEndpoint = RegionEndpoint.USEast1,
                }));

                s3Configuration.ClientFactory(() => new AmazonS3Client(
                        new AmazonS3Config
                        {
                            RegionEndpoint = RegionEndpoint.USEast1
                        }));
            });

            #endregion

            var adapter =TransportAdapter.TransportAdapter.Create(transportAdapterConfig);

            await adapter.Start()
                .ConfigureAwait(false);

            Console.WriteLine("Press <enter> to shutdown adapter.");
            Console.ReadLine();

            await adapter.Stop()
                .ConfigureAwait(false);
        }
    }
}
