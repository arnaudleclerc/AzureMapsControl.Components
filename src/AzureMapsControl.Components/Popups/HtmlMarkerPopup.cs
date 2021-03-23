namespace AzureMapsControl.Components.Popups
{
    using System;
    using System.Threading.Tasks;

    public sealed class HtmlMarkerPopup : Popup
    {
        internal bool HasBeenToggled { get; set; }

        public HtmlMarkerPopup(PopupOptions options) : base(options)
        {
        }

        public HtmlMarkerPopup(PopupOptions options, PopupEventActivationFlags eventActivationFlags) : base(options, eventActivationFlags)
        {
        }

        public HtmlMarkerPopup(string id, PopupOptions options) : base(id, options)
        {
        }

        public HtmlMarkerPopup(string id, PopupOptions options, PopupEventActivationFlags eventActivationFlags) : base(id, options, eventActivationFlags)
        {
        }

        public override async Task CloseAsync()
        {
            if (HasBeenToggled)
            {
                await base.CloseAsync();
            }
        }

        public override async Task OpenAsync()
        {
            if (HasBeenToggled)
            {
                await base.OpenAsync();
            }
        }

        public override async Task RemoveAsync()
        {
            if (HasBeenToggled)
            {
                await base.RemoveAsync();
            }
        }

        public override async Task UpdateAsync(Action<PopupOptions> update)
        {
            if (HasBeenToggled)
            {
                await base.UpdateAsync(update);
            }
        }
    }
}
