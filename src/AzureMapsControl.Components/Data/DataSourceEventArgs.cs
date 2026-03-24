
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using AzureMapsControl.Components.Atlas;

namespace AzureMapsControl.Components.Data;
[ExcludeFromCodeCoverage]
internal record DataSourceEventArgs
{
    public string Id { get; init; }
    public IEnumerable<Shape<Geometry>> Shapes { get; init; }
    public string Type { get; init; }
}
