namespace AzureMapsControl.Components.Atlas
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas.FormatOptions;

    /// <summary>
    /// Species details of how a property is to be displayed.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class PropertyInfo
    {
        /// <summary>
        /// The path to the property with each sub-property separated with a forward slash "/", for example "property/subproperty1/subproperty2.
        /// Array indices can be added as subproperties as well, for example "property/0"
        /// </summary>
        public string PropertyPath { get; set; }

        /// <summary>
        /// The label to display for this property. If not specified, will fallback to the property name.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Specifies if the label of this property should be hidden and the content should span both columns of the table.
        /// </summary>
        public bool? HideLabel { get; set; }

        /// <summary>
        /// If the property is a date object, these options specify how it should be formatted when displayed.
        /// If not specified, will fallback to the popup templates date format setting.
        /// </summary>
        public DateTimeFormatOptions DateFormat { get; set; }

        /// <summary>
        /// If the property is a number, these options specify how it should be formatted when displayed.
        /// If not specified, will fallback to the popup templates number format setting.
        /// </summary>
        public NumberFormatOptions NumberFormat { get; set; }

        /// <summary>
        /// Format options for hyperlink strings
        /// </summary>
        public HyperLinkFormatOptions HyperlinkFormat { get; set; }
    }
}
