namespace AzureMapsControl.Components.Indoor
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Indoor.Style;
    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    public delegate void IndoorManagerEvent(IndoorManagerEventArgs eventArgs);

    public sealed class IndoorManager
    {
        internal readonly string Id;
        private readonly IMapJsRuntime _jsRuntime;
        private readonly ILogger _logger;
        private readonly IndoorManagerEventHelper _eventHelper;

        public bool Disposed { get; private set; }

        internal IndoorManagerEventHelper EventHelper => _eventHelper;

        public event IndoorManagerEvent OnFacilityChanged;
        public event IndoorManagerEvent OnLevelChanged;

        internal IndoorManager(IMapJsRuntime jsRuntime, ILogger logger)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
            Id = Guid.NewGuid().ToString();

            _eventHelper = new IndoorManagerEventHelper(DispatchEventAsync);
        }

        /// <summary>
        /// Adds or updates the indoor styles to the map.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ComponentDisposedException">The component has already been disposed</exception>
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
        /// <exception cref="ComponentDisposedException">The component has already been disposed</exception>
        public async ValueTask DisposeAsync()
        {
            _logger.LogAzureMapsControlInfo(AzureMapLogEvent.IndoorManager_DisposeAsync, "IndoorManager - DisposeAsync");
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorManager_DisposeAsync, "Id", Id);

            EnsureNotDisposed();

            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), Id);
            Disposed = true;
        }

        /// <summary>
        /// Retrieves the currently selected facility.
        /// </summary>
        /// <returns>Currently selected facility</returns>
        /// <exception cref="ComponentDisposedException">The component has already been disposed</exception>
        public async ValueTask<(string FacilityId, int LevelOrdinal)> GetCurrentFacilityAsync()
        {
            _logger.LogAzureMapsControlInfo(AzureMapLogEvent.IndoorManager_GetCurrentFacilityAsync, "IndoorManager - GetCurrentFacilityAsync");
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorManager_GetCurrentFacilityAsync, "Id", Id);

            EnsureNotDisposed();

            var currentFacility = await _jsRuntime.InvokeAsync<IndoorFacility>(Constants.JsConstants.Methods.Indoor.GetCurrentFacility.ToIndoorNamespace(), Id);
            return (currentFacility.FacilityId, currentFacility.LevelOrdinal);
        }

        /// <summary>
        /// Gets the options used by the IndoorManager.
        /// </summary>
        /// <returns>Options of the IndoorManager</returns>
        /// <exception cref="ComponentDisposedException">The component has already been disposed</exception>
        public async ValueTask<IndoorManagerOptions> GetOptionsAsync()
        {
            _logger.LogAzureMapsControlInfo(AzureMapLogEvent.IndoorManager_GetOptionsAsync, "IndoorManager - GetOptionsAsync");
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorManager_GetOptionsAsync, "Id", Id);

            EnsureNotDisposed();

            return await _jsRuntime.InvokeAsync<IndoorManagerOptions>(Constants.JsConstants.Methods.Indoor.GetOptions.ToIndoorNamespace(), Id);
        }

        /// <summary>
        /// Retrieves the style definition that is used in this indoor style manager.
        /// </summary>
        /// <returns>Style definition used in this indoor style manager</returns>
        /// <exception cref="ComponentDisposedException">The component has already been disposed</exception>
        public async ValueTask<StyleDefinition> GetStyleDefinitionAsync()
        {
            _logger.LogAzureMapsControlInfo(AzureMapLogEvent.IndoorManager_GetStyleDefinitionAsync, "IndoorManager - GetStyleDefinitionAsync");
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorManager_GetStyleDefinitionAsync, "Id", Id);

            EnsureNotDisposed();

            return await _jsRuntime.InvokeAsync<StyleDefinition>(Constants.JsConstants.Methods.Indoor.GetStyleDefinition.ToIndoorNamespace(), Id);
        }

        /// <summary>
        /// Turn on or off Dynamic Styling.
        /// Turn on Dynamic Styling will involve calls to Get State Tile.
        /// Turn off Dynamic Styling will stop calling Get State Tile.
        /// Requires Initiate Indoor Manager with options that have statesetId value set.
        /// </summary>
        /// <param name="enabled">true to enable Dynamic Styling; false to disable Dynamic Styling.</param>
        /// <returns></returns>
        public async ValueTask SetDynamicStylingAsync(bool enabled)
        {
            _logger.LogAzureMapsControlInfo(AzureMapLogEvent.IndoorManager_SetDynamicStylingAsync, "IndoorManager - SetDynamicStylingAsync");
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorManager_SetDynamicStylingAsync, "Id", Id);
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorManager_SetDynamicStylingAsync, "Enabled", enabled);

            EnsureNotDisposed();

            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.SetDynamicStyling.ToIndoorNamespace(), Id, enabled);
        }

        /// <summary>
        /// Sets the current facility and its ordinal.
        /// If null or an empty string is provided for the facilityId,
        /// it will reset any facility selection and remove the level picker.
        /// </summary>
        /// <param name="facilityId">The facility Id.</param>
        /// <param name="levelOrdinal">The level ordinal number.</param>
        /// <returns></returns>
        public async ValueTask SetFacilityAsync(string facilityId, int levelOrdinal)
        {
            _logger.LogAzureMapsControlInfo(AzureMapLogEvent.IndoorManager_SetFacilityAsync, "IndoorManager - SetFacilityAsync");
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorManager_SetFacilityAsync, "Id", Id);
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorManager_SetFacilityAsync, "FacilityId", facilityId);
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorManager_SetFacilityAsync, "LevelOrdinal", levelOrdinal);

            EnsureNotDisposed();

            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.SetFacility.ToIndoorNamespace(), Id, facilityId, levelOrdinal);
        }

        /// <summary>
        /// Sets the options used by the IndoorManager.
        /// </summary>
        /// <param name="options">The indoor manager options.</param>
        /// <returns></returns>
        public async ValueTask SetOptionsAsync(IndoorManagerOptions options)
        {
            _logger.LogAzureMapsControlInfo(AzureMapLogEvent.IndoorManager_SetOptionsAsync, "IndoorManager - SetOptionsAsync");
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorManager_SetOptionsAsync, "Id", Id);

            EnsureNotDisposed();

            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.SetOptions.ToIndoorNamespace(), Id, options);
        }

        private async ValueTask DispatchEventAsync(IndoorManagerJsEventArgs eventArgs)
        {
            await Task.Run(() => {

                var arguments = new IndoorManagerEventArgs {
                    FacilityId = eventArgs.FacilityId,
                    LevelNumber = eventArgs.LevelNumber,
                    PrevFacilityId = eventArgs.PrevFacilityId,
                    PrevLevelNumber = eventArgs.PrevLevelNumber
                };

                switch (eventArgs.Type)
                {
                    case "facilitychanged":
                        OnFacilityChanged?.Invoke(arguments);
                        break;

                    case "levelchanged":
                        OnLevelChanged?.Invoke(arguments);
                        break;
                }
            });
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
