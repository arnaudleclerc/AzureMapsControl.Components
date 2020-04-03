namespace AzureMapsControl.Components.Events
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class EventActivationFlags<T>
        where T : AtlasEventType
    {
        protected EventActivationFlags(Dictionary<T, bool> eventFlags) => EventsFlags = eventFlags;

        protected readonly Dictionary<T, bool> EventsFlags;
    }

    public abstract class EventActivationFlags<T, U>
        : EventActivationFlags<T>
        where T : AtlasEventType
        where U : EventActivationFlags<T, U>
    {
        protected EventActivationFlags(Dictionary<T, bool> eventFlags): base(eventFlags) { }

        internal IEnumerable<string> EnabledEvents => EventsFlags?.Where(kvp => kvp.Value).Select(kvp => kvp.Key.ToString());

        public U Enable(params T[] eventTypes)
        {
            if (eventTypes != null)
            {
                foreach (var eventType in eventTypes)
                {
                    EventsFlags[eventType] = true;
                }
            }
            return this as U;
        }

        public U Disable(params T[] eventTypes)
        {
            if (eventTypes != null)
            {
                foreach (var eventType in eventTypes)
                {
                    EventsFlags[eventType] = false;
                }
            }
            return this as U;
        }
    }
}
