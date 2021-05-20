namespace AzureMapsControl.Components.Indoor
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    public sealed class IndoorManager
    {
        internal readonly string Id;
        private readonly IMapJsRuntime _jsRuntime;
        private readonly ILogger _logger;

        public bool Disposed { get; private set; }

        internal IndoorManager(IMapJsRuntime jsRuntime, ILogger logger)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Adds or updates the indoor styles to the map.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentDisposedException">The component has already been disposed</exception>
        public async ValueTask InitializeAsync()
        {
            _logger.LogAzureMapsControlInfo(AzureMapLogEvent.IndoorManager_InitializeAsync, "IndoorManager - InitializeAsync");
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorManager_InitializeAsync, "Id", Id);

            EnsureNotDisposed();

            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Initialize.ToIndoorNamespace(), Id);
        }

        /// <summary>
        /// Disposes the IndoorManager.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentDisposedException">The component has already been disposed</exception>
        public async ValueTask DisposeAsync()
        {
            _logger.LogAzureMapsControlInfo(AzureMapLogEvent.IndoorManager_DisposeAsync, "IndoorManager - DisposeAsync");
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorManager_DisposeAsync, "Id", Id);

            EnsureNotDisposed();

            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), Id);
            Disposed = true;
        }

        private void EnsureNotDisposed()
        {
            if (Disposed)
            {
                throw new ComponentDisposedException();
            }
        }
    }
}
