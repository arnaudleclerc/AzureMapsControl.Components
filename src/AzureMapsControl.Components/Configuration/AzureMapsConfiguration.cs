namespace AzureMapsControl.Components.Configuration
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Options for specifying how the map control should authenticate with the Azure Maps services.
    /// </summary>
    [JsonConverter(typeof(AzureMapsConfigurationJsonConverter))]
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

    internal sealed class AzureMapsConfigurationJsonConverter : JsonConverter<AzureMapsConfiguration>
    {
        public override AzureMapsConfiguration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, AzureMapsConfiguration value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("authType", value.AuthType);
            if (value.AuthType == "subscriptionKey")
            {
                writer.WriteString("subscriptionKey", value.SubscriptionKey);
            }
            else if (value.AuthType == "aad")
            {
                writer.WriteString("aadAppId", value.AadAppId);
                writer.WriteString("aadTenant", value.AadTenant);
                writer.WriteString("clientId", value.ClientId);
            }
            writer.WriteEndObject();
        }
    }
}
