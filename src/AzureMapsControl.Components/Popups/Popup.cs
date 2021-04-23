namespace AzureMapsControl.Components.Popups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Guards;
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
        internal PopupOptions Options { get; private set; }

        public event PopupEvent OnClose;
        public event PopupEvent OnDrag;
        public event PopupEvent OnDragEnd;
        public event PopupEvent OnDragStart;
        public event PopupEvent OnOpen;

        internal event PopupRemovedEvent OnRemoved;

        public Popup() : this(null) { }

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
        /// <exception cref="PopupAlreadyRemovedException">The popup has already been removed</exception>
        public virtual async ValueTask OpenAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Popup_OpenAsync, "Opening popup");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Popup_OpenAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();
            EnsureNotRemoved();

            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Open.ToPopupNamespace(), Id);
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="PopupAlreadyRemovedException">The popup has already been removed</exception>
        public virtual async ValueTask CloseAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Popup_CloseAsync, "Closing popup");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Popup_CloseAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();
            EnsureNotRemoved();

            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Close.ToPopupNamespace(), Id);
        }

        /// <summary>
        /// Remove the popup from the map
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="PopupAlreadyRemovedException">The popup has already been removed</exception>
        public virtual async ValueTask RemoveAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Popup_RemoveAsync, "Removing popup");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Popup_RemoveAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();
            EnsureNotRemoved();

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
        /// <exception cref="PopupAlreadyRemovedException">The popup has already been removed</exception>
        [Obsolete("Will be removed in a future version. Use SetOptionsAsync instead.")]
        public virtual async ValueTask UpdateAsync(Action<PopupOptions> update)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Popup_UpdateAsync, "Popup - UpdateAsync");

            await SetOptionsAsync(update);
        }

        /// <summary>
        /// Set the options on the popup
        /// </summary>
        /// <param name="update">Update to apply on the options</param>
        /// <returns></returns>
        /// <exception cref="ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="PopupAlreadyRemovedException">The popup has already been removed</exception>
        public virtual async ValueTask SetOptionsAsync(Action<PopupOptions> update)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Popup_SetOptionsAsync, "Poup - SetOptionsAsync");

            EnsureJsRuntimeExists();
            EnsureNotRemoved();

            if (Options is null)
            {
                Options = new PopupOptions();
            }

            update.Invoke(Options);
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Popup_SetOptionsAsync, $"Id: {Id}");
            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.SetOptions.ToPopupNamespace(), Id, Options);
        }

        /// <summary>
        /// Apply a template on a popup
        /// </summary>
        /// <param name="template">Template to apply</param>
        /// <param name="properties">Properties of the template</param>
        /// <param name="update">Update to apply on the options</param>
        /// <returns></returns>
        /// <exception cref="ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="PopupAlreadyRemovedException">The popup has already been removed</exception>
        public async ValueTask ApplyTemplateAsync(PopupTemplate template, IDictionary<string, object> properties, Action<PopupOptions> update = null)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Popup_ApplyTemplateAsync, "Poup - ApplyTemplateAsync");

            Require.NotNull(properties, nameof(properties));

            EnsureJsRuntimeExists();
            EnsureNotRemoved();

            if(Options is null)
            {
                Options = new PopupOptions();
            }

            update?.Invoke(Options);

            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Popup_ApplyTemplateAsync, $"Id: {Id}");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Popup_ApplyTemplateAsync, $"Template: {template}");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Popup_ApplyTemplateAsync, $"Properties: {string.Join('|', properties.Select(kvp => kvp.Key + " : " + kvp.Value))}");
            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.ApplyTemplate.ToPopupNamespace(), Id, Options, properties, template);
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

        private void EnsureNotRemoved()
        {
            if (IsRemoved)
            {
                throw new PopupAlreadyRemovedException();
            }
        }
    }
}
