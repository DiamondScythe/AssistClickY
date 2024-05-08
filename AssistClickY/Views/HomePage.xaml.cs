using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using AssistClickY.Models;
using AssistClickY.ViewModels;
using AssistClickY.Windows;

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
            var editWindow = new HotkeyEditWindow
            {
                DataContext = new EditHotkeyViewModel(hotkey)
            };

            bool? result = editWindow.ShowDialog();

            if (result == true)
            {
                var editVM = (EditHotkeyViewModel)editWindow.DataContext;

                var updatedName = editVM.UpdatedName;
                var updatedHotkeyCombination = editVM.UpdatedHotkeyCombination;
                var updatedCurrentJob = editVM.UpdatedCurrentJob;

                var updatedHotkey = new Hotkey
                {
                    //new hotkeyid = old hotkeyid
                    HotkeyId = hotkey.HotkeyId,
                    Name = updatedName,
                    HotkeyCombination = updatedHotkeyCombination.ToString(),
                    Key = updatedHotkeyCombination.Key,
                    ModifierKeys = updatedHotkeyCombination.ModifierKeys,
                    HotkeyJob = updatedCurrentJob,
                };

                var viewModel = (HomeViewModel)DataContext;
                if (viewModel.EditHotkeyCommand.CanExecute(null))
                    viewModel.EditHotkeyCommand.Execute(updatedHotkey);
            }
        }
    }

    private void DeleteHotkey_Click(object sender, RoutedEventArgs e)
    {
        // Ask for confirmation
        MessageBoxResult result = MessageBox.Show(
            "Are you sure you want to delete this hotkey?",
            "Delete Confirmation",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);


        if (result == MessageBoxResult.Yes)
        {
            if (sender is MenuItem menuItem && menuItem.DataContext is Hotkey hotkey)
            {
                var viewModel = (HomeViewModel)DataContext;
                if (viewModel.DeleteHotkeyCommand.CanExecute(null))
                    viewModel.DeleteHotkeyCommand.Execute(hotkey);
            }
        }
    }
}
