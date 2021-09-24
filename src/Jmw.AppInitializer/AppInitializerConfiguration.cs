// <copyright file="AppInitializerConfiguration.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer
{
    using System;

    /// <summary>
    /// Configuration class for <see cref="AppInitializer"/>.
    /// </summary>
    public sealed class AppInitializerConfiguration
    {
        /// <summary>
        /// Gets the instance of the configuration.
        /// </summary>
        public static AppInitializerConfiguration Instance { get; } = new AppInitializerConfiguration();

        /// <summary>
        /// Gets or sets the number of initialization. If negative, infinite retries.
        /// </summary>
        public int MaxTries { get; set; } = -1;

        /// <summary>
        /// Gets or sets the interval between each retry.
        /// </summary>
        public TimeSpan Interval { get; set; } = new TimeSpan(0, 0, 1);

        /// <summary>
        /// Gets or sets a value indicating whether we use the middleware to return 503 when initializing.
        /// </summary>
        public bool UseMiddleWare { get; set; } = true;
    }
}
