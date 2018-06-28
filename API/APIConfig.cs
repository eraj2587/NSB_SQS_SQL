using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace API
{
    class APIConfig : BaseEndpointConfig
    {
        #region Constructors

        public APIConfig() :
            this(null, true)
        {
        }

        public APIConfig(string endpointName, bool isSendOnly) : 
            base(endpointName, isSendOnly)
        {

        }

        public override EndpointConfiguration BuildConfig()
        {
            var config= base.BuildConfig();
            config.MakeInstanceUniquelyAddressable("1");
            return config;
        }

        #endregion
    }
}
