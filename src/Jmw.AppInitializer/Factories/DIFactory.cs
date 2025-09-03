// <copyright file="DIFactory.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer.Factories
{
    using System;
    using Ardalis.GuardClauses;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Dependency Injection factory.
    /// </summary>
    /// <typeparam name="T">Type of class to construct.</typeparam>
    public class DIFactory<T> : Factory
        where T : class, IInitializer
    {
        /// <inheritdoc/>
        public override string InitializerName => typeof(T).Name;

        /// <inheritdoc/>
        public override IInitializer Construct(IServiceProvider serviceProvider)
        {
            Guard.Against.Null(serviceProvider);

            return serviceProvider.GetService<T>();
        }
    }
}
