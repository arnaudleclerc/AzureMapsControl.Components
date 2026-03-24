
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Events;
[ExcludeFromCodeCoverage]
public abstract class AtlasEventType
{
    private readonly string _event;

    protected internal AtlasEventType(string atlasEvent) => _event = atlasEvent;

    public override string ToString() => _event;
}
