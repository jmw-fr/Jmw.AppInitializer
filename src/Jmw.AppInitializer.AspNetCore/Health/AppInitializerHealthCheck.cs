// <copyright file="AppInitializerHealthCheck.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer.AspNetCore
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dawn;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    /// <summary>
    /// Health check for AppInitializer. Reports Unhealthy only if init failed.
    /// </summary>
    public class AppInitializerHealthCheck : IHealthCheck
    {
        private readonly AppInitializer appInitializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppInitializerHealthCheck"/> class.
        /// </summary>
        /// <param name="appInitializer">Instance of the AppInitializer.</param>
        public AppInitializerHealthCheck(AppInitializer appInitializer)
        {
            this.appInitializer = Guard.Argument(appInitializer, nameof(appInitializer)).NotNull();
        }

        /// <inheritdoc/>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            switch (this.appInitializer.InitializerStatus)
            {
                case InitializerStatus.NeverRun:
                case InitializerStatus.Initializing:
                case InitializerStatus.Initialized:
                    return Task.FromResult(HealthCheckResult.Healthy());
                default:
                    return Task.FromResult(HealthCheckResult.Unhealthy());
            }
        }
    }
}
