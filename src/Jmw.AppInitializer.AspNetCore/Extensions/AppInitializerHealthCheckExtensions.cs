// <copyright file="AppInitializerHealthCheckExtensions.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Microsoft.Extensions.DependencyInjection
{
    using Jmw.AppInitializer.AspNetCore;

    /// <summary>
    /// HealthCheck extensions.
    /// </summary>
    public static class AppInitializerHealthCheckExtensions
    {
        /// <summary>
        /// Adds then <see cref="AppInitializerHealthCheck"/> to the serviceCollection for using it with ASP.NEt HealthCheck.
        /// </summary>
        /// <param name="healthChecksBuilder">Health Check Builder.</param>
        /// <returns>Modified builder.</returns>
        public static IHealthChecksBuilder AddInitializerHealthCheck(this IHealthChecksBuilder healthChecksBuilder)
        {
            return healthChecksBuilder.AddCheck<AppInitializerHealthCheck>("AppInitializer");
        }
    }
}
