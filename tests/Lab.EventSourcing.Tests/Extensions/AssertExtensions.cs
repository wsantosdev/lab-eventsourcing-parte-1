using Lab.EventSourcing.Core;
using System;
using System.Collections.Generic;

namespace Lab.EventSourcing.Tests.Extensions
{
    public partial class Assert
    {
        public static void EventEmitted<T>(IEnumerable<IEvent> events) where T : IEvent
        {
            foreach (var @event in events)
                if (@event.GetType() == typeof(T)) return;

            throw new Exception("Event not emitted.");
        }
    }
}
