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
            internal const string Source = "Source";
            internal const string Datasource = "Datasource";
            internal const string GriddedDatasource = "GriddedDatasource";
            internal const string HtmlMarker = "HtmlMarker";
            internal const string OverviewMapControl = "OverviewMapControl";
            internal const string GeolocationControl = "GeolocationControl";
            internal const string FullScreenControl = "FullscreenControl";
            internal const string Indoor = "Indoor";
            internal const string Layer = "Layer";
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
                internal const string Drop = "drop";
                internal const string SetCoordinates = "setCoordinates";
                internal const string Morph = "morph";
                internal const string MoveAlongRoute = "moveAlongRoute";
            }

            internal static class Core
            {
                internal const string AddControls = "addControls";
                internal const string AddHtmlMarkers = "addHtmlMarkers";
                internal const string AddLayer = "addLayer";
                internal const string AddMap = "addMap";
                internal const string AddPopup = "addPopup";
                internal const string AddPopupWithTemplate = "addPopupWithTemplate";
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
                internal const string UpdateHtmlMarkers = "updateHtmlMarkers";
                internal const string CreateImageFromTemplate = "createImageFromTemplate";
                internal const string SetCanvasStyleProperty = "setCanvasStyleProperty";
                internal const string SetCanvasStyleProperties = "setCanvasStyleProperties";
                internal const string SetCanvasContainerStyleProperty = "setCanvasContainerStyleProperty";
                internal const string SetCanvasContainerStyleProperties = "setCanvasContainerStyleProperties";
                internal const string GetCamera = "getCamera";
                internal const string GetStyle = "getStyle";
                internal const string GetTraffic = "getTraffic";
                internal const string GetUserInteraction = "getUserInteraction";
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
                internal const string SetOptions = "setOptions";
                internal const string ApplyTemplate = "applyTemplate";
            }

            internal static class Source
            {
                internal const string AddShapes = "addShapes";
                internal const string AddFeatures = "addFeatures";
                internal const string Clear = "clear";
                internal const string ImportDataFromUrl = "importDataFromUrl";
                internal const string Remove = "remove";
                internal const string Dispose = "dispose";
                internal const string GetOptions = "getOptions";
                internal const string AddFeatureCollection = "addFeatureCollection";
                internal const string SetOptions = "setOptions";
            }

            internal static class Datasource
            {
                internal const string GetShapes = "getShapes";
            }

            internal static class GriddedDatasource
            {
                internal const string GetCellChildren = "getCellChildren";
                internal const string GetGridCells = "getGridCells";
                internal const string GetPoints = "getPoints";
                internal const string SetFeatureCollectionPoints = "setFeatureCollectionPoints";
            }

            internal static class HtmlMarker
            {
                internal const string TogglePopup = "togglePopup";
            }

            internal static class OverviewMapControl
            {
                internal const string SetOptions = "setOptions";
            }

            internal static class GeolocationControl
            {
                internal const string GetLastKnownPosition = "getLastKnownPosition";
                internal const string IsGeolocationSupported = "isGeolocationSupported";
                internal const string Dispose = "dispose";
                internal const string SetOptions = "setOptions";
                internal const string AddEvents = "addEvents";
            }

            internal static class FullScreenControl
            {
                internal const string IsFullScreenSupported = "isFullscreenSupported";
                internal const string Dispose = "dispose";
                internal const string SetOptions = "setOptions";
                internal const string IsFullScreen = "isFullscreen";
                internal const string AddEvents = "addEvents";
            }

            internal static class Indoor
            {
                internal const string CreateIndoorManager = "createIndoorManager";
                internal const string Initialize = "initialize";
                internal const string Dispose = "dispose";
                internal const string GetCurrentFacility = "getCurrentFacility";
                internal const string GetOptions = "getOptions";
                internal const string GetStyleDefinition = "getStyleDefinition";
                internal const string SetDynamicStyling = "setDynamicStyling";
                internal const string SetFacility = "setFacility";
                internal const string SetOptions = "setOptions";
            }

            internal static class Layer
            {
                internal const string SetOptions = "setOptions";
            }
        }
    }
}
