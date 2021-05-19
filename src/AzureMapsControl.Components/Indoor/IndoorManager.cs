namespace AzureMapsControl.Components.Indoor
{
    using System;

    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    public sealed class IndoorManager
    {
        internal readonly string Id;
        private readonly IMapJsRuntime _jsRuntime;
        private readonly ILogger _logger;

        internal IndoorManager(IMapJsRuntime jsRuntime, ILogger logger)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
            Id = Guid.NewGuid().ToString();
        }
    }
}
