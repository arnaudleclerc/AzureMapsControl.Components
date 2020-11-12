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
        internal Func<string, Task> RemoveCallback { get; set; }
        internal Func<string, PopupOptions, Task> UpdateCallback { get; set; }

        private bool _isRemoved = false;

        public string Id { get; }

        /// <summary>
        /// Options of the popup
        /// </summary>
        internal PopupOptions Options { get; }

        public Popup(PopupOptions options) : this(Guid.NewGuid().ToString(), options) { }
        public Popup(string id, PopupOptions options)
        {
            Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
            Options = options;
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
    }
}
