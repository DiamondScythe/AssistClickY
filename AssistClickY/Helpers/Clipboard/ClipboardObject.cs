using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssistClickY.Enums;

namespace AssistClickY.Helpers.Clipboard
{
    public class ClipboardObject
    {
        public ClipboardType Type { get; set; }

        public string Text { get; set; }

        public string ImageLink { get; set; }

        public string AudioLink { get; set; }
    }
}
