
using System.Threading.Tasks;

namespace AzureMapsControl.Components.Animations;
public interface IFlowingDashedLineAnimation : IAnimation
{
    /// <summary>
    /// Disposes the animation
    /// </summary>
    /// <returns></returns>
    ValueTask DisposeAsync();

    /// <summary>
    /// Plays the animation.
    /// </summary>
    /// <returns></returns>
    ValueTask PlayAsync();

    /// <summary>
    /// Stops the animation and jumps back to the beginning of the animation. 
    /// </summary>
    /// <returns></returns>
    ValueTask ResetAsync();

    /// <summary>
    /// Stops the animation and jumps back to the end of the animation. 
    /// </summary>
    /// <returns></returns>
    ValueTask StopAsync();

    /// <summary>
    /// Pauses the animation.
    /// </summary>
    /// <returns></returns>
    ValueTask PauseAsync();
}
