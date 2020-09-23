using System;
using Lab.EventSourcing.Core;

namespace Lab.EventSourcing.PriceAlert
{
    public class PriceAlert : EventSourcingModel
    {
        public Guid Id { get; private set; }
        public string Symbol { get; private set; }
        public decimal TargetPrice { get; private set; }
        public DateTime TriggeredAt { get; private set; }
        public bool Active { get; private set; }

        public static PriceAlert Create(string symbol, decimal targetPrice)
        {
            if(string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("A symbol must be provided.", nameof(symbol));

            if(targetPrice <= 0m)
                throw new ArgumentException("A target price greater than zero must be provided.", nameof(targetPrice));

            var alert = new PriceAlert();
            alert.RaiseEvent(new PriceAlertCreated(new Guid(), symbol, targetPrice));

            return alert;
        }

        public void Trigger()
        {
            if(TriggeredAt > DateTime.MinValue)
                throw new InvalidOperationException("A price alert cannot be triggered more than once.");

            if(!Active)
                throw new InvalidOperationException("An inactive price alert cannot be triggered.");

            RaiseEvent(new PriceAlertTriggered());
        }

        public void Cancel()
        {
            if(TriggeredAt > DateTime.MinValue)
                throw new InvalidOperationException("A triggered price alert cannot be cancelled.");

            if(!Active)
                throw new InvalidOperationException("A price alert cannot be cancelled more than once.");

            RaiseEvent(new PriceAlertCancelled());
        }

        public void Apply(PriceAlertCreated pendingEvent) =>
            (Id, Symbol, TargetPrice, Active) = (pendingEvent.Id, pendingEvent.Symbol, pendingEvent.TargetPrice, true);

        public void Apply(PriceAlertTriggered pendingEvent) =>
            (TriggeredAt, Active) = (pendingEvent.When, false);

        public void Apply(PriceAlertCancelled pendingEvent) =>
            Active = false;
    }
}
