namespace AzureMapsControl.Components.Tests.Atlas.FormatOptions
{
    using AzureMapsControl.Components.Atlas.FormatOptions;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class HyperLinkFormatOptionsTargetJsonConverterTests : JsonConverterTests<HyperLinkFormatOptionsTarget>
    {
        public HyperLinkFormatOptionsTargetJsonConverterTests() : base(new HyperLinkFormatOptionsTargetJsonConverter()) { }

        [Fact]
        public void Should_Write() => TestAndAssertWrite(HyperLinkFormatOptionsTarget.Blank, "\"" + HyperLinkFormatOptionsTarget.Blank.ToString() + "\"");
    }
}
