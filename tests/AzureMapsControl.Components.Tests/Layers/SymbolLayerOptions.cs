

using AzureMapsControl.Components.Layers;
using AzureMapsControl.Components.Tests.Json;

using Xunit;

namespace AzureMapsControl.Components.Tests.Layers;
public class SymbolLayerOptionsJsonConverterTests : JsonConverterTests<SymbolLayerOptions>
{
    public SymbolLayerOptionsJsonConverterTests() : base(new SymbolLayerOptionsJsonConverter()) { }

    [Fact]
    public void Should_Write()
    {
        var options = new SymbolLayerOptions {
            Filter = new Components.Atlas.ExpressionOrString("filter"),
            IconOptions = new IconOptions {
                AllowOverlap = true
            },
            LineSpacing = new Components.Atlas.ExpressionOrNumber(1),
            MaxZoom = 2,
            MinZoom = 3,
            Placement = SymbolLayerPlacement.Line,
            Source = "source",
            SourceLayer = "sourceLayer",
            TextOptions = new TextOptions {
                AllowOverlap = true
            },
            Visible = true
        };

        var expectedJson = "{"
            + "\"filter\":\"filter\""
            + ",\"maxZoom\":2"
            + ",\"minZoom\":3"
            + ",\"visible\":true"
            + ",\"source\":\"source\""
            + ",\"sourceLayer\":\"sourceLayer\""
            + ",\"iconOptions\":{"
            + "\"allowOverlap\":true"
            + "}"
            + ",\"lineSpacing\":1"
            + ",\"placement\":\"line\""
            + ",\"textOptions\":{"
            + "\"allowOverlap\":true"
            + "}"
            + "}";

        TestAndAssertWrite(options, expectedJson);
    }

    [Fact]
    public void Should_WriteNull() => TestAndAssertWrite(null, "null");

    [Fact]
    public void Should_Read()
    {
        var json = "{"
            + "\"filter\":\"filter\""
            + ",\"maxZoom\":2"
            + ",\"minZoom\":3"
            + ",\"visible\":true"
            + ",\"source\":\"source\""
            + ",\"sourceLayer\":\"sourceLayer\""
            + ",\"iconOptions\":{"
            + "\"allowOverlap\":true"
            + "}"
            + ",\"lineSpacing\":1"
            + ",\"placement\":\"line\""
            + ",\"textOptions\":{"
            + "\"allowOverlap\":true"
            + "}"
            + "}";

        var result = Read(json);
        Assert.Null(result.Filter);
        Assert.Equal(2, result.MaxZoom);
        Assert.Equal(3, result.MinZoom);
        Assert.True(result.Visible);
        Assert.Equal("source", result.Source);
        Assert.Equal("sourceLayer", result.SourceLayer);
        Assert.True(result.IconOptions.AllowOverlap);
        Assert.Null(result.LineSpacing);
        Assert.Equal(SymbolLayerPlacement.Line, result.Placement);
        Assert.True(result.TextOptions.AllowOverlap);
        Assert.True(result.Visible);
    }
}
