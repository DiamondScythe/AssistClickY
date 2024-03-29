using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistClickY.MediaTools
{
    public static class AudioRecordingHelpers
    {
        // TODO: fix: Audio doesn't finish recording unless the total amount of audible bits have exceeded 5 seconds.
        // read: https://github.com/naudio/NAudio/blob/master/Docs/WasapiLoopbackCapture.md
        public static async Task RecordAudio()
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
