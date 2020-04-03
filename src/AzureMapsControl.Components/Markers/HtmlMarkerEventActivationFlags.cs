namespace AzureMapsControl.Components.Markers
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Events;

    public sealed class HtmlMarkerEventActivationFlags : EventActivationFlags<HtmlMarkerEventType, HtmlMarkerEventActivationFlags>
    {
        public static HtmlMarkerEventActivationFlags All = new HtmlMarkerEventActivationFlags(true);
        public static HtmlMarkerEventActivationFlags None = new HtmlMarkerEventActivationFlags(false);

        private HtmlMarkerEventActivationFlags(bool defaultFlag) :
            base
            (
                new Dictionary<HtmlMarkerEventType, bool>
                {
                    { HtmlMarkerEventType.Click, defaultFlag },
                    { HtmlMarkerEventType.ContextMenu, defaultFlag },
                    { HtmlMarkerEventType.DblClick, defaultFlag },
                    { HtmlMarkerEventType.Drag, defaultFlag },
                    { HtmlMarkerEventType.DragEnd, defaultFlag },
                    { HtmlMarkerEventType.DragStart, defaultFlag },
                    { HtmlMarkerEventType.KeyDown, defaultFlag },
                    { HtmlMarkerEventType.KeyPress, defaultFlag },
                    { HtmlMarkerEventType.KeyUp, defaultFlag },
                    { HtmlMarkerEventType.MouseDown, defaultFlag },
                    { HtmlMarkerEventType.MouseEnter, defaultFlag },
                    { HtmlMarkerEventType.MouseLeave, defaultFlag },
                    { HtmlMarkerEventType.MouseMove, defaultFlag },
                    { HtmlMarkerEventType.MouseOut, defaultFlag },
                    { HtmlMarkerEventType.MouseOver, defaultFlag },
                    { HtmlMarkerEventType.MouseUp, defaultFlag }
                }
            )
        { }
    }
}
