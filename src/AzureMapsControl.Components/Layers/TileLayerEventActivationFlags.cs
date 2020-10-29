namespace AzureMapsControl.Components.Layers
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Events;

    public sealed class TileLayerEventActivationFlags : EventActivationFlags<TileLayerEventType, TileLayerEventActivationFlags>
    {
        public static readonly TileLayerEventActivationFlags All = new TileLayerEventActivationFlags(true);
        public static readonly TileLayerEventActivationFlags None = new TileLayerEventActivationFlags(false);

        private TileLayerEventActivationFlags(bool defaultFlag) :
            base
            (
                new Dictionary<TileLayerEventType, bool>
                {
                    { TileLayerEventType.Click, defaultFlag },
                    { TileLayerEventType.ContextMenu, defaultFlag },
                    { TileLayerEventType.DblClick, defaultFlag },
                    { TileLayerEventType.LayerAdded, defaultFlag },
                    { TileLayerEventType.LayerRemoved, defaultFlag },
                    { TileLayerEventType.MouseDown, defaultFlag },
                    { TileLayerEventType.MouseEnter, defaultFlag },
                    { TileLayerEventType.MouseLeave, defaultFlag },
                    { TileLayerEventType.MouseMove, defaultFlag },
                    { TileLayerEventType.MouseOut, defaultFlag },
                    { TileLayerEventType.MouseOver, defaultFlag },
                    { TileLayerEventType.MouseUp, defaultFlag },
                    { TileLayerEventType.TouchCancel, defaultFlag },
                    { TileLayerEventType.TouchEnd, defaultFlag },
                    { TileLayerEventType.TouchMove, defaultFlag },
                    { TileLayerEventType.TouchStart, defaultFlag },
                    { TileLayerEventType.Wheel, defaultFlag }
                }
            )
        { }
    }
}
