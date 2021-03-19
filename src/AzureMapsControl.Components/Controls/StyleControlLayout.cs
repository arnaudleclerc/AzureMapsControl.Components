namespace AzureMapsControl.Components.Controls
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The layout to display the styles in.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class StyleControlLayout
    {
        private readonly string _layout;

        /// <summary>
        /// A row of clickable icons for each style.
        /// </summary>
        public static StyleControlLayout Icons = new StyleControlLayout("icons");

        /// <summary>
        /// A scrollable list with the icons and names for each style.
        /// </summary>
        public static StyleControlLayout List = new StyleControlLayout("list");

        private StyleControlLayout(string layout) => _layout = layout;

        public override string ToString() => _layout;
    }
}
