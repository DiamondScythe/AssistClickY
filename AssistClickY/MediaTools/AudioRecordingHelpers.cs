using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssistClickY.MediaTools
{
    public static class AudioRecordingHelpers
    {
        private static bool currentlyRecording = false;
        private static bool stopRecording = false;

        // TODO: fix: Audio doesn't finish recording unless the total amount of audible bits have exceeded 5 seconds.
        // read: https://github.com/naudio/NAudio/blob/master/Docs/WasapiLoopbackCapture.md
        public static async Task RecordAudio()
        {
            currentlyRecording = true;

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
                    //or check for stopRecording flag
                    if (stopRecording || writer.Position > capture.WaveFormat.AverageBytesPerSecond * 5)
                    {
                        capture.StopRecording();
                    }
                };

                capture.RecordingStopped += (s, a) =>
                {
                    writer.Dispose();
                    writer = null;
                    capture.Dispose();

                    //copies saved audio to clipboard
                    var list = new StringCollection();
                    list.Add(outputFilePath);
                    Clipboard.SetFileDropList(list);

                    //reset the stoprecording flag
                    currentlyRecording = false;
                    stopRecording = false;
                };

                capture.StartRecording();
                while (capture.CaptureState != CaptureState.Stopped)
                {
                    Thread.Sleep(500);
                }
            });

            await captureTask;
        }
        public static void FinishRecording()
        {
            if (currentlyRecording)
            {
                stopRecording = true;
            }
            // Set the stopRecording flag to true
        }
    }
}
