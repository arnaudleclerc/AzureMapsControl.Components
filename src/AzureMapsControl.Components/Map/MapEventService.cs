namespace AzureMapsControl.Map
{
    internal class MapEventService
    {
        public event MapErrorEventHandler OnMapError;
        public event MapEventHandler OnMapReady;

        internal void DispatchEvent(MapEventArgs mapEvent)
        {
            switch (mapEvent.Type)
            {
                case "ready":
                    OnMapReady?.Invoke(mapEvent);
                    break;

                case "error":
                    OnMapError?.Invoke(mapEvent as MapErrorEventArgs);
                    break;
            }
        }
    }
}
