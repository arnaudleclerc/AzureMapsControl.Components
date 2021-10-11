namespace AzureMapsControl.Components.Data
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal struct SourceType
    {
        private readonly string _type;

        public static readonly SourceType DataSource = new SourceType("datasource");
        public static readonly SourceType GriddedDataSource = new SourceType("griddeddatasource");
        public static readonly SourceType VectorTileSource = new SourceType("vectortilesource");
        
        public SourceType(string type) => _type = type;

        public override string ToString() => _type;
    }
}
