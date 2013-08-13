﻿using System;

namespace AggregateSource.Testing.AggregateBehavior
{
    /// <summary>
    /// The when state within the test specification building process.
    /// </summary>
    public interface IAggregateCommandWhenStateBuilder
    {
        /// <summary>
        /// Then events should have occurred.
        /// </summary>
        /// <param name="events">The events that should have occurred.</param>
        /// <returns>A builder continuation.</returns>
        IAggregateCommandThenStateBuilder Then(params object[] events);

        /// <summary>
        /// Throws an exception.
        /// </summary>
        /// <param name="exception">The exception thrown.</param>
        /// <returns>A builder continuation.</returns>
        IAggregateCommandThrowStateBuilder Throws(Exception exception);
    }
}