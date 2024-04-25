using AssistClickY.Helpers.Clipboard;
using AssistClickY.Helpers.Misc;
using AssistClickY.Helpers.Mouse;
using AssistClickY.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel.DataTransfer;

namespace AssistClickY.Helpers.ContextMenu
{
    public static class ContextMenuSetup
    {
        public static ContextMenuStrip CreateContextMenu()
        {
            var contextMenu = new ContextMenuStrip();

            //sets size of images in image submenu
            contextMenu.ImageScalingSize = new Size(160, 90);

            // Create a submenu item with a list of child items
            var textSubMenu = new ToolStripMenuItem("Text items");

            // Attach an event handler to the DropDownOpening event
            textSubMenu.DropDownOpening += (sender, e) =>
            {
                List<ClipboardItem> CurrentClipboardItems = new List<ClipboardItem>();
                //filter to text items only
                CurrentClipboardItems = ClipboardManager.ClipboardItems.Where(item => item.Format == Enums.ClipboardItemType.Text).ToList();

                CurrentClipboardItems = ListTools.PreprocessList(CurrentClipboardItems, 6);

                // Clear existing items before repopulating
                textSubMenu.DropDownItems.Clear();

                if (CurrentClipboardItems.Any())
                {
                    // Populate with the latest text ClipboardItems (do the same thing for image and audio)
                    foreach (var item in CurrentClipboardItems)
                    {
                        string displayText;
                        if (item.ClipboardText.Length > 100)
                        {
                            displayText = item.ClipboardText.Truncate(100) + "...";
                        }
                        else
                        {
                            displayText = item.ClipboardText;
                        }

                        textSubMenu.DropDownItems.Add(displayText, null, (sender, e) => ClipboardPaster.PasteClipboardItem(item));
                    }
                }
                else
                {
                    textSubMenu.DropDownItems.Add("No text clipboard items available", null, (sender, e) => { });
                }
            };

            var imageSubMenu = new ToolStripMenuItem("Image items");

            imageSubMenu.DropDownOpening += (sender, e) =>
            {
                List<ClipboardItem> CurrentClipboardItems = new List<ClipboardItem>();
                //filter to image items only
                CurrentClipboardItems = ClipboardManager.ClipboardItems.Where(item => item.Format == Enums.ClipboardItemType.Image).ToList();

                CurrentClipboardItems = ListTools.PreprocessList(CurrentClipboardItems, 4);

                // Clear existing items before repopulating
                imageSubMenu.DropDownItems.Clear();

                if (CurrentClipboardItems.Any())
                {
                    // Populate with the latest text ClipboardItems (do the same thing for image and audio)
                    foreach (var item in CurrentClipboardItems)
                    {
                        //image processing for each item
                        //also please fix the duplicate clipboard monitor calls
                        using (FileStream fs = new FileStream(item.ClipboardImageLink, FileMode.Open, FileAccess.Read))
                        {
                            // Create an Image object from the file stream
                            Image image = Image.FromStream(fs);
                            imageSubMenu.DropDownItems.Add("Screenshot", image, (sender, e) => ClipboardPaster.PasteClipboardItem(item));
                        }
                    }
                }
                else
                {
                    imageSubMenu.DropDownItems.Add("No image clipboard items available", null, (sender, e) => { });
                }
            };

            var audioSubMenu = new ToolStripMenuItem("Audio items");

            audioSubMenu.DropDownOpening += (sender, e) =>
            {
                List<ClipboardItem> CurrentClipboardItems = new List<ClipboardItem>();
                //filter to audio items only
                CurrentClipboardItems = ClipboardManager.ClipboardItems.Where(item => item.Format == Enums.ClipboardItemType.Audio).ToList();

                CurrentClipboardItems = ListTools.PreprocessList(CurrentClipboardItems, 6);

                // Clear existing items before repopulating
                audioSubMenu.DropDownItems.Clear();

                if (CurrentClipboardItems.Any())
                {
                    foreach (var item in CurrentClipboardItems)
                    {
                        audioSubMenu.DropDownItems.Add(item.ClipboardAudioLink, null, (sender, e) => ClipboardPaster.PasteClipboardItem(item));
                    }
                }
                else
                {
                    audioSubMenu.DropDownItems.Add("No audio clipboard items available", null, (sender, e) => { });
                }
            };

            // Add the submenu to the context menu
            contextMenu.Items.Add(textSubMenu);
            contextMenu.Items.Add(imageSubMenu);
            contextMenu.Items.Add(audioSubMenu);

            contextMenu.Items.Add("HotKeyAction", null, (sender, e) => MouseRecorder.HotkeyAction());
            // Add other context menu items here
            return contextMenu;
        }
    }
}
