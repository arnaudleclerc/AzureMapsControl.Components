namespace AzureMapsControl.Components.Animations
{
    public interface IDisposableAnimationOptions : IAnimationOptions
    {
        /// <summary>
        /// Specifies if the animation should dispose itself once it has completed
        /// </summary>
        bool? DisposeOnComplete { get; set; }
    }
}
