using System;
using NServiceBus;

namespace Shared.Command
{
    #region PlaceOrder

    public class PlaceOrder :
        ICommand
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
    }

    #endregion
}