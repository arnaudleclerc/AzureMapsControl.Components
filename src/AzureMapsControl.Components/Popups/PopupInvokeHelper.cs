
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.Popups;
internal sealed class PopupInvokeHelper(Func<PopupEventArgs, ValueTask> callback) : EventInvokeHelper<PopupEventArgs>(callback);
