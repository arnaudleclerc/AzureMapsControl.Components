
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Atlas.FormatOptions.DateTime;
[ExcludeFromCodeCoverage]
public struct Hour
{
    private readonly string _type;

    public static readonly Hour Numeric = new Hour("numeric");
    public static readonly Hour TwoDigits = new Hour("2-digits");

    private Hour(string type) => _type = type;

    public override string ToString() => _type;
}
