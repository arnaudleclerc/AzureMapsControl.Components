namespace AzureMapsControl.Components.Layers
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Events;

    public sealed class ImageLayerEventActivationFlags : EventActivationFlags<ImageLayerEventType, ImageLayerEventActivationFlags>
    {
        public static readonly ImageLayerEventActivationFlags All = new ImageLayerEventActivationFlags(true);
        public static readonly ImageLayerEventActivationFlags None = new ImageLayerEventActivationFlags(false);

        private ImageLayerEventActivationFlags(bool defaultFlag) :
            base
            (
                new Dictionary<ImageLayerEventType, bool>
                {
                    { ImageLayerEventType.Click, defaultFlag },
                    { ImageLayerEventType.ContextMenu, defaultFlag },
                    { ImageLayerEventType.DblClick, defaultFlag },
                    { ImageLayerEventType.LayerAdded, defaultFlag },
                    { ImageLayerEventType.LayerRemoved, defaultFlag },
                    { ImageLayerEventType.MouseDown, defaultFlag },
                    { ImageLayerEventType.MouseEnter, defaultFlag },
                    { ImageLayerEventType.MouseLeave, defaultFlag },
                    { ImageLayerEventType.MouseMove, defaultFlag },
                    { ImageLayerEventType.MouseOut, defaultFlag },
                    { ImageLayerEventType.MouseOver, defaultFlag },
                    { ImageLayerEventType.MouseUp, defaultFlag },
                    { ImageLayerEventType.TouchCancel, defaultFlag },
                    { ImageLayerEventType.TouchEnd, defaultFlag },
                    { ImageLayerEventType.TouchMove, defaultFlag },
                    { ImageLayerEventType.TouchStart, defaultFlag },
                    { ImageLayerEventType.Wheel, defaultFlag }
                }
            )
        { }
    }
}
