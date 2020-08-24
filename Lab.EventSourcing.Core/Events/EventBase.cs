using System;

namespace Lab.EventSourcing.Core
{
    public abstract class EventBase : IEvent
    {
        public DateTime When { get; private set; }

        public EventBase() =>
            When = DateTime.Now;
    }
}