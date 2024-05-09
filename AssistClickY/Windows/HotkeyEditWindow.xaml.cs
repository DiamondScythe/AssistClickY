using AssistClickY.ViewModels;
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
    /// Interaction logic for HotkeyEditWindow.xaml
    /// </summary>
    public partial class HotkeyEditWindow : Window
    {
        public HotkeyEditWindow()
        {
            InitializeComponent();

            //to wait for the datacontext to load properly
            this.Loaded += HotkeyEditWindow_Loaded;
        }


        private void HotkeyEditWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Subscribe to the OnRequestClose event now that DataContext is set
            if (DataContext is EditHotkeyViewModel viewModel)
            {
                viewModel.OnRequestClose += ViewModel_OnRequestClose;
            }
        }

        // This event handler will be triggered when Save() is called in the ViewModel
        private void ViewModel_OnRequestClose(object sender, EventArgs e)
        {
            // Set the DialogResult to true, indicating a successful save
            this.DialogResult = true;

            // Close the window
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            // Unsubscribe from the event to avoid memory leaks
            if (DataContext is EditHotkeyViewModel viewModel)
            {
                viewModel.OnRequestClose -= ViewModel_OnRequestClose;
            }

            base.OnClosed(e);
        }
    }
}
