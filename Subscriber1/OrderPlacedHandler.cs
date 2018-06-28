using System.Threading.Tasks;
using NServiceBus;
using Shared;
using System;
using Shared.Command;
using Shared.Events;

#region PlaceOrderHandler

public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
{
    public Task Handle(OrderPlaced message, IMessageHandlerContext context)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Handled by subscriber 1: OrderPlaced for Order Id: "+message.OrderId);
        return Task.CompletedTask;
    }
}

#endregion
