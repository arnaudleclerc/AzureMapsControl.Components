namespace AzureMapsControl.Components.Guards
{
    using System;

    internal static class Require
    {
        internal static void NotNull(object element, string name)
        {
            if (element == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
