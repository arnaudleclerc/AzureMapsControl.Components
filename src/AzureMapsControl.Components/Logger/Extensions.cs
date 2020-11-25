namespace AzureMapsControl.Components.Logger
{
    using Microsoft.Extensions.Logging;

    internal enum AzureMapLogEvent
    {
        AzureMap_OnInitialized = 1000,
        AzureMap_OnAfterRenderAsync = 1001,
        AzureMap_AddControlsAsync = 1002,
        AzureMap_ClearSourcesAsync = 1003,
        AzureMap_AddSourceAsync = 1004,
        AzureMap_AttachDataSourcesCallback = 1005,
        AzureMap_RemoveSourceAsync = 1006,
        AzureMap_DataSource_ImportDataFromUrlAsync = 1007,
        AzureMap_DataSource_AddAsync = 1008,
        AzureMap_DataSource_RemoveAsync = 1009,
        AzureMap_DataSource_ClearAsync = 1010,
        AzureMap_DrawingToolbarEvent = 1011,
        AzureMap_AddDrawingToolbarAsync = 1012,
        AzureMap_UpdateDrawingToolbarAsync = 1013,
        AzureMap_RemoveDrawingToolbarAsync = 1014,
        AzureMap_HtmlMarkerEventReceivedAsync = 1015,
        AzureMap_ClearHtmlMarkersAsync = 1016,
        AzureMap_AddHtmlMarkersAsync = 1017,
        AzureMap_RemoveHtmlMarkersAsync = 1018,
        AzureMap_UpdateHtmlMarkersAsync = 1019,
        AzureMap_LayerEventReceivedAsync = 1020,
        AzureMap_ClearLayersAsync = 1021,
        AzureMap_AddLayerAsync = 1022,
        AzureMap_RemoveLayersAsync = 1023,
        AzureMap_MapEventReceivedAsync = 1024,
        AzureMap_SetCameraOptionsAsync = 1025,
        AzureMap_SetStyleOptionsAsync = 1026,
        AzureMap_SetUserInteractionAsync = 1027,
        AzureMap_SetTrafficAsync = 1028,
        AzureMap_ClearMapAsync = 1029,
        AzureMap_AddPopupAsync = 1030,
        AzureMap_Popup_OpenAsync = 1031,
        AzureMap_Popup_CloseAsync = 1032,
        AzureMap_Popup_RemoveAsync = 1033,
        AzureMap_Popup_UpdateAsync = 1034,
        AzureMap_ClearPopupsAsync = 1035,
        AzureMap_PopupEventReceivedAsync = 1036
    }

    internal static class Extensions
    {
        internal static void LogAzureMapsControlTrace(this ILogger logger, AzureMapLogEvent logEvent, string message, params object[] args) => logger.LogAzureMapsControl(LogLevel.Trace, logEvent, message, args);
        internal static void LogAzureMapsControlDebug(this ILogger logger, AzureMapLogEvent logEvent, string message, params object[] args) => logger.LogAzureMapsControl(LogLevel.Debug, logEvent, message, args);
        internal static void LogAzureMapsControlInfo(this ILogger logger, AzureMapLogEvent logEvent, string message, params object[] args) => logger.LogAzureMapsControl(LogLevel.Information, logEvent, message, args);
        internal static void LogAzureMapsControl(this ILogger logger, LogLevel logLevel, AzureMapLogEvent logEvent, string message, params object[] args) => logger.Log(logLevel, new EventId((int)logEvent), message, args);
    }
}
