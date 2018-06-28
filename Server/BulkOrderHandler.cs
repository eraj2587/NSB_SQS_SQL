using System.Threading.Tasks;
using NServiceBus;

using Shared;
using System;
using Shared.Command;
using Shared.Events;

#region PlaceOrderHandler

public class BulkOrderHandler :
    IHandleMessages<BulkOrder>
{
    public Task Handle(BulkOrder message, IMessageHandlerContext context)
    {
        Console.WriteLine("Bulk Order placed with id: "+message.Id);
        Console.WriteLine("Publishing: BulkOrderPlaced for Order Id: "+message.Id);

        //var orderPlaced = new BulkOrderPlaced
        //{
        //   OrderId=message.Id,
        //    Data=message.Data
        //};
        //return context.Publish(orderPlaced);
        return Task.FromResult("");
    }
}

#endregion
