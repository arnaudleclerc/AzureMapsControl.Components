
using AzureMapsControl.Components.Layers;

using Xunit;

namespace AzureMapsControl.Components.Tests.Layers;
public class SymbolLayerPlacementTests
{
    [Theory]
    [InlineData("line")]
    [InlineData("line-center")]
    [InlineData("point")]
    public static void Should_ReturnSymbolLayerPlacementFromString(string symbolLayerPlacementType)
    {
        var symbolLayerPlacement = SymbolLayerPlacement.FromString(symbolLayerPlacementType);
        Assert.Equal(symbolLayerPlacementType, symbolLayerPlacement.ToString());
    }

    [Fact]
    public static void Should_ReturnDefaultSymbolLayerPlacement_IfStringDoesNotMatch()
    {
        var symbolLayerPlacementType = "obviouslyNotAValidOne";
        var symbolLayerPlacement = SymbolLayerPlacement.FromString(symbolLayerPlacementType);
        Assert.Equal(default, symbolLayerPlacement);
    }
}
