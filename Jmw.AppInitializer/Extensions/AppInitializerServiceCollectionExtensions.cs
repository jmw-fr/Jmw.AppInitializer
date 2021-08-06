// <copyright file="AppInitializerServiceCollectionExtensions.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Threading.Tasks;
    using Dawn;
    using Jmw.AppInitializer;
    using Jmw.AppInitializer.Initializers;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extenions for configuring DI.
    /// </summary>
    public static class AppInitializerServiceCollectionExtensions
    {
        /// <summary>
        /// Adds an new initializer.
        /// </summary>
        /// <param name="services">DI service collection.</param>
        /// <param name="initializer">Instance of the initializer to add.</param>
        /// <returns>Modified DI service collection.</returns>
        public static IServiceCollection AddInitializer(this IServiceCollection services, IInitializer initializer)
        {
            Guard.Argument(services, nameof(services)).NotNull();
            Guard.Argument(initializer, nameof(initializer)).NotNull();

            services
                .AddAppInitializer()
                .AddInitializer(initializer);

            return services;
        }

        /// <summary>
        /// Adds an new initializer from a class type.
        /// </summary>
        /// <param name="services">DI service collection.</param>
        /// <returns>Modified DI service collection.</returns>
        /// <typeparam name="T">Type of the initializer.</typeparam>
        public static IServiceCollection AddInitializer<T>(this IServiceCollection services)
            where T : IInitializer, new()
        {
            Guard.Argument(services, nameof(services)).NotNull();

            services
                .AddAppInitializer()
                .AddInitializer(new TypeInitializer<T>());

            return services;
        }

        /// <summary>
        /// Adds an new initializer from a function.
        /// </summary>
        /// <param name="services">DI service collection.</param>
        /// <param name="initializer">Initialier function.</param>
        /// <returns>Modified DI service collection.</returns>
        public static IServiceCollection AddInitializer(this IServiceCollection services, Func<IServiceProvider, Task> initializer)
        {
            Guard.Argument(services, nameof(services)).NotNull();

            services
                .AddAppInitializer()
                .AddInitializer(new FunctionInitializer(initializer));

            return services;
        }

        private static AppInitializerConfiguration AddAppInitializer(this IServiceCollection services)
        {
            services
                .AddSingleton<AppInitializer>()
                .AddSingleton(s => AppInitializerConfiguration.Instance);

            return AppInitializerConfiguration.Instance;
        }
    }
}
