namespace AzureMapsControl.Components.Tests.Layers
{
    using System.Linq;

    using AzureMapsControl.Components.Layers;

    using Xunit;

    public class LayerEventActivationFlagsTests
    {
        [Fact]
        public void Should_HaveEverything_Disabled()
        {
            var layerEventActivationFlags = LayerEventActivationFlags.None();
            Assert.Empty(layerEventActivationFlags.EnabledEvents);
        }

        [Fact]
        public void Should_HaveEverything_Enabled()
        {
            var layerEventActivationFlags = LayerEventActivationFlags.All();
            Assert.NotEmpty(layerEventActivationFlags.EnabledEvents);
        }

        [Fact]
        public void Should_EnableOne()
        {
            var layerEventActivationFlags = LayerEventActivationFlags.None().Enable(LayerEventType.Click);
            Assert.Single(layerEventActivationFlags.EnabledEvents);
            Assert.Equal(layerEventActivationFlags.EnabledEvents.First(), LayerEventType.Click.ToString());
        }

        [Fact]
        public void Should_DisableOne()
        {
            var layerEventActivationFlags = LayerEventActivationFlags.All().Disable(LayerEventType.Click);
            Assert.NotEmpty(layerEventActivationFlags.EnabledEvents);
            Assert.DoesNotContain(layerEventActivationFlags.EnabledEvents.First(), LayerEventType.Click.ToString());
        }
    }
}
