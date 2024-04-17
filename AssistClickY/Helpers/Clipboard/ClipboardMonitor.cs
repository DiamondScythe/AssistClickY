using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssistClickY.Helpers.Clipboard
{
    public class ClipboardMonitor
    {
        //https://stackoverflow.com/a/33018459/15864049
        //try this

        public event EventHandler ClipboardChanged;

        public ClipboardMonitor()
        {
            // Start monitoring clipboard changes
            //System.Windows.Forms.Clipboard.Changed += OnClipboardChanged;
        }

        private void OnClipboardChanged(object sender, EventArgs e)
        {
            // Raise event to notify subscribers that the clipboard has changed
            ClipboardChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
