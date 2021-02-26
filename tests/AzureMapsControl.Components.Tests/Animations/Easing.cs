namespace AzureMapsControl.Components.Tests.Animations
{
    using AzureMapsControl.Components.Animations.Options;

    using Xunit;

    public class EasingTests
    {
        [Theory]
        [InlineData("linear")]
        [InlineData("easeInSine")]
        [InlineData("easeOutSine")]
        [InlineData("easeInOutSine")]
        [InlineData("easeInQuad")]
        [InlineData("easeOutQuad")]
        [InlineData("easeInOutQuad")]
        [InlineData("easeInCubic")]
        [InlineData("easeOutCubic")]
        [InlineData("easeInOutCubic")]
        [InlineData("easeInQuart")]
        [InlineData("easeOutQuart")]
        [InlineData("easeInOutQuart")]
        [InlineData("easeInQuint")]
        [InlineData("easeOutQuint")]
        [InlineData("easeInOutQuint")]
        [InlineData("easeInExpo")]
        [InlineData("easeOutExpo")]
        [InlineData("easeInOutExpo")]
        [InlineData("easeInCirc")]
        [InlineData("easeOutCirc")]
        [InlineData("easeInOutCirc")]
        [InlineData("easeInBack")]
        [InlineData("easeOutBack")]
        [InlineData("easeInOutBack")]
        [InlineData("easeInElastic")]
        [InlineData("easeOutElastic")]
        [InlineData("easeInOutElastic")]
        [InlineData("easeInBounce")]
        [InlineData("easeOutBounce")]
        [InlineData("easeInOutBounce")]
        public static void Should_ReturnEasingFromString(string easingType)
        {
            var easing = Easing.FromString(easingType);
            Assert.Equal(easingType, easing.ToString());
        }

        [Fact]
        public static void Should_ReturnDefaultEasing_IfStringDoesNotMatch()
        {
            var easingType = "obviouslyNotAValidOne";
            var easing = Easing.FromString(easingType);
            Assert.Equal(default, easing);
        }
    }
}
