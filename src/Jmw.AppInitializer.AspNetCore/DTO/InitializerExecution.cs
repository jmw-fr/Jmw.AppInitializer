// <copyright file="InitializerExecution.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer.AspNetCore.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Initializer history.
    /// </summary>
    public class InitializerExecution
    {
        /// <summary>
        /// Gets the initializer name.
        /// </summary>
        public string InitializerName { get; internal set; }

        /// <summary>
        /// Gets the initializer status.
        /// </summary>
        public InitializerStatus InitializerStatus { get; internal set; }

        /// <summary>
        /// Gets the lists of trials for this initializer.
        /// </summary>
        public IEnumerable<InitializerTrial> Trials { get; internal set; }
    }
}
