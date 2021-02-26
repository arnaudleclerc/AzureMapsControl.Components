namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    public interface ISeekAnimation
    {
        /// <summary>
        /// Advances the animation to specific step. 
        /// </summary>
        /// <param name="progress">The progress of the animation to advance to. A value between 0 and 1.</param>
        /// <returns></returns>
        Task SeekAsync(decimal progress);
    }
}
