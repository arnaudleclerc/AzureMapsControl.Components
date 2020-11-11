namespace AzureMapsControl.Components.Tests.Popups
{
    using AzureMapsControl.Components.Exceptions;
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

        [Fact]
        public async void Should_CloseAsync()
        {
            var assertClose = false;
            var popup = new Popup();
            popup.ClosePopupCallback = async popupId => assertClose = popupId == popup.Id;
            await popup.CloseAsync();
            Assert.True(assertClose);
        }

        [Fact]
        public async void Should_RemoveAsync()
        {
            var assertRemove = false;
            var popup = new Popup();
            popup.RemoveAsyncCallback = async popupId => assertRemove = popupId == popup.Id;
            await popup.RemoveAsync();
            Assert.True(assertRemove);
        }

        [Fact]
        public async void Should_NotRemoveTwice_Async()
        {
            var popup = new Popup();
            popup.RemoveAsyncCallback = async _ => { };
            await popup.RemoveAsync();
            await Assert.ThrowsAnyAsync<PopupAlreadyRemovedException>(async () => await popup.RemoveAsync());
        }
    }
}
