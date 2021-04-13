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

        public override async ValueTask CloseAsync()
        {
            if (HasBeenToggled)
            {
                await base.CloseAsync();
            }
        }

        public override async ValueTask OpenAsync()
        {
            if (HasBeenToggled)
            {
                await base.OpenAsync();
            }
        }

        public override async ValueTask RemoveAsync()
        {
            if (HasBeenToggled)
            {
                await base.RemoveAsync();
            }
        }

        public override async ValueTask UpdateAsync(Action<PopupOptions> update)
        {
            if (HasBeenToggled)
            {
                await base.UpdateAsync(update);
            }
        }

        public override async ValueTask SetOptionsAsync(Action<PopupOptions> update)
        {
            if (HasBeenToggled)
            {
                await base.SetOptionsAsync(update);
            }
        }

    }
}
