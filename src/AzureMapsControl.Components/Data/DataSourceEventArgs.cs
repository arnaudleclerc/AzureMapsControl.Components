namespace AzureMapsControl.Components.Data
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    [ExcludeFromCodeCoverage]
    internal class DataSourceEventArgs
    {
        public string Id { get; set; }
        public IEnumerable<Shape<Geometry>> Shapes { get; set; }
        public string Type { get; set; }
    }
}
