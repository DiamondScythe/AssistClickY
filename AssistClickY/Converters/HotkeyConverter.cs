using AssistClickY.Models;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AssistClickY.Converters
{
    public class HotkeyConverter : IValueConverter
    {
        //Hotkey is self-implemented type, HotKey is mahapp metro's type. Big stinky hack.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Hotkey hotkey)
            {
                // Convert Hotkey object to MahApps.Metro.Controls.HotKey object
                return new HotKey(hotkey.Key, hotkey.ModifierKeys);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MahApps.Metro.Controls.HotKey mahHotKey)
            {
                return new Hotkey
                {
                    ModifierKeys = mahHotKey.ModifierKeys,
                    Key = mahHotKey.Key
                };
            }
            return null;
        }
    }
}
