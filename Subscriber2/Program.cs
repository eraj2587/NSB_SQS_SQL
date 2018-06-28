using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber2
{
    class Program
    {
        static SendAndProcessEndpoint<BaseEndpointConfig> _endpoint;
        static void Main(string[] args)
        {
            _endpoint = new SendAndProcessEndpoint<BaseEndpointConfig>(new Subscriber2Config());
            AsyncMain().GetAwaiter().GetResult();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        private static async Task AsyncMain()
        {
            _endpoint.Initialize();
            Console.Title = "NSB.Subscriber2";

            await _endpoint.StartEndpoint();
            Console.ReadKey();
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            _endpoint.StopEndpoint();
        }
    }
}
