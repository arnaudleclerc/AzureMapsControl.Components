namespace AzureMapsControl.Configuration
{
    public class AzureMapConfiguration
    {
        public string SubscriptionKey { get; set; }

        internal bool Validate() => !string.IsNullOrWhiteSpace(SubscriptionKey);
    }
}
