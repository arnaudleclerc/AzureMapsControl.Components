namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public abstract class Geometry
    {
        private string _type;

        public string Id { get; set; }

        public string Type
        {
            get => string.IsNullOrEmpty(_type) ? GetGeometryType() : _type;
            set => _type = value;
        }

        public Geometry() { }
        public Geometry(string id) => Id = id;

        internal abstract string GetGeometryType();
    }

    [ExcludeFromCodeCoverage]
    public abstract class Geometry<TPosition> : Geometry
    {
        public TPosition Coordinates { get; set; }

        public Geometry() { }
        public Geometry(string id) : base(id) { }

        public Geometry(TPosition coordinates): base(Guid.NewGuid().ToString()) => Coordinates = coordinates;
    }
}
