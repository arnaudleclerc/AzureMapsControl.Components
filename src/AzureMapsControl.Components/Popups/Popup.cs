namespace AzureMapsControl.Components.Popups
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Exceptions;

    /// <summary>
    /// An information window anchored at a specified position on a map.
    /// </summary>
    public sealed class Popup
    {
        internal Func<string, Task> OpenPopupCallback { get; set; }
        internal Func<string, Task> ClosePopupCallback { get; set; }
        internal Func<string, Task> RemoveAsyncCallback { get; set; }

        private bool _isRemoved = false;

        public string Id { get; }

        /// <summary>
        /// Options of the popup
        /// </summary>
        public PopupOptions Options { get; set; }

        public Popup() : this(Guid.NewGuid().ToString()) { }
        public Popup(string id) => Id = id;

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

            await RemoveAsyncCallback.Invoke(Id);
            _isRemoved = true;
        }
    }
}
