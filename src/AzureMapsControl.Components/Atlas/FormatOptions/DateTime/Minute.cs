
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Atlas.FormatOptions.DateTime;
[ExcludeFromCodeCoverage]
public struct Minute
{
    private readonly string _type;

    public static readonly Minute Numeric = new Minute("numeric");
    public static readonly Minute TwoDigits = new Minute("2-digits");

    private Minute(string type) => _type = type;

    public override string ToString() => _type;
}
