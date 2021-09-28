// <copyright file="InstanceFactory.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer.Factories
{
    using System;
    using Dawn;

    /// <summary>
    /// Instance Factory.
    /// </summary>
    public class InstanceFactory : Factory
    {
        private readonly IInitializer initializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceFactory"/> class.
        /// </summary>
        /// <param name="initializer">Initializer instance.</param>
        internal InstanceFactory(IInitializer initializer)
        {
            this.initializer = Guard.Argument(initializer, nameof(initializer)).NotNull().Value;
        }

        /// <inheritdoc/>
        public override string InitializerName => initializer.GetType().Name;

        /// <inheritdoc/>
        public override IInitializer Construct(IServiceProvider serviceProvider)
        {
            return initializer;
        }
    }
}
