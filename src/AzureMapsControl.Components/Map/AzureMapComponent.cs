namespace AzureMapsControl.Map
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Configuration;

    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Options;
    using Microsoft.JSInterop;

    public class AzureMapComponent : ComponentBase, IDisposable
    {
        [Inject]
        internal IJSRuntime JSRuntime { get; set; }

        [Inject]
        internal IOptions<AzureMapConfiguration> Configuration { get; set; }

        [Parameter]
        public string MapId { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("azureMapsControl.addMap", MapId, Configuration.Value.SubscriptionKey);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        public void Dispose()
        {
        }
    }
}
