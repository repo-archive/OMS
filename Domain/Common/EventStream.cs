using System.Collections.Generic;

namespace Domain.Common
{
    public class EventStream
    {
        // version of the event stream returned
        public long Version;
        // all events in the stream
        public List<IEvent> Events = new List<IEvent>();
    }
}