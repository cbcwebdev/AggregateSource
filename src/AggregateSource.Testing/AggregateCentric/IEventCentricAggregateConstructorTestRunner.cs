﻿using System;

namespace AggregateSource.Testing.AggregateCentric
{
    /// <summary>
    /// Represents an aggregate constructor test specification runner.
    /// </summary>
    public interface IEventCentricAggregateConstructorTestRunner
    {
        /// <summary>
        /// Runs the specified test specification.
        /// </summary>
        /// <param name="specification">The test specification to run.</param>
        /// <returns>The result of running the test specification.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="specification"/> is <c>null</c>.</exception>
        EventCentricAggregateConstructorTestResult Run(EventCentricAggregateConstructorTestSpecification specification);
    }
}