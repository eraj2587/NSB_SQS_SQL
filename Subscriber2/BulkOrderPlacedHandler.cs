using System.Threading.Tasks;
using NServiceBus;
using Shared;
using System;
using Shared.Command;
using Shared.Events;

#region BulkOrderPlacedHandler

public class BulkOrderPlacedHandler : IHandleMessages<BulkOrderPlaced>
{
    public Task Handle(BulkOrderPlaced message, IMessageHandlerContext context)
    {
        Console.WriteLine("Handled by subscriber 2: BulkOrderPlaced for Order Id: "+message.OrderId);
        return Task.CompletedTask;
    }
}

#endregion
