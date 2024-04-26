using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using AssistClickY.Helpers.ContextMenu;
using NHotkey;
using NHotkey.Wpf;

namespace AssistClickY.Helpers.ContextMenu
{
    public static class NotificationIconSetup
    {
        public static void SetupNotificationIcon(NotifyIcon nfIcon)
        {
            nfIcon.Icon = new System.Drawing.Icon("Resources/Images/4tan.ico");
            nfIcon.Text = "Click for settings";
            nfIcon.Click += (sender, e) => { /* Handle click event */ };
            nfIcon.Visible = true;
            nfIcon.ContextMenuStrip = ContextMenuSetup.CreateContextMenu();

            //HotkeyManager.AddHotkey(ModifierKeys.Shift, Key.A, () => { ContextMenuHelpers.ToggleTrayMenu(nfIcon); });
            HotkeyManager.Current.AddOrReplace("OpenContextMenu", Key.D, ModifierKeys.Alt, ToggleContextMenuAction(nfIcon));
        }

        private static EventHandler<HotkeyEventArgs> ToggleContextMenuAction(NotifyIcon nfIcon)
        {
            return (sender, e) =>
            {
                ContextMenuHelpers.ToggleTrayMenu(nfIcon);
            };
        }

        //alternatively you can just do (lambda expression):
        //HotkeyManager.Current.AddOrReplace("OpenContextMenu", Key.L, ModifierKeys.Alt, (sender, e) =>
        //{
        //    ContextMenuHelpers.ToggleTrayMenu(nfIcon);
        //    e.Handled = true;
        //});
    }
}
