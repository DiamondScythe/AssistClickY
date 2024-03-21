using System.Windows.Controls;

using AssistClickY.ViewModels;

namespace AssistClickY.Views;

public partial class HomePage : Page
{
    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
