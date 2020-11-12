namespace AzureMapsControl.Components.Constants
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal static class JsConstants
    {
        /// <summary>
        /// JsInterop namespace
        /// </summary>
        internal const string Namespace = "azureMapsControl";

        internal const string MethodAddMap = "addMap";
        internal const string MethodClearMap = "clearMap";
        internal const string MethodSetOptions = "setOptions";

        internal const string MethodAddControl = "addControls";

        internal const string MethodAddHtmlMarkers = "addHtmlMarkers";
        internal const string MethodClearHtmlMarkers = "clearHtmlMarkers";
        internal const string MethodRemoveHtmlMarkers = "removeHtmlMarkers";
        internal const string MethodUpdateHtmlMarkers = "updateHtmlMarkers";

        internal const string MethodAddDrawingToolbar = "addDrawingToolbar";
        internal const string MethodUpdateDrawingToolbar = "updateDrawingToolbar";
        internal const string MethodRemoveDrawingToolbar = "removeDrawingToolbar";

        internal const string MethodAddLayer = "addLayer";
        internal const string MethodRemoveLayers = "removeLayers";
        internal const string MethodClearLayers = "clearLayers";

        internal const string MethodAddSource = "addSource";
        internal const string MethodRemoveSource = "removeSource";
        internal const string MethodClearSources = "clearSources";

        internal const string MethodDataSourceImportDataFromUrl = "dataSource_importDataFromUrl";
        internal const string MethodDataSourceAdd = "dataSource_add";
        internal const string MethodDataSourceRemove = "dataSource_remove";
        internal const string MethodDataSourceClear = "dataSource_clear";

        internal const string MethodAddPopup = "addPopup";
        internal const string MethodPopupOpen = "popup_open";
        internal const string MethodPopupClose = "popup_close";
        internal const string MethodPopupRemove = "popup_remove";
        internal const string MethodPopupUpdate = "popup_update";
        internal const string MethodClearPopups = "clearPopups";
    }
}
