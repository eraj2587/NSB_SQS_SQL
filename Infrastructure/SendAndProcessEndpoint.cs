using NServiceBus;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SendAndProcessEndpoint<T>
          where T : BaseEndpointConfig
    {
        #region Member Variables and Constatns

        private readonly BaseEndpointConfig _endPointConfig;
        private EndpointConfiguration _nsbConfig;
        private IEndpointInstance _endpointInstance;

        #endregion

        #region Constructors

        public SendAndProcessEndpoint(T config)
        {
            if (config == null)
                throw new ArgumentNullException("Endpoint configuration cannot be null");

            _endPointConfig = config;
        }

        #endregion

        #region Public Methods

        public virtual void Initialize()
        {
            _nsbConfig = _endPointConfig.BuildConfig();
        }

        public virtual async Task StartEndpoint()
        {
            _endpointInstance = await Endpoint.Start(_nsbConfig).ConfigureAwait(false);
        }

        public virtual EndpointConfiguration GetEndpointConfig()
        {
            if (_nsbConfig != null)
                return _nsbConfig;

            throw new Exception("Can not initialize NSB endpoint instance");
        }


        public virtual IEndpointInstance GetEndpointInstance()
        {
            if (_endpointInstance != null)
                return _endpointInstance;

            throw new Exception("Can not initialize NSB endpoint instance");
        }

        public virtual void StopEndpoint()
        {
            if (_endpointInstance != null)
                _endpointInstance.Stop().GetAwaiter().GetResult();
        }

        public virtual async Task SendMessage(ICommand message)
        {
            if (_endpointInstance != null)
                await _endpointInstance.Send(message).ConfigureAwait(false);
        }

        public virtual async Task SendMessage(string endpoint, ICommand message)
        {
            if (_endpointInstance != null)
                await _endpointInstance.Send(endpoint,message).ConfigureAwait(false);
        }

        public virtual async Task Subscribe()
        {
            if (_endpointInstance != null)
                await _endpointInstance.Subscribe<OrderPlaced>().ConfigureAwait(false);
        }

        public virtual void PublishMessage(IEvent message)
        {
            if (_endpointInstance != null)
                _endpointInstance.Publish(message).ConfigureAwait(false);
        }

        public string GetEndpointName()
        {
            return _endPointConfig._configEndpointName;
        }

        #endregion
    }
}
