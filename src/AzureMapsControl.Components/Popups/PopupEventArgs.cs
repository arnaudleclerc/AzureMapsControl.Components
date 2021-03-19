namespace AzureMapsControl.Components.Popups
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class PopupEventArgs
    {
        public string Id { get; set; }
        public string Type { get; set; }
    }
}
