// <copyright file="AppInitializerController.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer.AspNetCore.Controllers
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using System.Threading;
    using System.Threading.Tasks;
    using Dawn;
    using Jmw.AppInitializer.AspNetCore.DTO;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// App Initializer controller.
    /// </summary>
    [ApiController]
    [Route("appInitializer")]
    public class AppInitializerController : ControllerBase
    {
        private readonly AppInitializer appInitializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppInitializerController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appInitializer">Instance of AppInitializer.</param>
        public AppInitializerController(AppInitializer appInitializer)
        {
            this.appInitializer = Guard.Argument(appInitializer, nameof(appInitializer));
        }

        /// <summary>
        /// Returns the status of the initializer.
        /// </summary>
        /// <returns>Status of the initializer.</returns>
        [HttpGet]
        public AppInitializerStatus Get()
        {
            return new AppInitializerStatus()
            {
                Tries = this.appInitializer.Tries,
                MaxTries = this.appInitializer.MaxTries,
                Begin = this.appInitializer.Begin,
                End = this.appInitializer.End,
                InitializerStatus = this.appInitializer.InitializerStatus,
                Initializers = this.appInitializer.Initializers
                    .Select(h => new InitializerExecution()
                    {
                         InitializerName = h.InitializerName,
                         InitializerStatus = h.InitializerStatus,
                         Trials = h.Tries.Select(t => new InitializerTrial
                         {
                             Begin = t.Begin,
                             End = t.End,
                             Exception = t?.Exception?.Message,
                             InnerException = t?.Exception?.InnerException?.Message,
                         }),
                    }),
            };
        }

        /// <summary>
        /// IIS Warmup function. It waits until the warmup finished intializing.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>No Content.</returns>
        [HttpGet("WarmUp")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetWarmUpAsync(CancellationToken cancellationToken)
        {
            await this.appInitializer
                .InitializerStatusAsObservable
                .FirstAsync(i => i != InitializerStatus.Initializing)
                .ToTask(cancellationToken);

            return Ok();
        }
    }
}
