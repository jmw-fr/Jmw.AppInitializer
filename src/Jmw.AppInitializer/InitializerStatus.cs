// <copyright file="InitializerStatus.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.AppInitializer
{
    /// <summary>
    /// Enumerate the different values of the status.
    /// </summary>
    public enum InitializerStatus
    {
        /// <summary>
        /// The initialize has never been run.
        /// </summary>
        NeverRun = 2,

        /// <summary>
        /// The initializer has run with success.
        /// </summary>
        Initialized = 0,

        /// <summary>
        /// The initializer is running.
        /// </summary>
        Initializing = 1,

        /// <summary>
        /// The initializer has run with error.
        /// </summary>
        OnError = -1,
    }
}
