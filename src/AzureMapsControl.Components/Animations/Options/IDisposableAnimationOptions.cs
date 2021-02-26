namespace AzureMapsControl.Components.Animations.Options
{
    public interface IDisposableAnimationOptions
    {
        /// <summary>
        /// Specifies if the animation should dispose itself once it has completed
        /// </summary>
        bool? DisposeOnComplete { get; set; }
    }
}
