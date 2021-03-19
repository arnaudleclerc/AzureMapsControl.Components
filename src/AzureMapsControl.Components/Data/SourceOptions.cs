namespace AzureMapsControl.Components.Data
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public abstract class SourceOptions
    {
        internal virtual object GenerateJsOptions() => this;
    }
}
