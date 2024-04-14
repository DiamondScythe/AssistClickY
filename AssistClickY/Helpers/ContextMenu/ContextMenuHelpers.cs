using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AssistClickY.Helpers.Mouse;

namespace AssistClickY.Helpers.ContextMenu
{
    public static class ContextMenuHelpers
    {
        public static void ToggleTrayMenu(NotifyIcon nfIcon)
        {
            ContextMenuStrip cmsTray = nfIcon.ContextMenuStrip;

            if (cmsTray != null && !cmsTray.IsDisposed)
            {
                if (cmsTray.Visible)
                {
                    cmsTray.Close();
                }
                else
                {
                    //NativeMethods.SetForegroundWindow(cmsTray.Handle);
                    cmsTray.Show(MouseManager.GetCursorPosition());
                }
            }
        }
    }
}
