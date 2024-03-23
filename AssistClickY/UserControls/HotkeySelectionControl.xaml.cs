using AssistClickY.Models;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Reflection.Metadata;
using System.Security.Policy;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AssistClickY.UserControls
{
    /// <summary>
    /// Interaction logic for HotkeySelectionControl.xaml
    /// </summary>
    public partial class HotkeySelectionControl : UserControl
    {
        //This line registers a new dependency property called HotkeyProperty with the WPF property system. The DependencyProperty.Register method takes the following arguments:

        //"Hotkey": The name of the dependency property.
        //typeof(Hotkey): The type of the property value.
        //typeof(HotkeySelectionControl): The owner type (the class where the dependency property is defined).
        //new PropertyMetadata(null) : Metadata associated with the dependency property.In this case, it's just setting the default value to null.
        public static readonly DependencyProperty HotkeyProperty =
        DependencyProperty.Register("Hotkey", typeof(Hotkey), typeof(HotkeySelectionControl), new PropertyMetadata(null));
        //This is a CLR(common language runtime) property wrapper around the dependency property HotkeyProperty.The get and set accessors use the GetValue and SetValue methods to interact with the underlying dependency property.
        //When you set the Hotkey property, the SetValue method is called, which notifies the WPF property system that the property value has changed. This allows WPF to handle data binding, styling, and animation updates automatically.

        //Similarly, when you retrieve the value of the Hotkey property, the GetValue method is called, which retrieves the current value of the underlying dependency property.

        //By using a dependency property, you can take advantage of WPF's built-in property system, which provides automatic change notifications, data validation, and other features that are not available with traditional CLR properties.
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
