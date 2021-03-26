namespace AzureMapsControl.Components.Atlas
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public abstract class Geometry
    {
        private string _type;

        internal string Id { get; set; }

        public string Type
        {
            get => string.IsNullOrEmpty(_type) ? GetGeometryType() : _type;
            set => _type = value;
        }

        public Geometry() { }

        internal abstract string GetGeometryType();
    }

    [ExcludeFromCodeCoverage]
    public abstract class Geometry<TPosition> : Geometry
    {
        public TPosition Coordinates { get; set; }

        public Geometry() { }

        public Geometry(TPosition coordinates) : base() => Coordinates = coordinates;
    }
}
