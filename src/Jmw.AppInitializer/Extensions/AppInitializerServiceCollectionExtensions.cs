// <copyright file="AppInitializerServiceCollectionExtensions.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Microsoft.Extensions.DependencyInjection
{
    using Dawn;
    using Jmw.AppInitializer;

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

            return services
                .AddSingleton(initializer)
                .AddAppInitializer();
        }

        /// <summary>
        /// Adds an new initializer from a class type.
        /// </summary>
        /// <param name="services">DI service collection.</param>
        /// <returns>Modified DI service collection.</returns>
        /// <typeparam name="T">Type of the initializer.</typeparam>
        public static IServiceCollection AddInitializer<T>(this IServiceCollection services)
            where T : class, IInitializer
        {
            Guard.Argument(services, nameof(services)).NotNull();

            return services
                .AddTransient<IInitializer, T>()
                .AddAppInitializer();
        }

        private static IServiceCollection AddAppInitializer(this IServiceCollection services)
        {
            return services
                .AddSingleton(s => new AppInitializer(s))
                .AddTransient(s => AppInitializerConfiguration.Instance);
        }
    }
}
