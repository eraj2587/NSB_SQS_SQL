using Infrastructure;
using NServiceBus;
using System;

namespace Server
{
    class ServerConfig : BaseEndpointConfig
    {
        #region Constructors

        public ServerConfig() :
            this(null, false)
        {
        }

        public ServerConfig(string endpointName, bool isSendOnly) : 
            base(endpointName, isSendOnly)
        {

        }

        public override EndpointConfiguration BuildConfig()
        {
            return base.BuildConfig();
        }

        #endregion
    }
}
