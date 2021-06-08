namespace AzureMapsControl.Components.FullScreen
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Events;

    public sealed class FullScreenEventActivationFlags : EventActivationFlags<FullScreenEventType, FullScreenEventActivationFlags>
    {
        private FullScreenEventActivationFlags(bool defaultFlag) :
            base(new Dictionary<FullScreenEventType, bool> {
                { FullScreenEventType.FullScreenChanged, defaultFlag }
            })
        { }

        public static FullScreenEventActivationFlags All() => new FullScreenEventActivationFlags(true);
        public static FullScreenEventActivationFlags None() => new FullScreenEventActivationFlags(false);
    }
}
