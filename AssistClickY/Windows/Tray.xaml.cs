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
using System.Windows.Shapes;

namespace AssistClickY.Windows
{
    /// <summary>
    /// Interaction logic for Tray.xaml
    /// </summary>
    public partial class Tray : Window
    {
        public Tray()
        {
            InitializeComponent();

            // Set the properties of the window
            WindowStyle = WindowStyle.None;
            AllowsTransparency = false;
            Background = Brushes.AliceBlue;
            Topmost = true;
            Left = 100; // Set as desired
            Top = 100; // Set as desired
            Width = 200; // Set as desired
            Height = 200; // Set as desired

            // Attach MouseDown event handler to any UI element within the window
            this.MouseDown += Window_MouseDown;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Check if the left mouse button is pressed
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Call DragMove to allow dragging the window
                DragMove();
            }
        }
    }
}
