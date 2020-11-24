namespace AzureMapsControl.Components.Popups
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Exceptions;

    public delegate void PopupEvent(PopupEventArgs eventArgs);

    /// <summary>
    /// An information window anchored at a specified position on a map.
    /// </summary>
    public sealed class Popup
    {
        internal Func<string, Task> OpenPopupCallback { get; set; }
        internal Func<string, Task> ClosePopupCallback { get; set; }
        internal Func<string, Task> RemoveCallback { get; set; }
        internal Func<string, PopupOptions, Task> UpdateCallback { get; set; }

        private bool _isRemoved = false;

        public string Id { get; }

        public PopupEventActivationFlags EventActivationFlags { get; set; }

        /// <summary>
        /// Options of the popup
        /// </summary>
        internal PopupOptions Options { get; }

        public event PopupEvent OnClose;
        public event PopupEvent OnDrag;
        public event PopupEvent OnDragEnd;
        public event PopupEvent OnDragStart;
        public event PopupEvent OnOpen;

        public Popup(PopupOptions options) : this(Guid.NewGuid().ToString(), options) { }

        public Popup(PopupOptions options, PopupEventActivationFlags eventActivationFlags) : this(Guid.NewGuid().ToString(), options, eventActivationFlags) { }

        public Popup(string id, PopupOptions options) : this(id, options, PopupEventActivationFlags.None()) { }

        public Popup(string id, PopupOptions options, PopupEventActivationFlags eventActivationFlags)
        {
            Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
            Options = options;
            EventActivationFlags = eventActivationFlags;
        }

        /// <summary>
        /// Open the popup
        /// </summary>
        /// <returns></returns>
        public async Task OpenAsync() => await OpenPopupCallback.Invoke(Id);

        /// <summary>
        /// Close the popup
        /// </summary>
        /// <returns></returns>
        public async Task CloseAsync() => await ClosePopupCallback.Invoke(Id);

        /// <summary>
        /// Remove the popup from the map
        /// </summary>
        /// <returns></returns>
        public async Task RemoveAsync()
        {
            if (_isRemoved)
            {
                throw new PopupAlreadyRemovedException();
            }

            await RemoveCallback.Invoke(Id);
            _isRemoved = true;
        }

        /// <summary>
        /// Update the popup with the updated options
        /// </summary>
        /// <param name="update">Update to provide on the options</param>
        /// <returns></returns>
        public async Task UpdateAsync(Action<PopupOptions> update)
        {
            update.Invoke(Options);
            await UpdateCallback.Invoke(Id, Options);
        }

        internal void DispatchEvent(PopupEventArgs eventArgs)
        {
            switch (eventArgs.Type)
            {
                case "close":
                    OnClose?.Invoke(eventArgs);
                    break;

                case "drag":
                    OnDrag?.Invoke(eventArgs);
                    break;

                case "dragend":
                    OnDragEnd?.Invoke(eventArgs);
                    break;

                case "dragstart":
                    OnDragStart?.Invoke(eventArgs);
                    break;

                case "open":
                    OnOpen?.Invoke(eventArgs);
                    break;
            }
        }
    }
}
