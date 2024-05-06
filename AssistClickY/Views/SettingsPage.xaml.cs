using System.Windows.Controls;

using AssistClickY.ViewModels;

namespace AssistClickY.Views;

public partial class SettingsPage : Page
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
