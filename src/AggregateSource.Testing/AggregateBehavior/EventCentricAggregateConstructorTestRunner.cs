﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AggregateSource.Testing.AggregateBehavior
{
    /// <summary>
    /// Represents an aggregate constructor test specification runner.
    /// </summary>
    public class EventCentricAggregateConstructorTestRunner : IEventCentricAggregateConstructorTestRunner
    {
        readonly IEventComparer _comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventCentricAggregateConstructorTestRunner"/> class.
        /// </summary>
        /// <param name="comparer">The comparer to use when comparing events.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="comparer"/> is <c>null</c>.</exception>
        public EventCentricAggregateConstructorTestRunner(IEventComparer comparer)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");
            _comparer = comparer;
        }

        /// <summary>
        /// Runs the specified test specification.
        /// </summary>
        /// <param name="specification">The test specification to run.</param>
        /// <returns>
        /// The result of running the test specification.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="specification"/> is <c>null</c>.</exception>
        public EventCentricAggregateConstructorTestResult Run(EventCentricAggregateConstructorTestSpecification specification)
        {
            if (specification == null) throw new ArgumentNullException("specification");
            IAggregateRootEntity sut = null;
            var result = Catch.Exception(() => sut = specification.SutFactory());
            if (result.HasValue)
            {
                return new EventCentricAggregateConstructorTestResult(
                    specification, 
                    TestResultState.Failed, 
                    Optional<object[]>.Empty, 
                    new Optional<Exception>(result.Value));
            }
            var actualEvents = sut.GetChanges().ToArray();
            if (!actualEvents.SequenceEqual(specification.Thens, new WrappedEventComparerEqualityComparer(_comparer)))
            {
                return new EventCentricAggregateConstructorTestResult(
                    specification, 
                    TestResultState.Failed, 
                    new Optional<object[]>(actualEvents),
                    Optional<Exception>.Empty);
            }
            return new EventCentricAggregateConstructorTestResult(
                specification, 
                TestResultState.Passed,
                Optional<object[]>.Empty,
                Optional<Exception>.Empty);
        }
    }
}