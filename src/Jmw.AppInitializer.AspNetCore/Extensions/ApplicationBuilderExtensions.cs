// <copyright file="ApplicationBuilderExtensions.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Microsoft.AspNetCore.Builder;

using System;
using Ardalis.GuardClauses;
using Jmw.AppInitializer;
using Jmw.AppInitializer.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions for <see cref="IApplicationBuilder"/>.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configure and start the <see cref="AppInitializer"/>.
    /// </summary>
    /// <param name="appBuilder">Application builder.</param>
    /// <param name="serviceProvider">Instance of <see cref="IServiceProvider"/>.</param>
    /// <param name="configuration">Optional configuration function.</param>
    /// <typeparam name="TApplicationBuilder">Type of application builder.</typeparam>
    /// <returns>Provided application builder.</returns>
    public static TApplicationBuilder UseAppInitializer<TApplicationBuilder>(
        this TApplicationBuilder appBuilder,
        IServiceProvider serviceProvider,
        Action<AppInitializerConfiguration> configuration = null)
        where TApplicationBuilder : IApplicationBuilder
    {
        Guard.Against.Null(appBuilder);
        Guard.Against.Null(serviceProvider);

        var initializer = serviceProvider.GetRequiredService<AppInitializer>();

        configuration?.Invoke(AppInitializerConfiguration.Instance);

        if (AppInitializerConfiguration.Instance.UseMiddleWare)
        {
            appBuilder.UseMiddleware<AppInitializerMiddleware>();
        }

        Guard.Against.NegativeOrZero(
            AppInitializerConfiguration.Instance.MaxTries);

        initializer.StartInitializer(AppInitializerConfiguration.Instance);

        return appBuilder;
    }
}
