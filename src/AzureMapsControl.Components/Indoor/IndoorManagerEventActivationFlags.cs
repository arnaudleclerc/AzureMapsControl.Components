namespace AzureMapsControl.Components.Indoor
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Events;

    public sealed class IndoorManagerEventActivationFlags : EventActivationFlags<IndoorManagerEventType, IndoorManagerEventActivationFlags>
    {
        private IndoorManagerEventActivationFlags(bool defaultFlag) :
            base
            (
                new Dictionary<IndoorManagerEventType, bool>
                {
                    { IndoorManagerEventType.FacilityChanged, defaultFlag },
                    { IndoorManagerEventType.LevelChanged, defaultFlag }
                }
            )
        { }

        public static IndoorManagerEventActivationFlags All() => new(true);
        public static IndoorManagerEventActivationFlags None() => new(false);
    }
}
