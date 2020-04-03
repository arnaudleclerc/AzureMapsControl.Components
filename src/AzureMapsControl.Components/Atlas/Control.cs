namespace AzureMapsControl.Components.Atlas
{
    using System;

    public class Control
    {
        internal ControlType Type { get; }
        internal ControlPosition Position { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="controlType">Control to add</param>
        /// <param name="controlPosition">Position where the control will be placed</param>
        public Control(ControlType controlType, ControlPosition controlPosition)
        {
            if (controlType == null)
            {
                throw new ArgumentNullException(nameof(controlType));
            }

            if (controlPosition == null)
            {
                throw new ArgumentNullException(nameof(controlPosition));
            }

            Type = controlType;
            Position = controlPosition;
        }
    }
}
