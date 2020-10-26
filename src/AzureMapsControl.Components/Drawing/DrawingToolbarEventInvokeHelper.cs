namespace AzureMapsControl.Components.Drawing
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Events;

    internal class DrawingToolbarEventInvokeHelper : EventInvokeHelper<DrawingToolbarJsEventArgs>
    {
        public DrawingToolbarEventInvokeHelper(Func<DrawingToolbarJsEventArgs, Task> callback) : base(callback) { }
    }
}
