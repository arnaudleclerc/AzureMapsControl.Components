
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.Drawing;
[ExcludeFromCodeCoverage]
internal class DrawingToolbarEventInvokeHelper : EventInvokeHelper<DrawingToolbarJsEventArgs>
{
    public DrawingToolbarEventInvokeHelper(Func<DrawingToolbarJsEventArgs, ValueTask> callback) : base(callback) { }
}
