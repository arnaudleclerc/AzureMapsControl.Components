namespace AzureMapsControl.Configuration
{
    public class AzureMapsConfiguration
    {
        public string SubscriptionKey { get; set; }

        internal bool Validate() => !string.IsNullOrWhiteSpace(SubscriptionKey);
    }
}
