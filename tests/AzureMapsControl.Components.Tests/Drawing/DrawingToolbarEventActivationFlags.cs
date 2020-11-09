namespace AzureMapsControl.Components.Tests.Drawing
{
    using System.Linq;

    using AzureMapsControl.Components.Drawing;

    using Xunit;

    public class DrawingToolbarEventActivationFlagsTests
    {
        [Fact]
        public void Should_HaveEverything_Disabled()
        {
            var drawingToolbarEventActionsFlags = DrawingToolbarEventActivationFlags.None();
            Assert.Empty(drawingToolbarEventActionsFlags.EnabledEvents);
        }

        [Fact]
        public void Should_HaveEverything_Enabled()
        {
            var drawingToolbarEventActionsFlags = DrawingToolbarEventActivationFlags.All();
            Assert.NotEmpty(drawingToolbarEventActionsFlags.EnabledEvents);
        }

        [Fact]
        public void Should_EnableOne()
        {
            var drawingToolbarEventActionsFlags = DrawingToolbarEventActivationFlags.None().Enable(DrawingToolbarEventType.DrawingChanged);
            Assert.Single(drawingToolbarEventActionsFlags.EnabledEvents);
            Assert.Equal(drawingToolbarEventActionsFlags.EnabledEvents.First(), DrawingToolbarEventType.DrawingChanged.ToString());
        }

        [Fact]
        public void Should_DisableOne()
        {
            var drawingToolbarEventActionsFlags = DrawingToolbarEventActivationFlags.All().Disable(DrawingToolbarEventType.DrawingChanged);
            Assert.NotEmpty(drawingToolbarEventActionsFlags.EnabledEvents);
            Assert.DoesNotContain(drawingToolbarEventActionsFlags.EnabledEvents.First(), DrawingToolbarEventType.DrawingChanged.ToString());
        }
    }
}
