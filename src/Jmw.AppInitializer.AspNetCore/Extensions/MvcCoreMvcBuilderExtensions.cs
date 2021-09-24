// <copyright file="MvcCoreMvcBuilderExtensions.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Microsoft.Extensions.DependencyInjection
{
    using Dawn;
    using Jmw.AppInitializer.AspNetCore.Controllers;

    /// <summary>
    /// Extensions to MvcBuilder.
    /// </summary>
    public static class MvcCoreMvcBuilderExtensions
    {
        /// <summary>
        /// Adds the AppInitializer to as a part of MVC for using Appinitializer controllers.
        /// </summary>
        /// <param name="builder">MVC Builder.</param>
        /// <returns>Modified MVC Builder.</returns>
        public static IMvcBuilder AddAppInitializerPart(this IMvcBuilder builder)
        {
            Guard.Argument(builder, nameof(builder)).NotNull();

            builder.AddApplicationPart(typeof(AppInitializerController).Assembly);

            return builder;
        }
    }
}
