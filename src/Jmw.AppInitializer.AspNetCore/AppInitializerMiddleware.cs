// <copyright file="AppInitializerMiddleware.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer.AspNetCore
{
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Ardalis.GuardClauses;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Middleware returning http 503 status if initializer is not finished.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="AppInitializerMiddleware"/> class.
    /// </remarks>
    /// <param name="next">Next delegate.</param>
    public partial class AppInitializerMiddleware(
        RequestDelegate next)
    {
        private readonly RequestDelegate next = Guard.Against.Null(next);

        /// <summary>
        /// Processes a request.
        /// </summary>
        /// <param name="httpContext">Http context.</param>
        /// <param name="appInitializer">Instance of AppInitializer.</param>
        /// <returns>Async task.</returns>
        public async Task Invoke(HttpContext httpContext, AppInitializer appInitializer)
        {
            var path = httpContext.Request.Path.Value;

            if (appInitializer != null && !RegExpAppInitializer().IsMatch(path))
            {
                var initializerStatus = await appInitializer
                    .InitializerStatusAsObservable
                    .FirstAsync(i => i != InitializerStatus.Initializing)
                    .ToTask(httpContext.RequestAborted);

                switch (initializerStatus)
                {
                    case InitializerStatus.OnError:
                        httpContext.Response.StatusCode = 503;
                        httpContext.Response.ContentType = "text/plain; charset=utf-8";
                        await httpContext.Response.WriteAsync("AppInitializer failed.");
                        break;

                    case InitializerStatus.NeverRun:
                        httpContext.Response.StatusCode = 503;
                        httpContext.Response.ContentType = "text/plain; charset=utf-8";
                        await httpContext.Response.WriteAsync("AppInitializer not run yet.");
                        break;

                    default:
                        await next(httpContext);
                        break;
                }
            }
            else
            {
                await next(httpContext);
            }
        }

        [GeneratedRegex("^/appInitializer")]
        private static partial Regex RegExpAppInitializer();
    }
}
