namespace AzureMapsControl.Components.Indoor
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public struct IndoorLayerTheme
    {
        private readonly string _theme;

        public static readonly IndoorLayerTheme Auto = new IndoorLayerTheme("auto");
        public static readonly IndoorLayerTheme Dark = new IndoorLayerTheme("dark");
        public static readonly IndoorLayerTheme Light = new IndoorLayerTheme("light");

        private IndoorLayerTheme(string theme) => _theme = theme;

        public override string ToString() => _theme;
    }
}
