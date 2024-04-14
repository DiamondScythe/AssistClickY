using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistClickY.Helpers.Mouse
{
    internal class MouseManager
    {

        public static System.Drawing.Point GetCursorPosition()
        {
            System.Drawing.Point lpPoint = System.Windows.Forms.Control.MousePosition;


            return lpPoint;
        }
    }
}