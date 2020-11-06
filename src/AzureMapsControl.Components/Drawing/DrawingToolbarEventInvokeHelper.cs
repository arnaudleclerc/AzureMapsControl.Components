namespace AzureMapsControl.Components.Drawing
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Events;

    [ExcludeFromCodeCoverage]
    internal class DrawingToolbarEventInvokeHelper : EventInvokeHelper<DrawingToolbarJsEventArgs>
    {
        public DrawingToolbarEventInvokeHelper(Func<DrawingToolbarJsEventArgs, Task> callback) : base(callback) { }
    }
}
