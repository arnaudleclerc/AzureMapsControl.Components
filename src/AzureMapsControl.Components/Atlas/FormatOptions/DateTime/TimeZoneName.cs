namespace AzureMapsControl.Components.Atlas.FormatOptions.DateTime
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public struct TimeZoneName
    {
        private readonly string _type;

        public static readonly TimeZoneName Long = new TimeZoneName("long");
        public static readonly TimeZoneName Short = new TimeZoneName("short");

        private TimeZoneName(string type) => _type = type;

        public override string ToString() => _type;
    }
}
