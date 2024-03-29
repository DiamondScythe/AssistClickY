using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistClickY.MediaTools
{
    public static class ScreenshotHelpers
    {
        public static void TakeScreenshot()
        {

            var saveFolderPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Properties.Resources.ScreenshotSavePath);

            // Check if the directory exists, if not, create it
            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
            }

            var fileName = $"screenshot_{DateTime.Now:yyyyMMddHHmmss}.jpg";
            var fullPath = Path.Combine(saveFolderPath, fileName);

            using var bitmap = new Bitmap(1920, 1080);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(0, 0, 0, 0,
                bitmap.Size, CopyPixelOperation.SourceCopy);
            }
            bitmap.Save(fullPath, ImageFormat.Jpeg);
        }
    }
}
