using AssistClickY.Helpers.Mouse;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AssistClickY.Helpers.Mouse
{
    internal class MouseRecorder
    {

        public static event EventHandler<string> MousePositionRecorded;

        public static void HotkeyAction()
        {
            System.Drawing.Point currentMousePos = MouseManager.GetCursorPosition();
            string mousePositionString = "Current mouse position is " + currentMousePos.X + "-" + currentMousePos.Y;
            MousePositionRecorded?.Invoke(null, mousePositionString);
        }

    }
}
