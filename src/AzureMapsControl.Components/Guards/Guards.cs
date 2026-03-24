namespace AzureMapsControl.Components.Guards
{
    using System;

    internal static class Require
    {
        internal static void NotNull(object element, string name) =>
            ArgumentNullException.ThrowIfNull(element, name);

        internal static void NotNullOrWhiteSpace(string element, string name) =>
            ArgumentException.ThrowIfNullOrWhiteSpace(element, name);
    }
}
