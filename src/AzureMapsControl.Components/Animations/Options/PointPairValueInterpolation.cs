namespace AzureMapsControl.Components.Animations.Options
{
    /// <summary>
    /// Defines how the value of property in two points is extrapolated.
    /// </summary>
    public struct PointPairValueInterpolation
    {
        /// <summary>
        /// How the interpolation is performed. Certain interpolations require the data to be a certain value.
        /// </summary>
        public Interpolation Interpolation { get; set; }

        /// <summary>
        /// The path to the property with each sub-property separated with a forward slash "/", for example "property/subproperty1/subproperty2".
        /// Array indices can be added as subproperties as well, for example "property/0".
        /// </summary>
        public string PropertyPath { get; set; }
    }
}
