using AssistClickY.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using Microsoft.Extensions.Options;

namespace AssistClickY.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private MahApps.Metro.Controls.HotKey hotkeyCombination;

    public SettingsViewModel()
    {
    }
}
