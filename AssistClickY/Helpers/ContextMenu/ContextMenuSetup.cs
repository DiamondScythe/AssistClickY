using AssistClickY.Helpers.Mouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssistClickY.Helpers.ContextMenu
{
    public static class ContextMenuSetup
    {
        public static ContextMenuStrip CreateContextMenu()
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("HotKeyAction", null, (sender, e) => MouseRecorder.HotkeyAction());
            // Add other context menu items here
            return contextMenu;
        }
    }
}
