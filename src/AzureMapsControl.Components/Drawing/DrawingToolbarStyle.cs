namespace AzureMapsControl.Components.Drawing
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class DrawingToolbarStyle
    {
        private readonly string _style;

        public static readonly DrawingToolbarStyle Dark = new DrawingToolbarStyle("dark");
        public static readonly DrawingToolbarStyle Light = new DrawingToolbarStyle("light");

        public DrawingToolbarStyle(string style) => _style = style;

        public override string ToString() => _style;
    }
}
