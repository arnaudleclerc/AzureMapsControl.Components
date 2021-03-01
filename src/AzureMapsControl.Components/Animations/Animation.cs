namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Animations.Options;
    using AzureMapsControl.Components.Runtime;

    internal abstract class Animation : IAnimation
    {
        private readonly string _id;

        internal readonly IMapJsRuntime JsRuntime;

        public string Id => _id;
        public bool Disposed { get; internal set; }

        internal Animation(string id, IMapJsRuntime jsRuntime)
        {
            _id = id;
            JsRuntime = jsRuntime;
        }

        /// <summary>
        /// Disposes the animation
        /// </summary>
        /// <returns></returns>
        public virtual async Task DisposeAsync()
        {
            EnsureNotDisposed();
            await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Dispose.ToAnimationNamespace(), Id);
            Disposed = true;
        }

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        /// <returns></returns>
        public virtual async Task PauseAsync()
        {
            EnsureNotDisposed();
            await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Pause.ToAnimationNamespace(), Id);
        }

        /// <summary>
        /// Plays the animation.
        /// </summary>
        /// <returns></returns>
        public virtual async Task PlayAsync()
        {
            EnsureNotDisposed();
            await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Play.ToAnimationNamespace(), Id);
        }

        /// <summary>
        /// Stops the animation and jumps back to the beginning of the animation. 
        /// </summary>
        /// <returns></returns>
        public virtual async Task ResetAsync()
        {
            EnsureNotDisposed();
            await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Reset.ToAnimationNamespace(), Id);
        }

        /// <summary>
        /// Stops the animation and jumps back to the end of the animation. 
        /// </summary>
        /// <returns></returns>
        public virtual async Task StopAsync()
        {
            EnsureNotDisposed();
            await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Stop.ToAnimationNamespace(), Id);
        }

        /// <summary>
        /// Advances the animation to specific step. 
        /// </summary>
        /// <param name="progress">The progress of the animation to advance to. A value between 0 and 1.</param>
        /// <returns></returns>
        public virtual async Task SeekAsync(decimal progress)
        {
            EnsureNotDisposed();
            await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Seek.ToAnimationNamespace(), Id, progress);
        }

        protected void EnsureNotDisposed()
        {
            if (Disposed)
            {
                throw new AnimationDisposedException();
            }
        }
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
        public virtual async Task SetOptionsAsync(TOptions options)
        {
            EnsureNotDisposed();
            await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetOptions.ToAnimationNamespace(), Id, options);
        }
    }
}
