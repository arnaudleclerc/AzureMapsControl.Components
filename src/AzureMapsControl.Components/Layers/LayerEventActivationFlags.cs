namespace AzureMapsControl.Components.Layers
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Events;

    public sealed class LayerEventActivationFlags : EventActivationFlags<LayerEventType, LayerEventActivationFlags>
    {
        public static readonly LayerEventActivationFlags All = new LayerEventActivationFlags(true);
        public static readonly LayerEventActivationFlags None = new LayerEventActivationFlags(false);

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
    }
}
