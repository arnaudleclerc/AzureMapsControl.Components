namespace AzureMapsControl.Map
{
    using System.Collections.Generic;
    using System.Linq;

    public sealed class MapEventActivationFlags
    {
        private readonly Dictionary<string, bool> _eventsFlags;

        public static MapEventActivationFlags All = new MapEventActivationFlags(true);
        public static MapEventActivationFlags None = new MapEventActivationFlags(false);

        internal IEnumerable<string> EnabledEvents => _eventsFlags.Where(kvp => kvp.Value).Select(kvp => kvp.Key);

        private MapEventActivationFlags(bool defaultFlag) => _eventsFlags = new Dictionary<string, bool>
        {
            { MapEventType.BoxZoomEnd.ToString(), defaultFlag },
            { MapEventType.BoxZoomStart.ToString(), defaultFlag },
            { MapEventType.Click.ToString(), defaultFlag },
            { MapEventType.Data.ToString(), defaultFlag },
            { MapEventType.DblClick.ToString(), defaultFlag },
            { MapEventType.Drag.ToString(), defaultFlag },
            { MapEventType.DragEnd.ToString(), defaultFlag },
            { MapEventType.DragStart.ToString(), defaultFlag },
            { MapEventType.Error.ToString(), defaultFlag },
            { MapEventType.Idle.ToString(), defaultFlag },
            { MapEventType.LayerAdded.ToString(), defaultFlag },
            { MapEventType.LayerRemoved.ToString(), defaultFlag },
            { MapEventType.MouseDown.ToString(), defaultFlag },
            { MapEventType.MouseMove.ToString(), defaultFlag },
            { MapEventType.MouseOut.ToString(), defaultFlag },
            { MapEventType.MouseOver.ToString(), defaultFlag },
            { MapEventType.MouseUp.ToString(), defaultFlag },
            { MapEventType.Move.ToString(), defaultFlag },
            { MapEventType.MoveEnd.ToString(), defaultFlag },
            { MapEventType.MoveStart.ToString(), defaultFlag },
            { MapEventType.Pitch.ToString(), defaultFlag },
            { MapEventType.PitchEnd.ToString(), defaultFlag },
            { MapEventType.PitchStart.ToString(), defaultFlag },
            { MapEventType.Ready.ToString(), defaultFlag },
            { MapEventType.Render.ToString(), defaultFlag },
            { MapEventType.Resize.ToString(), defaultFlag },
            { MapEventType.Rotate.ToString(), defaultFlag },
            { MapEventType.RotateEnd.ToString(), defaultFlag },
            { MapEventType.RotateStart.ToString(), defaultFlag },
            { MapEventType.SourceAdded.ToString(), defaultFlag },
            { MapEventType.SourceDate.ToString(), defaultFlag },
            { MapEventType.SourceRemoved.ToString(), defaultFlag },
            { MapEventType.StyleData.ToString(), defaultFlag },
            { MapEventType.StyleImageMissing.ToString(), defaultFlag },
            { MapEventType.TokenAcquired.ToString(), defaultFlag },
            { MapEventType.TouchCancel.ToString(), defaultFlag },
            { MapEventType.TouchEnd.ToString(), defaultFlag },
            { MapEventType.TouchMove.ToString(), defaultFlag },
            { MapEventType.TouchStart.ToString(), defaultFlag },
            { MapEventType.Wheel.ToString(), defaultFlag },
            { MapEventType.Zoom.ToString(), defaultFlag },
            { MapEventType.ZoomEnd.ToString(), defaultFlag },
            { MapEventType.ZoomStart.ToString(), defaultFlag }
        };

        public MapEventActivationFlags Enable(params MapEventType[] eventTypes)
        {
            if(eventTypes != null)
            {
                foreach(var eventType in eventTypes)
                {
                    _eventsFlags[eventType.ToString()] = true;
                }
            }
            return this;
        }

        public MapEventActivationFlags Disable(params MapEventType[] eventTypes)
        {
            if (eventTypes != null)
            {
                foreach (var eventType in eventTypes)
                {
                    _eventsFlags[eventType.ToString()] = false;
                }
            }
            return this;
        }

    }
}
