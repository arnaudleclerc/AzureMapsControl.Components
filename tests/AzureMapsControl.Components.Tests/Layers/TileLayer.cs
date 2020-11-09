namespace AzureMapsControl.Components.Tests.Layers
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Map;

    using Xunit;

    public class TileLayerTests
    {
        [Fact]
        public void Should_HaveDefaultId()
        {
            var layer = new TileLayer();
            Assert.NotEmpty(layer.Id);
            Assert.Equal(LayerType.TileLayer, layer.Type);
            Assert.Empty(layer.EventActivationFlags.EnabledEvents);
        }

        [Fact]
        public void Should_HaveGivenId()
        {
            const string id = "id";
            var layer = new TileLayer(id);
            Assert.Equal(id, layer.Id);
            Assert.Equal(LayerType.TileLayer, layer.Type);
            Assert.Empty(layer.EventActivationFlags.EnabledEvents);
        }

        [Fact]
        public void Should_TriggerClick()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForMouseEvent("click");
            layer.OnClick += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerContextMenu()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForMouseEvent("contextmenu");
            layer.OnContextMenu += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerDblClick()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForMouseEvent("dblclick");
            layer.OnDblClick += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerLayerAdded()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = new MapJsEventArgs {
                Type = "layeradded"
            };
            layer.OnLayerAdded += args => triggered = args.Map == map && args.Type == eventArgs.Type;
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerLayerRemoved()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = new MapJsEventArgs {
                Type = "layerremoved"
            };
            layer.OnLayerRemoved += args => triggered = args.Map == map && args.Type == eventArgs.Type;
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerMouseDown()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForMouseEvent("mousedown");
            layer.OnMouseDown += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerMouseEnter()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForMouseEvent("mouseenter");
            layer.OnMouseEnter += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerMouseLeave()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForMouseEvent("mouseleave");
            layer.OnMouseLeave += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerMouseMove()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForMouseEvent("mousemove");
            layer.OnMouseMove += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerMouseOut()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForMouseEvent("mouseout");
            layer.OnMouseOut += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerMouseOver()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForMouseEvent("mouseover");
            layer.OnMouseOver += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerMouseUp()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForMouseEvent("mouseup");
            layer.OnMouseUp += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerTouchCancel()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForTouchEvent("touchcancel");
            layer.OnTouchCancel += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Pixels == eventArgs.Pixels
                    && args.Positions == eventArgs.Positions
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerTouchEnd()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForTouchEvent("touchend");
            layer.OnTouchEnd += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Pixels == eventArgs.Pixels
                    && args.Positions == eventArgs.Positions
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerTouchMove()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForTouchEvent("touchmove");
            layer.OnTouchMove += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Pixels == eventArgs.Pixels
                    && args.Positions == eventArgs.Positions
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerTouchStart()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = CreateEventArgsForTouchEvent("touchstart");
            layer.OnTouchStart += args => {
                triggered = args.Map == map
                    && args.LayerId == eventArgs.LayerId
                    && args.Pixel == eventArgs.Pixel
                    && args.Position == eventArgs.Position
                    && args.Shapes == eventArgs.Shapes
                    && args.Pixels == eventArgs.Pixels
                    && args.Positions == eventArgs.Positions
                    && args.Type == eventArgs.Type;
            };
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        [Fact]
        public void Should_TriggerWheel()
        {
            var layer = new TileLayer();
            var triggered = false;
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var eventArgs = new MapJsEventArgs {
                Type = "wheel"
            };
            layer.OnWheel += args => triggered = args.Map == map && args.Type == eventArgs.Type;
            layer.DispatchEvent(map, eventArgs);
            Assert.True(triggered);
        }

        private MapJsEventArgs CreateEventArgsForMouseEvent(string type) => new MapJsEventArgs {
            LayerId = "layerId",
            Pixel = new Pixel(),
            Position = new Position(),
            Shapes = new List<Feature>(),
            Type = type
        };

        private MapJsEventArgs CreateEventArgsForTouchEvent(string type) => new MapJsEventArgs {
            LayerId = "layerId",
            Pixel = new Pixel(),
            Pixels = new List<Pixel>(),
            Position = new Position(),
            Positions = new List<Position>(),
            Shapes = new List<Feature>(),
            Type = type
        };
    }
}
