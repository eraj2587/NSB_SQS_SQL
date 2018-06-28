using System;
using NServiceBus;

namespace Shared.Events
{
    #region BulkOrderPlaced

    public class BulkOrderPlaced : IEvent
    {
        public Guid OrderId { get; set; }
        public Byte[] Data { get; set; }
    }

    #endregion
}