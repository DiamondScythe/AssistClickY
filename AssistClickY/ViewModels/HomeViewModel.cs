using AssistClickY.Data;
using AssistClickY.Enums;
using AssistClickY.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using System.Windows;
using AssistClickY.UserControls;
using AssistClickY.Windows;
using AssistClickY.Helpers.MediaTools;
using System.Diagnostics;

namespace AssistClickY.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly AssistClickYContext _dbContext;

        [ObservableProperty]
        private List<HotkeyJob> hotkeyJobs;

        [ObservableProperty]
        private List<Hotkey> hotkeys;

        public HomeViewModel(AssistClickYContext dbContext)
        {
            _dbContext = dbContext;
            HotkeyJobs = new List<HotkeyJob>((HotkeyJob[])Enum.GetValues(typeof(HotkeyJob)));

            Hotkeys = _dbContext.GetAllHotkeys();
        }

        [ObservableProperty]
        private MahApps.Metro.Controls.HotKey hotkeyCombination;

        [ObservableProperty]
        private HotkeyJob currentJob;


        [RelayCommand]
        private async Task AddHotkey()
        {
            try
            {
                // TODO: Add some validation
                var hotkey = new Hotkey
                {
                    HotkeyCombination = HotkeyCombination.ToString(),
                    HotkeyJob = CurrentJob,
                    Key = HotkeyCombination.Key,
                    ModifierKeys = HotkeyCombination.ModifierKeys,
                };

                _dbContext.Hotkeys.Add(hotkey);
                await _dbContext.SaveChangesAsync();

                Hotkeys = _dbContext.GetAllHotkeys();
            }
            catch (Exception ex)
            {

            }
        }

        [RelayCommand]
        private static void EditHotkey(Hotkey hotkey)
        {
            Trace.WriteLine("TestingEdit");
            Trace.WriteLine(hotkey.HotkeyCombination);
        }
        [RelayCommand]
        private async Task DeleteHotkey(Hotkey hotkey)
        {
            try
            {
                _dbContext.Hotkeys.Remove(hotkey);
                await _dbContext.SaveChangesAsync();

                Hotkeys = _dbContext.GetAllHotkeys();
            }
            catch (Exception ex)
            {

            }
        }


        [RelayCommand]
        private static void Screenshot() => ScreenshotHelpers.TakeScreenshot();

        [RelayCommand]
        private static async Task RecordAudio()
        {
            await AudioRecordingHelpers.RecordAudio();
        }
        [RelayCommand]
        private static void EnableTray()
        {
            TrayViewModel vm = new TrayViewModel();
            // Create a new Window
            var popupWindow = new Tray(vm);

            // Show the Window
            popupWindow.Show();
        }

    }
}