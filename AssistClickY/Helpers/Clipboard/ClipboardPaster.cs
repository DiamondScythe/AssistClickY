using AssistClickY.Enums;
using AssistClickY.Helpers.Misc;
using AssistClickY.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssistClickY.Helpers.Clipboard
{
    public class ClipboardPaster
    {
        public static void PasteClipboardItem(ClipboardItem item)
        {
            //public ClipboardItemType Format { get; set; }

            //public string ClipboardText { get; set; }

            //public string ClipboardImageLink { get; set; }

            //public string ClipboardAudioLink { get; set; }

            //use cooldown to temporarily disable the clipboard monitor
            var cooldown = Cooldown.Instance;
            cooldown.TriggerCooldown(200);

            //check the format of the item, then deal with it accordingly

            if (item.Format == ClipboardItemType.Text)
            {
                System.Windows.Forms.Clipboard.SetText(item.ClipboardText);
            }
            else if (item.Format == ClipboardItemType.Image)
            {
                StringCollection fileDropList = new StringCollection();
                fileDropList.Add(item.ClipboardImageLink);
                System.Windows.Forms.Clipboard.SetFileDropList(fileDropList);
            }
            else if (item.Format == ClipboardItemType.Audio)
            {
                StringCollection fileDropList = new StringCollection();
                fileDropList.Add(item.ClipboardAudioLink);
                System.Windows.Forms.Clipboard.SetFileDropList(fileDropList);
            }

            // Send Ctrl+V
            SendKeys.SendWait("^v"); // "^" is the Ctrl key
        }
    }
}
