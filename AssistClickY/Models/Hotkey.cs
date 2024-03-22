using AssistClickY.Enums;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AssistClickY.Models
{
    public class Hotkey
    {
        public int HotkeyId { get; set; }
        public string HotkeyCombination { get; set; }

        public ModifierKeys ModifierKeys { get; set; }

        public Key Key { get; set; }

        public HotkeyJob HotkeyJob { get; set; }
    }
}
