namespace AzureMapsControl.Components.Events
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public abstract class AtlasEventType
    {
        private readonly string _event;

        protected internal AtlasEventType(string atlasEvent) => _event = atlasEvent;

        public override string ToString() => _event;
    }
}
