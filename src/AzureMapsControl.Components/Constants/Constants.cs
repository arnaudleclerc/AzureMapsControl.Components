namespace AzureMapsControl.Components.Constants
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal static class JsConstants
    {
        internal static class Namespaces
        {
            internal const string Base = "azureMapsControl";
            internal const string Drawing = "azureMapsControl.drawing";
        }

        internal static class Methods
        {
            internal const string AddMap = "addMap";
            internal const string ClearMap = "clearMap";
            internal const string SetOptions = "setOptions";
            internal const string SetCameraOptions = "setCameraOptions";
            internal const string SetStyleOptions = "setStyleOptions";
            internal const string SetUserInteraction = "setUserInteraction";
            internal const string SetTraffic = "setTraffic";

            internal const string AddControls = "addControls";
            internal const string UpdateControl = "updateControl";

            internal const string AddHtmlMarkers = "addHtmlMarkers";
            internal const string ClearHtmlMarkers = "clearHtmlMarkers";
            internal const string RemoveHtmlMarkers = "removeHtmlMarkers";
            internal const string UpdateHtmlMarkers = "updateHtmlMarkers";

            internal const string AddDrawingToolbar = "addDrawingToolbar";
            internal const string UpdateDrawingToolbar = "updateDrawingToolbar";
            internal const string RemoveDrawingToolbar = "removeDrawingToolbar";

            internal const string AddLayer = "addLayer";
            internal const string RemoveLayers = "removeLayers";
            internal const string ClearLayers = "clearLayers";

            internal const string AddSource = "addSource";
            internal const string RemoveSource = "removeSource";
            internal const string ClearSources = "clearSources";

            internal const string DataSourceImportDataFromUrl = "dataSource_importDataFromUrl";
            internal const string DataSourceAdd = "dataSource_add";
            internal const string DataSourceRemove = "dataSource_remove";
            internal const string DataSourceClear = "dataSource_clear";

            internal const string AddPopup = "addPopup";
            internal const string PopupOpen = "popup_open";
            internal const string PopupClose = "popup_close";
            internal const string PopupRemove = "popup_remove";
            internal const string PopupUpdate = "popup_update";
            internal const string ClearPopups = "clearPopups";
        }
    }
}
