namespace AzureMapsControl.Components.Runtime
{
    using System.Threading.Tasks;

    internal interface IMapJsRuntime
    {
        ValueTask InvokeVoidAsync(string identifier, params object[] args);
        ValueTask<TValue> InvokeAsync<TValue>(string identifier, params object[] args);
    }
}
