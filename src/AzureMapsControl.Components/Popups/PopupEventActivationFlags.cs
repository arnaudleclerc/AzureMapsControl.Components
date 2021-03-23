namespace AzureMapsControl.Components.Popups
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Events;

    public sealed class PopupEventActivationFlags : EventActivationFlags<PopupEventType, PopupEventActivationFlags>
    {
        private PopupEventActivationFlags(bool defaultFlag) : base(new Dictionary<PopupEventType, bool> {
            { PopupEventType.Close, defaultFlag },
            { PopupEventType.Drag, defaultFlag },
            { PopupEventType.DragEnd, defaultFlag },
            { PopupEventType.DragStart, defaultFlag },
            { PopupEventType.Open, defaultFlag }
        })
        { }

        public static PopupEventActivationFlags All() => new PopupEventActivationFlags(true);
        public static PopupEventActivationFlags None() => new PopupEventActivationFlags(false);
    }
}
