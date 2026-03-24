
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.Drawing;
[ExcludeFromCodeCoverage]
internal sealed class DrawingToolbarEventInvokeHelper(Func<DrawingToolbarJsEventArgs, ValueTask> callback) : EventInvokeHelper<DrawingToolbarJsEventArgs>(callback);
