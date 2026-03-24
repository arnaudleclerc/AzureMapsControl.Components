
using System.Threading.Tasks;

namespace AzureMapsControl.Components.Runtime;
internal interface IMapJsRuntime
{
    ValueTask InvokeVoidAsync(string identifier, params object[] args);
    ValueTask<TValue> InvokeAsync<TValue>(string identifier, params object[] args);
}
