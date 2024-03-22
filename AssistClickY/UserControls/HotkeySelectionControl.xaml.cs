using AssistClickY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssistClickY.UserControls
{
    /// <summary>
    /// Interaction logic for HotkeySelectionControl.xaml
    /// </summary>
    public partial class HotkeySelectionControl : UserControl
    {
        public static readonly DependencyProperty HotkeyProperty =
        DependencyProperty.Register("Hotkey", typeof(Hotkey), typeof(HotkeySelectionControl), new PropertyMetadata(null));

        public Hotkey Hotkey
        {
            get { return (Hotkey)GetValue(HotkeyProperty); }
            set { SetValue(HotkeyProperty, value); }
        }

        public HotkeySelectionControl()
        {
            InitializeComponent();
        }
    }
}
