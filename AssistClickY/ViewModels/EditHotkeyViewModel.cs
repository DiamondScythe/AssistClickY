using AssistClickY.Data;
using AssistClickY.Enums;
using AssistClickY.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WinRT;

namespace AssistClickY.ViewModels
{
    public partial class EditHotkeyViewModel : ObservableObject
    {
        public EditHotkeyViewModel(Hotkey hotkey)
        {
            HotkeyJobs = new List<HotkeyJob>((HotkeyJob[])Enum.GetValues(typeof(HotkeyJob)));

            oldName = hotkey.Name;
            oldHotkey = hotkey.HotkeyCombination;
            oldJob = hotkey.HotkeyJob.ToString();
        }

        [ObservableProperty]
        private string oldName;

        [ObservableProperty]
        private string oldHotkey;

        [ObservableProperty]
        private string oldJob;

        [ObservableProperty]
        private List<HotkeyJob> hotkeyJobs;

        [ObservableProperty]
        private MahApps.Metro.Controls.HotKey updatedHotkeyCombination;

        [ObservableProperty]
        private string updatedName;

        [ObservableProperty]
        private HotkeyJob updatedCurrentJob;

        public event EventHandler OnRequestClose;

        [RelayCommand]
        private void Save()
        {
            // Trigger the event to request the window to close and set the DialogResult to true
            OnRequestClose?.Invoke(this, EventArgs.Empty); // Safe event invocation
        }
    }
}
