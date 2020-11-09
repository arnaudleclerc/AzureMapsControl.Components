namespace AzureMapsControl.Components.Layers
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Events;

    public sealed class LayerEventActivationFlags : EventActivationFlags<LayerEventType, LayerEventActivationFlags>
    {
        private LayerEventActivationFlags(bool defaultFlag) :
            base
            (
                new Dictionary<LayerEventType, bool>
                {
                    { LayerEventType.Click, defaultFlag },
                    { LayerEventType.ContextMenu, defaultFlag },
                    { LayerEventType.DblClick, defaultFlag },
                    { LayerEventType.LayerAdded, defaultFlag },
                    { LayerEventType.LayerRemoved, defaultFlag },
                    { LayerEventType.MouseDown, defaultFlag },
                    { LayerEventType.MouseEnter, defaultFlag },
                    { LayerEventType.MouseLeave, defaultFlag },
                    { LayerEventType.MouseMove, defaultFlag },
                    { LayerEventType.MouseOut, defaultFlag },
                    { LayerEventType.MouseOver, defaultFlag },
                    { LayerEventType.MouseUp, defaultFlag },
                    { LayerEventType.TouchCancel, defaultFlag },
                    { LayerEventType.TouchEnd, defaultFlag },
                    { LayerEventType.TouchMove, defaultFlag },
                    { LayerEventType.TouchStart, defaultFlag },
                    { LayerEventType.Wheel, defaultFlag }
                }
            )
        { }

        public static LayerEventActivationFlags All() => new LayerEventActivationFlags(true);
        public static LayerEventActivationFlags None() => new LayerEventActivationFlags(false);
    }
}
