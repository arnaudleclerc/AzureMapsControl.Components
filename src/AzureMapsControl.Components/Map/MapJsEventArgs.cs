
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using AzureMapsControl.Components.Atlas;

namespace AzureMapsControl.Components.Map;
[ExcludeFromCodeCoverage]
internal record MapJsEventArgs
{
    public string Error { get; init; }
    public string Type { get; init; }
    public string LayerId { get; init; }
    public IEnumerable<Feature<Geometry>> Features { get; init; }
    public IEnumerable<Shape<Geometry>> Shapes { get; init; }
    public Pixel Pixel { get; init; }
    public Position Position { get; init; }
    public string DataType { get; init; }
    public bool? IsSourceLoaded { get; init; }
    public Source Source { get; init; }
    public string SourceDataType { get; init; }
    public string Style { get; init; }
    public Tile Tile { get; init; }
    public string Message { get; init; }
    public IEnumerable<Pixel> Pixels { get; init; }
    public IEnumerable<Position> Positions { get; init; }
    public string Id { get; init; }
}
