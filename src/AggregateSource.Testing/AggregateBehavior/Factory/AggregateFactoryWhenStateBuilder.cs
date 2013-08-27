﻿using System;

namespace AggregateSource.Testing.AggregateBehavior.Factory
{
    class AggregateFactoryWhenStateBuilder : IAggregateFactoryWhenStateBuilder
    {
        readonly Func<IAggregateRootEntity> _sutFactory;
        readonly object[] _givens;
        readonly Func<IAggregateRootEntity, IAggregateRootEntity> _when;

        public AggregateFactoryWhenStateBuilder(Func<IAggregateRootEntity> sutFactory, object[] givens,
                                                Func<IAggregateRootEntity, IAggregateRootEntity> when)
        {
            _sutFactory = sutFactory;
            _givens = givens;
            _when = when;
        }

        public IAggregateFactoryThenNoneStateBuilder ThenNone()
        {
            return new AggregateFactoryThenNoneStateBuilder(_sutFactory, _givens, _when);
        }

        public IAggregateFactoryThenStateBuilder Then(params object[] events)
        {
            if (events == null) throw new ArgumentNullException("events");
            return new AggregateFactoryThenStateBuilder(_sutFactory, _givens, _when, events);
        }

        public IAggregateFactoryThrowStateBuilder Throws(Exception exception)
        {
            if (exception == null) throw new ArgumentNullException("exception");
            return new AggregateFactoryThrowStateBuilder(_sutFactory, _givens, _when, exception);
        }
    }
}