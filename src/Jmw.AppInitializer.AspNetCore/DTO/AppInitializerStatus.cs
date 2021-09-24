// <copyright file="AppInitializerStatus.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer.AspNetCore.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Status of the AppInitializer.
    /// </summary>
    public class AppInitializerStatus
    {
        /// <summary>
        /// Gets the number of initialization tries.
        /// </summary>
        public int Tries { get; internal set; }

        /// <summary>
        /// Gets the number of initialization max tries.
        /// </summary>
        public int MaxTries { get; internal set; }

        /// <summary>
        /// Gets the initializer execution begin date.
        /// </summary>
        public DateTimeOffset Begin { get; internal set; }

        /// <summary>
        /// Gets the initializer execution end date.
        /// </summary>
        public DateTimeOffset? End { get; internal set; }

        /// <summary>
        /// Gets the app initializer overall status.
        /// </summary>
        public InitializerStatus InitializerStatus { get; internal set; }

        /// <summary>
        /// Gets the initializers history.
        /// </summary>
        public IEnumerable<InitializerExecution> Initializers { get; internal set; }
    }
}
