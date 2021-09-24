// <copyright file="ApplicationBuilderExtensions.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Microsoft.AspNetCore.Builder
{
    using System;
    using Dawn;
    using Jmw.AppInitializer;
    using Jmw.AppInitializer.AspNetCore;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extensions for <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure and start the <see cref="AppInitializer"/>.
        /// </summary>
        /// <param name="appBuilder">Application builder.</param>
        /// <param name="serviceProvider">Instance of <see cref="IServiceProvider"/>.</param>
        /// <param name="configuration">Optional configuration function.</param>
        /// <returns>Provided application builder.</returns>
        public static IApplicationBuilder UseAppInitializer(
            this IApplicationBuilder appBuilder,
            IServiceProvider serviceProvider,
            Action<AppInitializerConfiguration> configuration = null)
        {
            Guard.Argument(appBuilder, nameof(appBuilder)).NotNull();
            Guard.Argument(serviceProvider, nameof(serviceProvider)).NotNull();

            var initializer = serviceProvider.GetRequiredService<AppInitializer>();

            if (configuration != null)
            {
                configuration(AppInitializerConfiguration.Instance);
            }

            if (AppInitializerConfiguration.Instance.UseMiddleWare)
            {
                appBuilder.UseMiddleware<AppInitializerMiddleware>();
            }

            Guard.Argument(
                AppInitializerConfiguration.Instance.MaxTries,
                $"{nameof(AppInitializerConfiguration)}.{nameof(AppInitializerConfiguration.MaxTries)}")
                .NotZero();

            initializer.StartInitializer(AppInitializerConfiguration.Instance);

            return appBuilder;
        }
    }
}
