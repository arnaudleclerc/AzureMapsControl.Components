namespace AzureMapsControl.Components.Map
{
    using System.Threading.Tasks;

    public delegate Task MapReadyEvent();

    internal class MapService : IMapAdderService
    {
        public Map Map
        {
            get;
            private set;
        }

        public event MapReadyEvent OnMapReadyAsync;

        public async Task AddMapAsync(Map map)
        {
            Map = map;

            if (OnMapReadyAsync != null)
            {
                await OnMapReadyAsync.Invoke();
            }
        }

    }
}
