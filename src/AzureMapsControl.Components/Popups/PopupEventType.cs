namespace AzureMapsControl.Components.Popups
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Events;

    [ExcludeFromCodeCoverage]
    public sealed class PopupEventType : AtlasEventType
    {
        public static PopupEventType Close = new PopupEventType("close");
        public static PopupEventType Drag = new PopupEventType("drag");
        public static PopupEventType DragEnd = new PopupEventType("dragend");
        public static PopupEventType DragStart = new PopupEventType("dragstart");
        public static PopupEventType Open = new PopupEventType("open");

        private PopupEventType(string type) : base(type) { }
    }
}
