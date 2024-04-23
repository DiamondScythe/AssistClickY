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
            Trace.WriteLine("Clipboard changed and it has the format: " + format.ToString());
            Trace.WriteLine("Clipboard changed and object has the type: " + data.GetType());

            // You can now access clipboardItems list here

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

            if (format == ClipboardFormat.FileDrop)
            {
                //need to somehow differentiate between audio file drop and image file drop first

                StringCollection FileDropFileNames = System.Windows.Forms.Clipboard.GetFileDropList();

                // Check if the collection is not null
                if (FileDropFileNames != null)
                {
                    if (FileDropFileNames[0].EndsWith(".wav") || FileDropFileNames[0].EndsWith(".mp3"))
                    {
                        //deal with audio file
                        newItem.Format = Enums.ClipboardItemType.Audio;
                        newItem.ClipboardAudioLink = FileDropFileNames[0];
                    }
                    if (FileDropFileNames[0].EndsWith(".jpg") || FileDropFileNames[0].EndsWith(".jpeg") || FileDropFileNames[0].EndsWith(".png"))
                    {
                        //deal with image file
                        newItem.Format = Enums.ClipboardItemType.Image;
                        newItem.ClipboardImageLink = FileDropFileNames[0];
                    }
                }
                else
                {
                    Trace.WriteLine("String collection is null.");
                }
            }

            //add the item into the list
            clipboardItems.Add(newItem);
        }
    }
}
