// <copyright file="InitializerTrial.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer
{
    using System;

    /// <summary>
    /// Represents an initialier trial.
    /// </summary>
    public class InitializerTrial
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InitializerTrial"/> class.
        /// </summary>
        /// <param name="begin">Initializer execution begin date.</param>
        internal InitializerTrial(
            DateTimeOffset begin)
        {
            Begin = begin;
        }

        /// <summary>
        /// Gets the initializer execution begin date.
        /// </summary>
        public DateTimeOffset Begin { get; }

        /// <summary>
        /// Gets the initializer execution end date.
        /// </summary>
        public DateTimeOffset? End { get; private set; }

        /// <summary>
        /// Gets the initializer exception thrown if any.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Mak the trial as executed.
        /// </summary>
        internal void Executed()
        {
            End = DateTimeOffset.Now;
            Exception = null;
        }

        /// <summary>
        /// Mak the trial as executed with an exception.
        /// </summary>
        /// <param name="ex">The exception thrown during the execution.</param>
        internal void ExecutedWithException(Exception ex)
        {
            End = DateTimeOffset.Now;
            Exception = ex;
        }
    }
}
