namespace AzureMapsControl.Components.Map
{
    using System.Threading.Tasks;

    internal delegate Task MapReadyEvent();

    internal class MapService
    {
        internal Map Map
        {
            get;
            private set;
        }

        public event MapReadyEvent OnMapReadyAsync;

        internal async Task AddMapAsync(Map map)
        {
            Map = map;

            if (OnMapReadyAsync != null)
            {
                await OnMapReadyAsync.Invoke();
            }
        }

    }
}
