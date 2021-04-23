namespace AzureMapsControl.Components.Atlas.FormatOptions
{
    public struct LocaleMatcherFormatOptions
    {
        private string _type;

        private LocaleMatcherFormatOptions(string type) => _type = type;

        public override string ToString() => _type;
    }
}
