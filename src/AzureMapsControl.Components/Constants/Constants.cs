namespace AzureMapsControl.Components.Constants
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal static class JsConstants
    {
        internal static class Namespaces
        {
            internal const string Animation = "Animation";
            internal const string AzureMapsControl = "azureMapsControl";
            internal const string Core = "Core";
            internal const string Drawing = "Drawing";
            internal const string Popup = "Popup";
            internal const string Source = "Datasource";
        }

        internal static class Methods
        {
            internal static class Animation
            {
                internal const string Dispose = "dispose";
                internal const string Pause = "pause";
                internal const string Play = "play";
                internal const string Reset = "reset";
                internal const string Seek = "seek";
                internal const string Stop = "stop";
                internal const string SetOptions = "setOptions";
                internal const string Snakeline = "snakeline";
                internal const string MoveAlongPath = "moveAlongPath";
                internal const string FlowingDashedLine = "flowingDashedLine";
                internal const string DropMarkers = "dropMarkers";
                internal const string GroupAnimations = "groupAnimations";
            }

            internal static class Core
            {
                internal const string AddControls = "addControls";
                internal const string AddHtmlMarkers = "addHtmlMarkers";
                internal const string AddLayer = "addLayer";
                internal const string AddMap = "addMap";
                internal const string AddPopup = "addPopup";
                internal const string AddSource = "addSource";
                internal const string ClearHtmlMarkers = "clearHtmlMarkers";
                internal const string ClearLayers = "clearLayers";
                internal const string ClearMap = "clearMap";
                internal const string ClearPopups = "clearPopups";
                internal const string ClearSources = "clearSources";
                internal const string RemoveHtmlMarkers = "removeHtmlMarkers";
                internal const string RemoveLayers = "removeLayers";
                internal const string RemoveSource = "removeSource";
                internal const string SetCameraOptions = "setCameraOptions";
                internal const string SetOptions = "setOptions";
                internal const string SetStyleOptions = "setStyleOptions";
                internal const string SetTraffic = "setTraffic";
                internal const string SetUserInteraction = "setUserInteraction";
                internal const string UpdateControl = "updateControl";
                internal const string UpdateHtmlMarkers = "updateHtmlMarkers";
                internal const string CreateImageFromTemplate = "createImageFromTemplate";
            }

            internal static class Drawing
            {
                internal const string AddDrawingToolbar = "addDrawingToolbar";
                internal const string RemoveDrawingToolbar = "removeDrawingToolbar";
                internal const string UpdateDrawingToolbar = "updateDrawingToolbar";
            }

            internal static class Popup
            {
                internal const string Close = "close";
                internal const string Open = "open";
                internal const string Remove = "remove";
                internal const string Update = "update";
            }

            internal static class Source
            {
                internal const string Add = "add";
                internal const string Clear = "clear";
                internal const string ImportDataFromUrl = "importDataFromUrl";
                internal const string Remove = "remove";
            }
        }
    }
}
