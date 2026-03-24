
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.Data;
internal sealed class DataSourceEventInvokeHelper(Func<DataSourceEventArgs, ValueTask> callback) : EventInvokeHelper<DataSourceEventArgs>(callback);
