// <copyright file="AppInitializer.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Threading.Tasks;
    using Dawn;
    using Jmw.AppInitializer.Factories;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Initialier manager.
    /// </summary>
    public class AppInitializer
    {
        private readonly IServiceProvider serviceProvider;

        private readonly List<InitializerHistory> appInitializerHistories;

        private readonly BehaviorSubject<InitializerStatus> statut;

        private IDisposable initTimer;

        private AppInitializerConfiguration appInitializerConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppInitializer"/> class.
        /// </summary>
        /// <param name="serviceProvider">Instance of <see cref="IServiceProvider"/>.</param>
        internal AppInitializer(IServiceProvider serviceProvider)
        {
            this.serviceProvider = Guard.Argument(serviceProvider, nameof(serviceProvider)).NotNull().Value;
            appInitializerHistories = new List<InitializerHistory>();
            statut = new BehaviorSubject<InitializerStatus>(InitializerStatus.NeverRun);
        }

        /// <summary>
        /// Gets the number of initialization tries.
        /// </summary>
        public int Tries { get; private set; }

        /// <summary>
        /// Gets the number of initialization tries.
        /// </summary>
        public int MaxTries => appInitializerConfiguration?.MaxTries ?? -1;

        /// <summary>
        /// Gets the initializer execution begin date.
        /// </summary>
        public DateTimeOffset Begin { get; private set; }

        /// <summary>
        /// Gets the initializer execution end date.
        /// </summary>
        public DateTimeOffset? End { get; private set; }

        /// <summary>
        /// Gets the initializer status.
        /// </summary>
        public InitializerStatus InitializerStatus => statut.Value;

        /// <summary>
        /// Gets the initializer status as an <see cref="IObservable{T}"/>.
        /// </summary>
        public IObservable<InitializerStatus> InitializerStatusAsObservable => statut;

        /// <summary>
        /// Gets the initializers history.
        /// </summary>
        public IEnumerable<InitializerHistory> Initializers => appInitializerHistories.AsReadOnly();

        /// <summary>
        /// Starts the initializer.
        /// </summary>
        /// <param name="configuration">Configuration to apply.</param>
        public void StartInitializer(AppInitializerConfiguration configuration)
        {
            Guard.Argument(configuration, nameof(configuration)).NotNull();

            initTimer?.Dispose();
            initTimer = null;

            appInitializerConfiguration = configuration;
            Tries = 0;

            appInitializerHistories.Clear();
            appInitializerHistories.AddRange(serviceProvider
                .GetServices<Factory>()
                .Select(factory => new InitializerHistory(factory)));

            statut.OnNext(InitializerStatus.Initializing);

            Begin = DateTimeOffset.Now;
            initTimer = Observable
                .Interval(configuration.Interval)
                .Subscribe(s => OnRunInitializersAsync().Wait());
        }

        private async Task OnRunInitializersAsync()
        {
            try
            {
                bool failed = false;
                Tries++;

                foreach (var initializerHistory in appInitializerHistories
                    .Where(s => s.InitializerStatus != InitializerStatus.Initialized))
                {
                    initializerHistory.InitializerStatus = InitializerStatus.Initializing;

                    var trial = initializerHistory.AddTrial();

                    try
                    {
                        using (var scope = serviceProvider.CreateScope())
                        {
                            var initializer = initializerHistory.Factory.Construct(scope.ServiceProvider)
                                ?? throw new InvalidOperationException($"Factory {initializerHistory.Factory.GetType().Name} Failed to construct initializer");

                            await initializer.InitializeAsync();

                            initializerHistory.InitializerStatus = InitializerStatus.Initialized;

                            trial.Executed();
                        }
                    }
                    catch (Exception ex)
                    {
                        trial.ExecutedWithException(ex);
                        initializerHistory.InitializerStatus = InitializerStatus.OnError;

                        failed = true;
                    }
                }

                if (!failed)
                {
                    End = DateTimeOffset.Now;
                    statut.OnNext(InitializerStatus.Initialized);
                    initTimer?.Dispose();
                }
                else if (this.appInitializerConfiguration.MaxTries > 0 && Tries >= this.appInitializerConfiguration.MaxTries)
                {
                    End = DateTimeOffset.Now;
                    statut.OnNext(InitializerStatus.OnError);
                    initTimer?.Dispose();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
