using AssistClickY.Enums;
using AssistClickY.Helpers.Clipboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistClickY.Models
{
    public class ClipboardItem
    {
        private static int nextId = 0;

        public int ClipboardItemId { get; set; }

        public ClipboardItemType Format { get; set; }

        public string ClipboardText { get; set; }

        public string ClipboardImageLink { get; set; }

        public string ClipboardAudioLink { get; set; }

        public ClipboardItem()
        {
            ClipboardItemId = nextId++;
        }
    }
}
