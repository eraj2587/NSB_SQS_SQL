using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ClientConfig : BaseEndpointConfig
    {
        #region Constructors

        public ClientConfig() :
            this(null, false)
        {
        }

        public ClientConfig(string endpointName, bool isSendOnly) : 
            base(endpointName, isSendOnly)
        {
        }

        #endregion
    }
}
