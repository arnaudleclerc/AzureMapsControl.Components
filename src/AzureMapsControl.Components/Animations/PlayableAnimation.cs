namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Runtime;

    internal class Animation : IAnimation
    {
        internal readonly string Id;
        protected readonly IMapJsRuntime JsRuntime;

        internal Animation(string id, IMapJsRuntime jsRuntime)
        {
            Id = id;
            JsRuntime = jsRuntime;
        }

        /// <summary>
        /// Disposes the animation
        /// </summary>
        /// <returns></returns>
        public async Task DisposeAsync() => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Dispose.ToAnimationNamespace(), Id);

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        /// <returns></returns>
        public async Task PauseAsync() => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Pause.ToAnimationNamespace(), Id);

        /// <summary>
        /// Plays the animation.
        /// </summary>
        /// <returns></returns>
        public async Task PlayAsync() => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Play.ToAnimationNamespace(), Id);

        /// <summary>
        /// Stops the animation and jumps back to the beginning of the animation. 
        /// </summary>
        /// <returns></returns>
        public async Task ResetAsync() => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Reset.ToAnimationNamespace(), Id);

        /// <summary>
        /// Stops the animation and jumps back to the end of the animation. 
        /// </summary>
        /// <returns></returns>
        public async Task StopAsync() => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Stop.ToAnimationNamespace(), Id);
    }
}
