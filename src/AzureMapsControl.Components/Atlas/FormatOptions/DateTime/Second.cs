
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Atlas.FormatOptions.DateTime;
[ExcludeFromCodeCoverage]
public struct Second
{
    private readonly string _type;

    public static readonly Second Numeric = new Second("numeric");
    public static readonly Second TwoDigits = new Second("2-digits");

    private Second(string type) => _type = type;

    public override string ToString() => _type;
}
