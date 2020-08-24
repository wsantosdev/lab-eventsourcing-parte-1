using System;
using Lab.EventSourcing.Core;

namespace Lab.EventSourcing.PriceAlert
{
    public class PriceAlertCreated : EventBase 
    {
        public Guid Id { get; private set; }
        public string Symbol { get; private set; }
        public decimal TargetPrice { get; private set; }

        public PriceAlertCreated(Guid id, string symbol, decimal targetPrice) =>
            (Id, Symbol, TargetPrice) = (id, symbol, targetPrice);
    }
}