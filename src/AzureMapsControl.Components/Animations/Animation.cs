namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Runtime;

    internal abstract class Animation
    {
        internal readonly string Id;
        internal readonly IMapJsRuntime JsRuntime;

        internal Animation(string id, IMapJsRuntime jsRuntime)
        {
            Id = id;
            JsRuntime = jsRuntime;
        }

        /// <summary>
        /// Disposes the animation
        /// </summary>
        /// <returns></returns>
        public virtual async Task DisposeAsync() => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Dispose.ToAnimationNamespace(), Id);

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        /// <returns></returns>
        public virtual async Task PauseAsync() => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Pause.ToAnimationNamespace(), Id);

        /// <summary>
        /// Plays the animation.
        /// </summary>
        /// <returns></returns>
        public virtual async Task PlayAsync() => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Play.ToAnimationNamespace(), Id);

        /// <summary>
        /// Stops the animation and jumps back to the beginning of the animation. 
        /// </summary>
        /// <returns></returns>
        public virtual async Task ResetAsync() => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Reset.ToAnimationNamespace(), Id);

        /// <summary>
        /// Stops the animation and jumps back to the end of the animation. 
        /// </summary>
        /// <returns></returns>
        public virtual async Task StopAsync() => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Stop.ToAnimationNamespace(), Id);

        /// <summary>
        /// Advances the animation to specific step. 
        /// </summary>
        /// <param name="progress">The progress of the animation to advance to. A value between 0 and 1.</param>
        /// <returns></returns>
        public virtual async Task SeekAsync(decimal progress) => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Seek.ToAnimationNamespace(), Id, progress);
    }

    internal abstract class Animation<TOptions> : Animation
        where TOptions : IAnimationOptions
    {
        internal Animation(string id, IMapJsRuntime jsRuntime) : base(id, jsRuntime)
        {
        }

        /// <summary>
        /// Sets the options of the animation.
        /// </summary>
        /// <param name="options">Options to update the animation with</param>
        /// <returns></returns>
        public virtual async Task SetOptionsAsync(TOptions options) => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetOptions.ToAnimationNamespace(), Id, options);
    }
}
