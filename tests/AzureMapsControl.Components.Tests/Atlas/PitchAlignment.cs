
using AzureMapsControl.Components.Atlas;

using Xunit;

namespace AzureMapsControl.Components.Tests.Atlas;
public class PitchAlignmentTests
{
    [Theory]
    [InlineData("auto")]
    [InlineData("map")]
    [InlineData("viewport")]
    public static void Should_ReturnPitchAlignmentFromString(string pitchAlignmentType)
    {
        var pitchAlignment = PitchAlignment.FromString(pitchAlignmentType);
        Assert.Equal(pitchAlignmentType, pitchAlignment.ToString());
    }

    [Fact]
    public static void Should_ReturnDefaultPitchAlignment_IfStringDoesNotMatch()
    {
        var interpolationType = "obviouslyNotAValidOne";
        var interpolation = PitchAlignment.FromString(interpolationType);
        Assert.Equal(default, interpolation);
    }
}
