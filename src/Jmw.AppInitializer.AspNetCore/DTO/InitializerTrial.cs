// <copyright file="InitializerTrial.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer.AspNetCore.DTO
{
    using System;

    /// <summary>
    /// Initializer trial.
    /// </summary>
    public class InitializerTrial
    {
        /// <summary>
        /// Gets the initializer execution begin date.
        /// </summary>
        public DateTimeOffset Begin { get; internal set; }

        /// <summary>
        /// Gets the initializer execution end date.
        /// </summary>
        public DateTimeOffset? End { get; internal set; }

        /// <summary>
        /// Gets the initializer exception's message thrown if any.
        /// </summary>
        public string Exception { get; internal set; }

        /// <summary>
        /// Gets the initializer inner exception's message thrown if any.
        /// </summary>
        public string InnerException { get; internal set; }
    }
}
