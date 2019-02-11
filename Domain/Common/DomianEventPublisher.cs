// Copyright 2012,2013 Vaughn Vernon
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public class DomainEventPublisher
    {
        private bool m_publishing;
        List<IDomainEventSubscriber<IDomainEvent>> m_subscribers;

        public DomainEventPublisher()
        {
            m_publishing = false;
        }

        List<IDomainEventSubscriber<IDomainEvent>> Subscribers
        {
            get => m_subscribers ?? (m_subscribers = new List<IDomainEventSubscriber<IDomainEvent>>());
            set => m_subscribers = value;
        }

        public void Publish<T>(T domainEvent) where T : IDomainEvent
        {
            if (m_publishing || !HasSubscribers()) return;

            try
            {
                m_publishing = true;

                var eventType = domainEvent.GetType();

                foreach (var subscriber in Subscribers)
                {
                    var subscribedToType = subscriber.SubscribedToEventType();
                    if (eventType == subscribedToType || subscribedToType == typeof(IDomainEvent))
                    {
                        subscriber.HandleEvent(domainEvent);
                    }
                }
            }
            finally
            {
                m_publishing = false;
            }
        }

        public void PublishAll(ICollection<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                Publish(domainEvent);
            }
        }

        public void Reset()
        {
            if (!m_publishing)
            {
                Subscribers = null;
            }
        }

        public void Subscribe(IDomainEventSubscriber<IDomainEvent> subscriber)
        {
            if (!m_publishing)
            {
                Subscribers.Add(subscriber);
            }
        }

        public void Subscribe(Action<IDomainEvent> handle)
        {
            Subscribe(new DomainEventSubscriber<IDomainEvent>(handle));
        }

        class DomainEventSubscriber<TEvent> : IDomainEventSubscriber<TEvent>
            where TEvent : IDomainEvent
        {
            public DomainEventSubscriber(Action<TEvent> handle)
            {
                m_handle = handle;
            }

            readonly Action<TEvent> m_handle;

            public void HandleEvent(TEvent domainEvent)
            {
                m_handle(domainEvent);
            }

            public Type SubscribedToEventType()
            {
                return typeof(TEvent);
            }
        }

        bool HasSubscribers()
        {
            return m_subscribers != null && Subscribers.Count != 0;
        }
    }
}
