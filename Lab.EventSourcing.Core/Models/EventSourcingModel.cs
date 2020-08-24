using System.Collections.Generic;
using System.Linq;

namespace Lab.EventSourcing.Core
{
    public abstract class EventSourcingModel
    {
        private Queue<IEvent> _pendingEvents = new Queue<IEvent>();
        public IEnumerable<IEvent> PendingEvents { get => _pendingEvents.AsEnumerable(); }

        protected void RaiseEvent<TEvent>(TEvent pendingEvent) where TEvent: IEvent
        {
            _pendingEvents.Enqueue(pendingEvent);
            ((dynamic)this).Apply((dynamic)pendingEvent);
        }

        public void Commit() =>
            _pendingEvents.Clear();
    }
}