using AssistClickY.Data;
using AssistClickY.Enums;
using AssistClickY.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScreenCapture.NET;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

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
        private static void Screenshot()
        {
            using var bitmap = new Bitmap(1920, 1080);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(0, 0, 0, 0,
                bitmap.Size, CopyPixelOperation.SourceCopy);
            }
            bitmap.Save("filename.jpg", ImageFormat.Jpeg);
        }
    }
}