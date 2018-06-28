using Infrastructure;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber2
{
    class Subscriber2Config : BaseEndpointConfig
    {
        #region Constructors

        public Subscriber2Config() :
            this(null, false)
        {
        }
        public Subscriber2Config(string endpointName, bool isSendOnly) :
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
