namespace AzureMapsControl.Components.Tests.Atlas
{
    using AzureMapsControl.Components.Atlas;

    using Xunit;

    public class EitherTests
    {
        [Fact]
        public void Should_HaveFirstChoice()
        {
            var choice = "choice";
            var either = new Either<string, int>(choice);
            Assert.Equal(choice, either.FirstChoice);
            Assert.Equal(default, either.SecondChoice);
            Assert.True(either.HasFirstChoice);
        }

        [Fact]
        public void Should_HaveSecondChoice()
        {
            var choice = 1;
            var either = new Either<string, int>(choice);
            Assert.Equal(default, either.FirstChoice);
            Assert.Equal(choice, either.SecondChoice);
            Assert.False(either.HasFirstChoice);
        }
    }
}
