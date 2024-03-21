using System.Windows.Controls;

using AssistClickY.Contracts.Views;
using AssistClickY.ViewModels;

using MahApps.Metro.Controls;

namespace AssistClickY.Views;

public partial class ShellWindow : MetroWindow, IShellWindow
{
    public ShellWindow(ShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public Frame GetNavigationFrame()
        => shellFrame;

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();
}
