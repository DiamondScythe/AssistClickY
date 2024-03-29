using AssistClickY.Data;
using AssistClickY.Enums;
using AssistClickY.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using AssistClickY.MediaTools;
using NAudio.Wave;
using NAudio.CoreAudioApi;

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
        private static void Screenshot() => ScreenshotHelpers.TakeScreenshot();

        [RelayCommand]

        // TODO: fix: Audio doesn't finish recording unless the total amount of audible bits have exceeded 5 seconds.
        // read: https://github.com/naudio/NAudio/blob/master/Docs/WasapiLoopbackCapture.md
        private static async Task RecordAudio()
        {
            var outputFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Properties.Resources.AudioSavePath);

            // Check if the directory exists, if not, create it
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            var fileName = $"audio_{DateTime.Now:yyyyMMddHHmmss}.wav";

            var outputFilePath = Path.Combine(outputFolder, fileName);
            var capture = new WasapiLoopbackCapture();
            // optionally we can set the capture waveformat here: e.g. capture.WaveFormat = new WaveFormat(44100, 16,2);
            var writer = new WaveFileWriter(outputFilePath, capture.WaveFormat);

            var captureTask = Task.Run(() =>
            {
                capture.DataAvailable += (s, a) =>
                {
                    writer.Write(a.Buffer, 0, a.BytesRecorded);
                    //records for 5 seconds of audible bits. Change the number to change record duration.
                    if (writer.Position > capture.WaveFormat.AverageBytesPerSecond * 5)
                    {
                        capture.StopRecording();
                    }
                };

                capture.RecordingStopped += (s, a) =>
                {
                    writer.Dispose();
                    writer = null;
                    capture.Dispose();
                };

                capture.StartRecording();
                while (capture.CaptureState != CaptureState.Stopped)
                {
                    Thread.Sleep(500);
                }
            });

            await captureTask;
        }
    }
}