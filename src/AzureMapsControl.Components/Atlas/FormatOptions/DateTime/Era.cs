namespace AzureMapsControl.Components.Atlas.FormatOptions.DateTime
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public struct Era
    {
        private readonly string _type;

        public static readonly Era Long = new Era("long");
        public static readonly Era Short = new Era("short");
        public static readonly Era Narrow = new Era("narrow");

        private Era(string type) => _type = type;

        public override string ToString() => _type;
    }
}
