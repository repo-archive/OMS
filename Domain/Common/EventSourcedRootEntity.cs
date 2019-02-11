using System.Collections.Generic;

namespace Domain.Common
{
    public abstract class EventSourcedRootEntity : EntityWithCompositeId
    {
        protected EventSourcedRootEntity()
        {
            this.m_mutatingEvents = new List<IDomainEvent>();
        }

        protected EventSourcedRootEntity(IEnumerable<IDomainEvent> eventStream, int streamVersion)
            : this()
        {
            foreach (var e in eventStream)
                When(e);
            m_unmutatedVersion = streamVersion;
        }

        readonly List<IDomainEvent> m_mutatingEvents;
        readonly int m_unmutatedVersion;

        protected int MutatedVersion => m_unmutatedVersion + 1;

        protected int UnmutatedVersion => m_unmutatedVersion;

        public IList<IDomainEvent> GetMutatingEvents()
        {
            return m_mutatingEvents.ToArray();
        }

        void When(IDomainEvent e)
        {
            (this as dynamic).Apply(e);
        }

        protected void Apply(IDomainEvent e)
        {
            m_mutatingEvents.Add(e);
            When(e);
        }
    }
}
