// <copyright file="TypeInitializer.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer.Initializers
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Initializer from DI.
    /// </summary>
    /// <typeparam name="T">Initializer type.</typeparam>
    internal class TypeInitializer<T> : IInitializer
        where T : IInitializer, new()
    {
        /// <inheritdoc/>
        public Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var t = new T();

            return t.InitializeAsync(serviceProvider);
        }
    }
}
