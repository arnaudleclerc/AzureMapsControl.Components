namespace AzureMapsControl.Components.FullScreen
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Events;

    [ExcludeFromCodeCoverage]
    public sealed class FullScreenEventType : AtlasEventType
    {
        public static readonly FullScreenEventType FullScreenChanged = new FullScreenEventType("fullscreenchanged");
        private FullScreenEventType(string type) : base(type) { }
    }
}
