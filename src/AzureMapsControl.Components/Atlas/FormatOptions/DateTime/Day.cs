
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Atlas.FormatOptions.DateTime;
[ExcludeFromCodeCoverage]
public struct Day
{
    private readonly string _type;

    public static readonly Day Numeric = new Day("numeric");
    public static readonly Day TwoDigits = new Day("2-digits");

    private Day(string type) => _type = type;

    public override string ToString() => _type;
}
