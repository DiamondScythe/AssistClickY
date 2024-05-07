using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using AssistClickY.Models;
using AssistClickY.ViewModels;

namespace AssistClickY.Views;

public partial class HomePage : Page
{
    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void EditHotkey_Click(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem menuItem && menuItem.DataContext is Hotkey hotkey)
        {
            var viewModel = (HomeViewModel)DataContext;
            if (viewModel.EditHotkeyCommand.CanExecute(null))
                viewModel.EditHotkeyCommand.Execute(hotkey);
        }
    }

    private void DeleteHotkey_Click(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem menuItem && menuItem.DataContext is Hotkey hotkey)
        {
            var viewModel = (HomeViewModel)DataContext;
            if (viewModel.DeleteHotkeyCommand.CanExecute(null))
                viewModel.DeleteHotkeyCommand.Execute(hotkey);
        }
    }
}
