namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Runtime;

    public sealed class PlayableAnimation
    {
        private readonly string _id;
        private readonly IMapJsRuntime _jsRuntime;

        internal string Id => _id;

        public IPlayableAnimationOptions Options { get; private set; }

        internal PlayableAnimation(string id, IPlayableAnimationOptions options, IMapJsRuntime jsRuntime)
        {
            _id = id;
            Options = options;
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Disposes the animation
        /// </summary>
        /// <returns></returns>
        public async Task DisposeAsync() => await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Dispose.ToAnimationNamespace(), _id);

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        /// <returns></returns>
        public async Task PauseAsync() => await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Pause.ToAnimationNamespace(), _id);

        /// <summary>
        /// Plays the animation.
        /// </summary>
        /// <returns></returns>
        public async Task PlayAsync() => await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Play.ToAnimationNamespace(), _id);

        /// <summary>
        /// Stops the animation and jumps back to the beginning of the animation. 
        /// </summary>
        /// <returns></returns>
        public async Task ResetAsync() => await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Reset.ToAnimationNamespace(), _id);

        /// <summary>
        /// Advances the animation to specific step. 
        /// </summary>
        /// <param name="progress">The progress of the animation to advance to. A value between 0 and 1.</param>
        /// <returns></returns>
        public async Task SeekAsync(decimal progress) => await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Seek.ToAnimationNamespace(), _id, progress);

        /// <summary>
        /// Stops the animation and jumps back to the end of the animation. 
        /// </summary>
        /// <returns></returns>
        public async Task StopAsync() => await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Stop.ToAnimationNamespace(), _id);

        /// <summary>
        /// Sets the options of the animation.
        /// </summary>
        /// <param name="options">Options to update the animation with</param>
        /// <returns></returns>
        public async Task SetOptionsAsync(IPlayableAnimationOptions options)
        {
            Options = options;
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetOptions.ToAnimationNamespace(), _id, options);
        }
    }
}
