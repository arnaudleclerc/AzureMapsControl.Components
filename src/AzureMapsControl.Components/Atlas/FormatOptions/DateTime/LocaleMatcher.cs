
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Atlas.FormatOptions.DateTime;
[ExcludeFromCodeCoverage]
public struct LocaleMatcher
{
    private readonly string _type;

    public static readonly LocaleMatcher BestFit = new LocaleMatcher("best fit");
    public static readonly LocaleMatcher Lookup = new LocaleMatcher("lookup");

    private LocaleMatcher(string type) => _type = type;

    public override string ToString() => _type;
}
