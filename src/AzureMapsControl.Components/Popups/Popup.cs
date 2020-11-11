namespace AzureMapsControl.Components.Popups
{
    using System;

    /// <summary>
    /// An information window anchored at a specified position on a map.
    /// </summary>
    public sealed class Popup
    {
        public string Id { get; }

        /// <summary>
        /// Options of the popup
        /// </summary>
        public PopupOptions Options { get; set; }

        public Popup() : this(Guid.NewGuid().ToString()) { }
        public Popup(string id) => Id = id;

    }
}
