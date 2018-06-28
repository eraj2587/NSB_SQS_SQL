using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IEndpointConfig
    {
        #region Methods

        EndpointConfiguration BuildConfig();

        #endregion
    }
}
