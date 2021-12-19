﻿using System;
using System.Collections.Generic;

namespace AsCore.Domain.Abstractions.BuildingBlocks
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>
    {
        [NonSerialized]
        private readonly Queue<IDomainEvent> _domainEvents = new();

        protected AggregateRoot(TKey id)
            : base(id)
        {
        }

        public int Version { get; private set; }

        public IDomainEvent[] DequeueDomainEvents()
        {
            var eventsArray = _domainEvents.ToArray();
            _domainEvents.Clear();

            return eventsArray;
        }

        protected void Enqueue(IDomainEvent @event)
        {
            _domainEvents.Enqueue(@event);
            Version++;
        }
    }
}
