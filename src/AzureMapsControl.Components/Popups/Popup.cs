namespace AzureMapsControl.Components.Popups
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    public delegate void PopupEvent(PopupEventArgs eventArgs);
    internal delegate void PopupRemovedEvent();

    /// <summary>
    /// An information window anchored at a specified position on a map.
    /// </summary>
    public class Popup
    {
        internal bool IsRemoved { get; set; }

        internal IMapJsRuntime JSRuntime { get; set; }
        internal ILogger Logger { get; set; }

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

        internal event PopupRemovedEvent OnRemoved;

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
        /// <exception cref="ComponentNotAddedToMapException">The control has not been added to the map</exception>
        public virtual async ValueTask OpenAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Popup_OpenAsync, "Opening popup");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Popup_OpenAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();

            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Open.ToPopupNamespace(), Id);
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ComponentNotAddedToMapException">The control has not been added to the map</exception>
        public virtual async ValueTask CloseAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Popup_CloseAsync, "Closing popup");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Popup_CloseAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();

            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Close.ToPopupNamespace(), Id);
        }

        /// <summary>
        /// Remove the popup from the map
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ComponentNotAddedToMapException">The control has not been added to the map</exception>
        public virtual async ValueTask RemoveAsync()
        {
            if (IsRemoved)
            {
                throw new PopupAlreadyRemovedException();
            }

            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Popup_RemoveAsync, "Removing popup");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Popup_RemoveAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();

            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Remove.ToPopupNamespace(), Id);

            OnRemoved?.Invoke();
            IsRemoved = true;
        }

        /// <summary>
        /// Update the popup with the updated options
        /// </summary>
        /// <param name="update">Update to provide on the options</param>
        /// <returns></returns>
        /// <exception cref="ComponentNotAddedToMapException">The control has not been added to the map</exception>
        public virtual async ValueTask UpdateAsync(Action<PopupOptions> update)
        {
            EnsureJsRuntimeExists();

            update.Invoke(Options);
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Popup_UpdateAsync, "Removing popup");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Popup_UpdateAsync, $"Id: {Id}");
            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Update.ToPopupNamespace(), Id, Options);
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

        private void EnsureJsRuntimeExists()
        {
            if (JSRuntime is null)
            {
                throw new ComponentNotAddedToMapException();
            }
        }
    }
}
