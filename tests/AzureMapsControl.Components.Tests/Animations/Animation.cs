namespace AzureMapsControl.Components.Tests.Animations
{
    using System;
    using System.Collections.Generic;

    using AzureMapsControl.Components.Animations;
    using AzureMapsControl.Components.Animations.Options;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;

    public class AnimationTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new();

        internal class AnimationFactory
        {
            internal static Animation GetAnimation(string type, string id, IMapJsRuntime runtime)
            {
                return type switch {
                    "dropmarkers" => new DropMarkersAnimation(id, runtime),
                    "flowingdashed" => new FlowingDashedLineAnimation(id, runtime),
                    "movealongpath" => new MoveAlongPathAnimation(id, runtime),
                    "snakeline" => new SnakeLineAnimation(id, runtime),
                    "group" => new GroupAnimation(id, runtime),
                    "drop" => new DropAnimation(id, runtime),
                    "setcoordinates" => new SetCoordinatesAnimation(id, runtime),
                    "morph" => new MorphAnimation(id, runtime),
                    "movealongroute" => new MoveAlongRouteAnimation(id, runtime),
                    _ => throw new NotSupportedException(type),
                };
            }
        }

        public static IEnumerable<object[]> AllAnimationsTypes =>
            new List<object[]> {
                new object[] { "dropmarkers" },
                new object[] { "flowingdashed" },
                new object[] { "movealongpath" },
                new object[] { "snakeline" },
                new object[] { "group" },
                new object[] { "drop" },
                new object[] { "setcoordinates" },
                new object[] { "morph" },
                new object[] { "movealongroute" }
            };

        public static IEnumerable<object[]> AllSeekAnimationsTypes =>
            new List<object[]> {
                new object[] { "dropmarkers" },
                new object[] { "movealongpath" },
                new object[] { "snakeline" },
                new object[] { "drop" },
                new object[] { "setcoordinates" },
                new object[] { "morph" },
            };

        public static IEnumerable<object[]> AllPauseAnimationsTypes =>
                   new List<object[]> {
                new object[] { "dropmarkers" },
                new object[] { "flowingdashed" },
                new object[] { "movealongpath" },
                new object[] { "snakeline" },
                new object[] { "drop" },
                new object[] { "setcoordinates" },
                new object[] { "morph" },
        };

        public static IEnumerable<object[]> AllPlayAnimationsTypes =>
            new List<object[]> {
                new object[] { "dropmarkers" },
                new object[] { "flowingdashed" },
                new object[] { "movealongpath" },
                new object[] { "snakeline" },
                new object[] { "group" },
                new object[] { "drop" },
                new object[] { "setcoordinates" },
                new object[] { "morph" }
            };

        public static IEnumerable<object[]> AllResetAnimationsTypes =>
            new List<object[]> {
                new object[] { "dropmarkers" },
                new object[] { "flowingdashed" },
                new object[] { "movealongpath" },
                new object[] { "snakeline" },
                new object[] { "group" },
                new object[] { "drop" },
                new object[] { "setcoordinates" },
                new object[] { "morph" }
            };

        public static IEnumerable<object[]> AllStopAnimationsTypes =>
            new List<object[]> {
                new object[] { "dropmarkers" },
                new object[] { "flowingdashed" },
                new object[] { "movealongpath" },
                new object[] { "snakeline" },
                new object[] { "group" },
                new object[] { "drop" },
                new object[] { "setcoordinates" },
                new object[] { "morph" }
            };

        [Theory]
        [MemberData(nameof(AllAnimationsTypes))]
        public void Should_Create(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            Assert.Equal(id, animation.Id);
            Assert.False(animation.Disposed);
        }

        [Theory]
        [MemberData(nameof(AllAnimationsTypes))]
        public async void Should_DisposeAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            await animation.DisposeAsync();
            Assert.True(animation.Disposed);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Dispose.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllAnimationsTypes))]
        public async void Should_ThrowAnimationDisposedException_DisposeAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            animation.Disposed = true;
            await Assert.ThrowsAsync<AnimationDisposedException>(async () => await animation.DisposeAsync());

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Dispose.ToAnimationNamespace(), id), Times.Never);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllPauseAnimationsTypes))]
        public async void Should_PauseAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            await animation.PauseAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Pause.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllPauseAnimationsTypes))]
        public async void Should_ThrowAnimationDisposedException_PauseAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            animation.Disposed = true;
            await Assert.ThrowsAsync<AnimationDisposedException>(async () => await animation.PauseAsync());

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Pause.ToAnimationNamespace(), id), Times.Never);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllPlayAnimationsTypes))]
        public async void Should_PlayAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            await animation.PlayAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Play.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllPlayAnimationsTypes))]
        public async void Should_ThrowAnimationDisposedException_PlayAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            animation.Disposed = true;
            await Assert.ThrowsAsync<AnimationDisposedException>(async () => await animation.PlayAsync());

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Play.ToAnimationNamespace(), id), Times.Never);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllResetAnimationsTypes))]
        public async void Should_ResetAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            await animation.ResetAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Reset.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllResetAnimationsTypes))]
        public async void Should_ThrowAnimationDisposedException_ResetAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            animation.Disposed = true;
            await Assert.ThrowsAsync<AnimationDisposedException>(async () => await animation.ResetAsync());

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Reset.ToAnimationNamespace(), id), Times.Never);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllSeekAnimationsTypes))]
        public async void Should_SeekAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            var seek = 0.5m;
            await animation.SeekAsync(seek);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Seek.ToAnimationNamespace(), id, seek), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllSeekAnimationsTypes))]
        public async void Should_ThrowAnimationDisposedException_SeekAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            var seek = 0.5m;
            animation.Disposed = true;
            await Assert.ThrowsAsync<AnimationDisposedException>(async () => await animation.SeekAsync(seek));

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Seek.ToAnimationNamespace(), id, seek), Times.Never);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllStopAnimationsTypes))]
        public async void Should_StopAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            await animation.StopAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Stop.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(AllStopAnimationsTypes))]
        public async void Should_ThrowAnimationDisposedException_StopAsync(string animationType)
        {
            var id = "id";
            var animation = AnimationFactory.GetAnimation(animationType, id, _jsRuntime.Object);
            animation.Disposed = true;
            await Assert.ThrowsAsync<AnimationDisposedException>(async () => await animation.StopAsync());

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Stop.ToAnimationNamespace(), id), Times.Never);
            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
