// <copyright file="FunctionInitializer.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer.Initializers
{
    using System;
    using System.Threading.Tasks;
    using Dawn;

    /// <summary>
    /// Initializer using a provided function to initialize.
    /// </summary>
    internal class FunctionInitializer : IInitializer
    {
        private readonly Func<IServiceProvider, Task> initializerFunction;

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionInitializer"/> class.
        /// </summary>
        /// <param name="initializer">Initializing function.</param>
        public FunctionInitializer(Func<IServiceProvider, Task> initializer)
        {
            initializerFunction = Guard.Argument(initializer, nameof(initializer)).NotNull();
        }

        /// <inheritdoc/>
        public Task InitializeAsync(IServiceProvider serviceProvider)
        {
            return initializerFunction(serviceProvider);
        }
    }
}
