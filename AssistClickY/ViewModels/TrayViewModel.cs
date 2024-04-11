using AssistClickY.Data;
using AssistClickY.Enums;
using AssistClickY.MediaTools;
using AssistClickY.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistClickY.ViewModels
{
    public partial class TrayViewModel : ObservableObject
    {
        [RelayCommand]
        private static void Screenshot() => ScreenshotHelpers.TakeScreenshot();

        [RelayCommand]
        private static async Task RecordAudio()
        {
            await AudioRecordingHelpers.RecordAudio();
        }

        [RelayCommand]
        private static void FinishRecording()
        {
            AudioRecordingHelpers.FinishRecording();
        }
    }
}
