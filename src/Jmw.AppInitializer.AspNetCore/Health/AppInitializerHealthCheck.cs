// <copyright file="AppInitializerHealthCheck.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer.AspNetCore
{
    using System.Threading;
    using System.Threading.Tasks;
    using Ardalis.GuardClauses;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    /// <summary>
    /// Health check for AppInitializer. Reports Unhealthy only if init failed.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="AppInitializerHealthCheck"/> class.
    /// </remarks>
    /// <param name="appInitializer">Instance of the AppInitializer.</param>
    public class AppInitializerHealthCheck(AppInitializer appInitializer)
        : IHealthCheck
    {
        private readonly AppInitializer appInitializer = Guard.Against.Null(appInitializer);

        /// <inheritdoc/>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return this.appInitializer.InitializerStatus switch
            {
                InitializerStatus.NeverRun or InitializerStatus.Initializing => Task.FromResult(HealthCheckResult.Degraded()),
                InitializerStatus.Initialized => Task.FromResult(HealthCheckResult.Healthy()),
                _ => Task.FromResult(HealthCheckResult.Unhealthy()),
            };
        }
    }
}
