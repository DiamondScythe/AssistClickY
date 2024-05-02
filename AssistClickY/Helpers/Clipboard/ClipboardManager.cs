using AssistClickY.Helpers.Misc;
using AssistClickY.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistClickY.Helpers.Clipboard
{
    public class ClipboardManager
    {
        private static readonly List<ClipboardItem> clipboardItems = new List<ClipboardItem>();

        public static List<ClipboardItem> ClipboardItems => clipboardItems;

        internal static void AddNewClipboardItem(ClipboardFormat format, object data)
        {
            //this cooldown mechanism is a hack. The clipboard monitor usually alerts twice for FileDrop clipboard items for no apparent reason.
            //this cooldown should prevent the same item from being added to the list twice.
            var cooldown = Cooldown.Instance;

            if (cooldown.IsCooldownActive())
            {
                return;
            }
            cooldown.TriggerCooldown(200);

            Trace.WriteLine("Clipboard changed and it has the format: " + format.ToString());
            Trace.WriteLine("Clipboard changed and object has the type: " + data.GetType());

            ClipboardItem newItem = new ClipboardItem();

            //public ClipboardItemType Format { get; set; }

            //public string ClipboardText { get; set; }

            //public string ClipboardImageLink { get; set; }

            //public string ClipboardAudioLink { get; set; }

            if (format == ClipboardFormat.Text)
            {
                newItem.Format = Enums.ClipboardItemType.Text;
                newItem.ClipboardText = System.Windows.Forms.Clipboard.GetText();
            }
            
            else if (format == ClipboardFormat.FileDrop)
            {
                //need to somehow differentiate between audio file drop and image file drop first

                StringCollection FileDropFileNames = System.Windows.Forms.Clipboard.GetFileDropList();

                // Check if the collection is not null
                if (FileDropFileNames != null && FileDropFileNames.Count < 2)
                {
                    if (FileDropFileNames[0].EndsWith(".wav") || FileDropFileNames[0].EndsWith(".mp3"))
                    {
                        //deal with audio file
                        newItem.Format = Enums.ClipboardItemType.Audio;
                        newItem.ClipboardAudioLink = FileDropFileNames[0];
                    }
                    else if (FileDropFileNames[0].EndsWith(".jpg") || FileDropFileNames[0].EndsWith(".jpeg") || FileDropFileNames[0].EndsWith(".png"))
                    {
                        //deal with image file
                        newItem.Format = Enums.ClipboardItemType.Image;
                        newItem.ClipboardImageLink = FileDropFileNames[0];
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Trace.WriteLine("String collection is null.");
                    return;
                }
            }
            else
            {
                return;
            }

            //add the item into the list
            clipboardItems.Add(newItem);
        }
    }
}
