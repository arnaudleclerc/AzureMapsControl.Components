namespace AzureMapsControl.Components.Popups
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// An information window anchored at a specified position on a map.
    /// </summary>
    public sealed class Popup
    {
        internal Func<string, Task> OpenPopupCallback { get; set; }
        internal Func<string, Task> ClosePopupCallback { get; set; }
        public string Id { get; }

        /// <summary>
        /// Options of the popup
        /// </summary>
        public PopupOptions Options { get; set; }

        public Popup() : this(Guid.NewGuid().ToString()) { }
        public Popup(string id) => Id = id;

        public async Task OpenAsync() => await OpenPopupCallback.Invoke(Id);
        public async Task CloseAsync() => await ClosePopupCallback.Invoke(Id);
    }
}
