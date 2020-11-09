namespace AzureMapsControl.Components.Map
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Events;

    public sealed class MapEventActivationFlags : EventActivationFlags<MapEventType, MapEventActivationFlags>
    {
        private MapEventActivationFlags(bool defaultFlag)
            : base(new Dictionary<MapEventType, bool>
        {
            { MapEventType.BoxZoomEnd, defaultFlag },
            { MapEventType.BoxZoomStart, defaultFlag },
            { MapEventType.Click, defaultFlag },
            { MapEventType.ContextMenu, defaultFlag },
            { MapEventType.Data, defaultFlag },
            { MapEventType.DblClick, defaultFlag },
            { MapEventType.Drag, defaultFlag },
            { MapEventType.DragEnd, defaultFlag },
            { MapEventType.DragStart, defaultFlag },
            { MapEventType.Error, defaultFlag },
            { MapEventType.Idle, defaultFlag },
            { MapEventType.LayerAdded, defaultFlag },
            { MapEventType.LayerRemoved, defaultFlag },
            { MapEventType.Load, defaultFlag },
            { MapEventType.MouseDown, defaultFlag },
            { MapEventType.MouseMove, defaultFlag },
            { MapEventType.MouseOut, defaultFlag },
            { MapEventType.MouseOver, defaultFlag },
            { MapEventType.MouseUp, defaultFlag },
            { MapEventType.Move, defaultFlag },
            { MapEventType.MoveEnd, defaultFlag },
            { MapEventType.MoveStart, defaultFlag },
            { MapEventType.Pitch, defaultFlag },
            { MapEventType.PitchEnd, defaultFlag },
            { MapEventType.PitchStart, defaultFlag },
            { MapEventType.Ready, defaultFlag },
            { MapEventType.Render, defaultFlag },
            { MapEventType.Resize, defaultFlag },
            { MapEventType.Rotate, defaultFlag },
            { MapEventType.RotateEnd, defaultFlag },
            { MapEventType.RotateStart, defaultFlag },
            { MapEventType.SourceAdded, defaultFlag },
            { MapEventType.SourceDate, defaultFlag },
            { MapEventType.SourceRemoved, defaultFlag },
            { MapEventType.StyleData, defaultFlag },
            { MapEventType.StyleImageMissing, defaultFlag },
            { MapEventType.TokenAcquired, defaultFlag },
            { MapEventType.TouchCancel, defaultFlag },
            { MapEventType.TouchEnd, defaultFlag },
            { MapEventType.TouchMove, defaultFlag },
            { MapEventType.TouchStart, defaultFlag },
            { MapEventType.Wheel, defaultFlag },
            { MapEventType.Zoom, defaultFlag },
            { MapEventType.ZoomEnd, defaultFlag },
            { MapEventType.ZoomStart, defaultFlag }
        })
        { }

        public static MapEventActivationFlags All() => new MapEventActivationFlags(true);
        public static MapEventActivationFlags None() => new MapEventActivationFlags(false);
    }
}
