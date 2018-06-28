using System;
using NServiceBus;

namespace Shared.Events
{
    #region OrderPlaced

    public class OrderPlaced : IEvent
    {
        public Guid OrderId { get; set; }
    }

    #endregion
}