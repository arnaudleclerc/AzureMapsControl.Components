namespace AzureMapsControl.Components.Configuration
{
    public class AzureMapsConfiguration
    {
        public string AadAppId { get; set; }
        public string AadTenant { get; set; }
        public string ClientId { get; set; }
        public string SubscriptionKey { get; set; }

        internal string AuthType
        {
            get {
                if (!string.IsNullOrWhiteSpace(SubscriptionKey))
                {
                    return "subscriptionKey";
                }

                return !string.IsNullOrWhiteSpace(AadAppId)
                    && !string.IsNullOrWhiteSpace(AadTenant)
                    && !string.IsNullOrWhiteSpace(ClientId)
                    ? "aad"
                    : "anonymous";
            }
        }
    }
}
