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
        internal const string MethodAddControl = "addControls";
        internal const string MethodAddHtmlMarkers = "addHtmlMarkers";
        internal const string MethodSetOptions = "setOptions";
        internal const string MethodRemoveHtmlMarkers = "removeHtmlMarkers";
        internal const string MethodUpdateHtmlMarkers = "updateHtmlMarkers";
        internal const string MethodAddDrawingToolbar = "addDrawingToolbar";
        internal const string MethodUpdateDrawingToolbar = "updateDrawingToolbar";
        internal const string MethodRemoveDrawingToolbar = "removeDrawingToolbar";
        internal const string MethodAddLayer = "addLayer";
        internal const string MethodRemoveLayers = "removeLayers";
        internal const string MethodClearLayers = "clearLayers";
        internal const string MethodAddDataSource = "addDataSource";
        internal const string MethodRemoveDataSource = "removeDataSource";
        internal const string MethodClearDataSources = "clearDataSources";
        internal const string MethodDataSourceImportDataFromUrl = "dataSource_importDataFromUrl";
        internal const string MethodDataSourceAdd = "dataSource_add";
    }
}
