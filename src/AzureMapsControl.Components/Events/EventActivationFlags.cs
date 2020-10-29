namespace AzureMapsControl.Components.Events
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class EventActivationFlags
    {
        internal abstract IEnumerable<string> EnabledEvents { get; }
    }

    public abstract class EventActivationFlags<T> : EventActivationFlags
        where T : AtlasEventType
    {
        protected EventActivationFlags(IDictionary<T, bool> eventFlags) => EventsFlags = eventFlags;

        protected readonly IDictionary<T, bool> EventsFlags;
    }

    public abstract class EventActivationFlags<T, U>
        : EventActivationFlags<T>
        where T : AtlasEventType
        where U : EventActivationFlags<T, U>
    {
        protected EventActivationFlags(IDictionary<T, bool> eventFlags) : base(eventFlags) { }

        internal override IEnumerable<string> EnabledEvents => EventsFlags?.Where(kvp => kvp.Value).Select(kvp => kvp.Key.ToString());

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
