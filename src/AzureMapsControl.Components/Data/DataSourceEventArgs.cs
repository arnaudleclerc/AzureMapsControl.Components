
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using AzureMapsControl.Components.Atlas;

namespace AzureMapsControl.Components.Data;
[ExcludeFromCodeCoverage]
internal class DataSourceEventArgs
{
    public string Id { get; set; }
    public IEnumerable<Shape<Geometry>> Shapes { get; set; }
    public string Type { get; set; }
}
