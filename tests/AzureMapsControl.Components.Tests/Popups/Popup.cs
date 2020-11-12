namespace AzureMapsControl.Components.Tests.Popups
{
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Popups;

    using Xunit;

    public class PopupTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_HaveId_IfInvalidOneIsGiven(string id)
        {
            var popup = new Popup(id, null);
            Assert.False(string.IsNullOrWhiteSpace(popup.Id));
        }

        [Fact]
        public void Should_BeInitialized_DefaultId()
        {
            var options = new PopupOptions();
            var popup = new Popup(options);
            Assert.NotNull(popup.Id);
            Assert.Equal(options, popup.Options);
        }

        [Fact]
        public void Should_BeInitialized()
        {
            const string id = "id";
            var options = new PopupOptions();
            var popup = new Popup(id, options);
            Assert.Equal(id, popup.Id);
            Assert.Equal(options, popup.Options);
        }

        [Fact]
        public async void Should_OpenAsync()
        {
            var assertOpen = false;
            var popup = new Popup(new PopupOptions());
            popup.OpenPopupCallback = async popupId => assertOpen = popupId == popup.Id;
            await popup.OpenAsync();
            Assert.True(assertOpen);
        }

        [Fact]
        public async void Should_CloseAsync()
        {
            var assertClose = false;
            var popup = new Popup(new PopupOptions());
            popup.ClosePopupCallback = async popupId => assertClose = popupId == popup.Id;
            await popup.CloseAsync();
            Assert.True(assertClose);
        }

        [Fact]
        public async void Should_RemoveAsync()
        {
            var assertRemove = false;
            var popup = new Popup(new PopupOptions());
            popup.RemoveCallback = async popupId => assertRemove = popupId == popup.Id;
            await popup.RemoveAsync();
            Assert.True(assertRemove);
        }

        [Fact]
        public async void Should_NotRemoveTwice_Async()
        {
            var popup = new Popup(new PopupOptions());
            popup.RemoveCallback = async _ => { };
            await popup.RemoveAsync();
            await Assert.ThrowsAnyAsync<PopupAlreadyRemovedException>(async () => await popup.RemoveAsync());
        }

        [Fact]
        public async void Should_Update_Async()
        {
            var assertUpdate = false;
            var popup = new Popup(new PopupOptions());
            var updatedContent = "updatedContent";
            popup.UpdateCallback = async (popupId, popupOptions) => assertUpdate = popupOptions == popup.Options && popup.Options.Content == updatedContent;
            await popup.UpdateAsync(options => options.Content = updatedContent);
            Assert.True(assertUpdate);
            Assert.Equal(updatedContent, popup.Options.Content);
        }
    }
}
