// <copyright file="Factory.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer.Factories
{
    using System;

    /// <summary>
    /// Abstract factory base.
    /// </summary>
    public abstract class Factory
    {
        /// <summary>
        /// Gets the initializer name.
        /// </summary>
        public abstract string InitializerName { get; }

        /// <summary>
        /// Construct an instance implementing <see cref="IInitializer"/>.
        /// </summary>
        /// <param name="serviceProvider">Scoped service provider.</param>
        /// <returns>Instance implementing <see cref="IInitializer"/>.</returns>
        public abstract IInitializer Construct(IServiceProvider serviceProvider);
    }
}
