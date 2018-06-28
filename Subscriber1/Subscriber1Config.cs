using Infrastructure;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber1
{
    class Subscriber1Config : BaseEndpointConfig
    {
        #region Constructors

        public Subscriber1Config() :
            this(null, false)
        {
        }

        public Subscriber1Config(string endpointName, bool isSendOnly) : 
            base(endpointName, isSendOnly)
        {
        }

        public override EndpointConfiguration BuildConfig()
        {
            var config = base.BuildConfig();
            return config;
        }

        #endregion
    }
}
