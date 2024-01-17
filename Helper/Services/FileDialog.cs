using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Services
{
    public class FileDialog
    {
        static Visual? _visual;
        public static void Config(Visual? visual)
        {
            _visual = visual;
        }
        public static async Task<byte[]> OpenImage()
        {
            var imageFile = await TopLevel.GetTopLevel(_visual)!.StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
            {
                Title = "select image",
                AllowMultiple = false
            });
            string pathToImage = imageFile[0].Path.LocalPath;

            byte[] imageBytes = File.ReadAllBytes(pathToImage);
            return imageBytes;
        }
    }
}
