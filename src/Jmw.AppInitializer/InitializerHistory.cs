// <copyright file="InitializerHistory.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer
{
    using System;
    using System.Collections.Generic;
    using Dawn;
    using Jmw.AppInitializer.Factories;

    /// <summary>
    /// History of an initializer.
    /// </summary>
    public class InitializerHistory
    {
        private readonly List<InitializerTrial> tries;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitializerHistory"/> class.
        /// </summary>
        /// <param name="factory">Initializer factory.</param>
        internal InitializerHistory(Factory factory)
        {
            Factory = Guard.Argument(factory, nameof(factory)).NotNull().Value;
            InitializerStatus = InitializerStatus.NeverRun;
            tries = new List<InitializerTrial>();
        }

        /// <summary>
        /// Gets the list of initialization tries.
        /// </summary>
        public IEnumerable<InitializerTrial> Tries => tries.AsReadOnly();

        /// <summary>
        /// Gets the initializer status.
        /// </summary>
        public InitializerStatus InitializerStatus { get; internal set; }

        /// <summary>
        /// Gets the initializer name.
        /// </summary>
        public string InitializerName => Factory.InitializerName;

        /// <summary>
        /// Gets the initializer factory.
        /// </summary>
        internal Factory Factory { get; }

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
