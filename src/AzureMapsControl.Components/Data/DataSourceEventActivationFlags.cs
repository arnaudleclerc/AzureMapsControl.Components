namespace AzureMapsControl.Components.Data
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Events;

    public sealed class DataSourceEventActivationFlags : EventActivationFlags<DataSourceEventType, DataSourceEventActivationFlags>
    {
        private DataSourceEventActivationFlags(bool defaultFlag) :
            base
            (
                new Dictionary<DataSourceEventType, bool>
                {
                    { DataSourceEventType.DataAdded, defaultFlag },
                    { DataSourceEventType.DataRemoved, defaultFlag },
                    { DataSourceEventType.DataSourceUpdated, defaultFlag },
                    { DataSourceEventType.SourceAdded, defaultFlag },
                    { DataSourceEventType.SourceRemoved, defaultFlag },
                }
            )
        { }

        public static DataSourceEventActivationFlags All() => new DataSourceEventActivationFlags(true);
        public static DataSourceEventActivationFlags None() => new DataSourceEventActivationFlags(false);
    }
}
