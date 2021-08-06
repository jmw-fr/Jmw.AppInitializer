// <copyright file="AppInitializer.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer
{
    using System;
    using Dawn;

    /// <summary>
    /// Initialier manager.
    /// </summary>
    public class AppInitializer
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppInitializer"/> class.
        /// </summary>
        /// <param name="serviceProvider">Instance of <see cref="IServiceProvider"/>.</param>
        internal AppInitializer(IServiceProvider serviceProvider)
        {
            this.serviceProvider = Guard.Argument(serviceProvider, nameof(serviceProvider)).NotNull().Value;
        }

        /// <summary>
        /// Starts the initializer.
        /// </summary>
        /// <param name="configuration">Configuration to apply.</param>
        internal void StartInitializer(AppInitializerConfiguration configuration)
        {
        }
    }
}
