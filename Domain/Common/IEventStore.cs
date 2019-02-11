#region (c) 2012-2012 Lokad - New BSD License 

// Copyright (c) Lokad 2012-2012, http://www.lokad.com
// This code is released as Open Source under the terms of the New BSD Licence

#endregion

using System.Collections.Generic;

namespace Domain.Common
{
    public interface IEvent { }

    public interface IEventStore
    {
        EventStream LoadEventStream(IIdentity id);
        EventStream LoadEventStream(IIdentity id, long skipEvents, int maxCount);
        /// <summary>
        /// Appends events to server stream for the provided identity.
        /// </summary>
        /// <param name="id">identity to append to.</param>
        /// <param name="expectedVersion">The expected version (specify -1 to append anyway).</param>
        /// <param name="events">The events to append.</param>
        /// <exception cref="OptimisticConcurrencyException">when new events were added to server
        /// since <paramref name="expectedVersion"/>
        /// </exception>
        void AppendToStream(IIdentity id, long expectedVersion, ICollection<IEvent> events);
    }
}