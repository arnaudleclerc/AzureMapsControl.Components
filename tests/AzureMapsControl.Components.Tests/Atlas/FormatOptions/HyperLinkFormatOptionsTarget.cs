
using AzureMapsControl.Components.Atlas.FormatOptions;
using AzureMapsControl.Components.Tests.Json;

using Xunit;

namespace AzureMapsControl.Components.Tests.Atlas.FormatOptions;
public class HyperLinkFormatOptionsTargetJsonConverterTests : JsonConverterTests<HyperLinkFormatOptionsTarget>
{
    public HyperLinkFormatOptionsTargetJsonConverterTests() : base(new HyperLinkFormatOptionsTargetJsonConverter()) { }

    [Fact]
    public void Should_Write() => TestAndAssertWrite(HyperLinkFormatOptionsTarget.Blank, "\"" + HyperLinkFormatOptionsTarget.Blank.ToString() + "\"");
}
