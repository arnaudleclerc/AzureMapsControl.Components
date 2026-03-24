
using System.Threading.Tasks;

namespace AzureMapsControl.Components.Animations;
public interface IMoveAlongRouteAnimation : IAnimation
{
    /// <summary>
    /// Disposes the animation
    /// </summary>
    /// <returns></returns>
    ValueTask DisposeAsync();
}
