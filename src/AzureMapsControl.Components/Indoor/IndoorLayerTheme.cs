namespace AzureMapsControl.Components.Indoor
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public struct IndoorLayerTheme
    {
        private readonly string _theme;

        public static readonly IndoorLayerTheme Auto = new("auto");
        public static readonly IndoorLayerTheme Dark = new("dark");
        public static readonly IndoorLayerTheme Light = new("light");

        private IndoorLayerTheme(string theme) => _theme = theme;

        public override string ToString() => _theme;

        /// <summary>
        /// Return an IndoorLayerTheme corresponding to the given value
        /// </summary>
        /// <param name="theme">Value of the IndoorLayerTheme</param>
        /// <returns>IndoorLayerTheme corresponding to the given value. If none was found, returns `default`</returns>
        public static IndoorLayerTheme FromString(string theme)
        {
            return theme switch {
                "auto" => Auto,
                "dark" => Dark,
                "light" => Light,
                _ => default
            };
        }
    }
}
