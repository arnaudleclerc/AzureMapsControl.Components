namespace AzureMapsControl.Components.Tests.Layers
{
    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Markers;

    using Xunit;

    public class HtmlMarkerLayerCallbackInvokeHelperTests
    {
        [Fact]
        public async void Should_GenerateMarkerOnCallbackAsync()
        {
            var service = new HtmlMarkerLayerCallbackInvokeHelper((id, position, properties) => {
                return new HtmlMarker(id, new HtmlMarkerOptions {
                    Position = position
                });
            });

            var id = "id";
            var position = new Position(0, 0);
            var marker = await service.InvokeMarkerCallbackAsync(id, position, null);
            Assert.Equal(id, marker.Id);
            Assert.Equal(position, marker.Options.Position);
        }
    }
}
