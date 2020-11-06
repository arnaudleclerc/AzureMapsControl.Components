namespace AzureMapsControl.Components.Configuration
{
    /// <summary>
    /// Options for specifying how the map control should authenticate with the Azure Maps services.
    /// </summary>
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class AzureMapsConfiguration
    {
        /// <summary>
        /// The Azure AD registered app ID. This is the app ID of an app registered in your Azure AD tenant.
        /// </summary>
        public string AadAppId { get; set; }

        /// <summary>
        /// The AAD tenant that owns the registered app specified by `aadAppId`.
        /// </summary>
        public string AadTenant { get; set; }

        /// <summary>
        /// The Azure Maps client ID, This is an unique identifier used to identify the maps account.
        /// Must be specified for AAD and anonymous authentication types.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Subscription key from your Azure Maps account.
        /// Must be specified for subscription key authentication type.
        /// </summary>
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
