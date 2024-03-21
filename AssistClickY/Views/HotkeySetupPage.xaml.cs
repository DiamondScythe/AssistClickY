using System.Windows.Controls;

using AssistClickY.ViewModels;

namespace AssistClickY.Views;

public partial class HotkeySetupPage : Page
{
    public HotkeySetupPage(HotkeySetupViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
