namespace AzureMapsControl.Components.Animations
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Logger;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    internal sealed class AnimationService : IAnimationService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ILogger _logger;
        public AnimationService(IJSRuntime jsRuntime, ILogger<AnimationService> logger)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
        }

        public async Task SnakeLine(LineString line, DataSource source, SnakeLineAnimationOptions options)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Snakeline, "Calling snakeline");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "LineId", line.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "Options", options);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animations.Snakeline.ToAnimationsNamespace(), Guid.NewGuid(), line.Id, source.Id, options);
        }
    }
}
