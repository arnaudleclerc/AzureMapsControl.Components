namespace AzureMapsControl.Components.Tests.Popups
{
    using AzureMapsControl.Components.Popups;

    using Xunit;

    public class PopupTests
    {
        [Fact]
        public void Should_BeInitialized_DefaultId()
        {
            var popup = new Popup();
            Assert.NotNull(popup.Id);
        }

        [Fact]
        public void Should_BeInitialized()
        {
            const string id = "id";
            var popup = new Popup(id);
            Assert.Equal(id, popup.Id);
        }

        [Fact]
        public async void Should_OpenAsync()
        {
            var assertOpen = false;
            var popup = new Popup();
            popup.OpenPopupCallback = async popupId => assertOpen = popupId == popup.Id;
            await popup.OpenAsync();
            Assert.True(assertOpen);
        }
    }
}
