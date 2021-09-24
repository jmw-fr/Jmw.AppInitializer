// <copyright file="AppInitializerMiddleware.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer.AspNetCore
{
    using System.Threading.Tasks;
    using Dawn;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Middleware returning http 503 status if initializer is not finished.
    /// </summary>
    public class AppInitializerMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppInitializerMiddleware"/> class.
        /// </summary>
        /// <param name="next">Next delegate.</param>
        public AppInitializerMiddleware(
            RequestDelegate next)
        {
            this.next = Guard.Argument(next, nameof(next)).NotNull();
        }

        /// <summary>
        /// Processes a request.
        /// </summary>
        /// <param name="context">Http context.</param>
        /// <param name="appInitializer">Instance of AppInitializer.</param>
        /// <returns>Async task.</returns>
        public async Task Invoke(HttpContext context, AppInitializer appInitializer)
        {
            if (appInitializer != null)
            {
                switch (appInitializer.InitializerStatus)
                {
                    case InitializerStatus.OnError:
                        context.Response.StatusCode = 503;
                        context.Response.ContentType = "text/plain; charset=utf-8";
                        await context.Response.WriteAsync("AppInitializer failed.");
                        break;

                    case InitializerStatus.NeverRun:
                        context.Response.StatusCode = 503;
                        context.Response.ContentType = "text/plain; charset=utf-8";
                        await context.Response.WriteAsync("AppInitializer not run yet.");
                        break;

                    case InitializerStatus.Initializing:
                        context.Response.StatusCode = 503;
                        context.Response.ContentType = "text/plain; charset=utf-8";
                        await context.Response.WriteAsync("AppInitializer initializing.");
                        break;

                    default:
                        await next(context);
                        break;
                }
            }
            else
            {
                await next(context);
            }
        }
    }
}
