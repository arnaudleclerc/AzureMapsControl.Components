namespace AzureMapsControl.Components.Drawing
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Events;

    public sealed class DrawingToolbarEventActivationFlags : EventActivationFlags<DrawingToolbarEventType, DrawingToolbarEventActivationFlags>
    {
        private DrawingToolbarEventActivationFlags(bool defaultFlag) : base(new Dictionary<DrawingToolbarEventType, bool>
        {
            { DrawingToolbarEventType.DrawingChanged, defaultFlag },
            { DrawingToolbarEventType.DrawingChanging, defaultFlag },
            { DrawingToolbarEventType.DrawingComplete, defaultFlag },
            { DrawingToolbarEventType.DrawingModeChanged, defaultFlag },
            { DrawingToolbarEventType.DrawingStarted, defaultFlag }
        })
        {

        }

        public static DrawingToolbarEventActivationFlags All() => new DrawingToolbarEventActivationFlags(true);
        public static DrawingToolbarEventActivationFlags None() => new DrawingToolbarEventActivationFlags(false);
    }
}
