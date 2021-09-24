// <copyright file="InitializerHistory.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer
{
    using System;
    using System.Collections.Generic;
    using Dawn;

    /// <summary>
    /// History of an initializer.
    /// </summary>
    public class InitializerHistory
    {
        private readonly List<InitializerTrial> tries;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitializerHistory"/> class.
        /// </summary>
        /// <param name="initializer">Initializer instance.</param>
        internal InitializerHistory(IInitializer initializer)
        {
            Initializer = Guard.Argument(initializer, nameof(initializer)).NotNull().Value;
            InitializerStatus = InitializerStatus.NeverRun;
            tries = new List<InitializerTrial>();
        }

        /// <summary>
        /// Gets the instance of the initializer.
        /// </summary>
        public IInitializer Initializer { get; }

        /// <summary>
        /// Gets the list of initialization tries.
        /// </summary>
        public IEnumerable<InitializerTrial> Tries => tries.AsReadOnly();

        /// <summary>
        /// Gets the initializer status.
        /// </summary>
        public InitializerStatus InitializerStatus { get; internal set; }

        /// <summary>
        /// Adds a trial.
        /// </summary>
        /// <returns>New instance.</returns>
        internal InitializerTrial AddTrial()
        {
            var trial = new InitializerTrial(DateTimeOffset.Now);
            tries.Add(trial);
            return trial;
        }
    }
}
