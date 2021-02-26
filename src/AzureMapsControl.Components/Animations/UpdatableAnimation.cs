namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Runtime;

    internal sealed class UpdatableAnimation : Animation, IUpdatableAnimation
    {
        internal UpdatableAnimation(string id, IMapJsRuntime jsRuntime) : base(id, jsRuntime) { }

        /// <summary>
        /// Advances the animation to specific step. 
        /// </summary>
        /// <param name="progress">The progress of the animation to advance to. A value between 0 and 1.</param>
        /// <returns></returns>
        public async Task SeekAsync(decimal progress) => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Seek.ToAnimationNamespace(), Id, progress);

        /// <summary>
        /// Sets the options of the animation.
        /// </summary>
        /// <param name="options">Options to update the animation with</param>
        /// <returns></returns>
        public async Task SetOptionsAsync(IAnimationOptions options) => await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetOptions.ToAnimationNamespace(), Id, options);
    }
}
