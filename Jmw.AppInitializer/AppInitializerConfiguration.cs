// <copyright file="AppInitializerConfiguration.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer
{
    using System;
    using Dawn;

    /// <summary>
    /// Configuration class for <see cref="AppInitializer"/>.
    /// </summary>
    public sealed class AppInitializerConfiguration
    {
        /// <summary>
        /// Gets or sets the number of retries. If negative, infinite retries.
        /// </summary>
        public int Retries { get; set; } = -1;

        /// <summary>
        /// Gets or sets the interval between each retry.
        /// </summary>
        public TimeSpan Interval { get; set; } = new TimeSpan(0, 0, 1);

        /// <summary>
        /// Gets the instance of the configuration.
        /// </summary>
        internal static AppInitializerConfiguration Instance { get; } = new AppInitializerConfiguration();

        /// <summary>
        /// Adds a new initializer instance.
        /// </summary>
        /// <param name="initializer">Initializer to add.</param>
        internal void AddInitializer(IInitializer initializer)
        {
            Guard.Argument(initializer, nameof(initializer)).NotNull();
        }
    }
}
