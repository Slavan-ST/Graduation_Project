using Avalonia;
using Avalonia.Controls;
using System.IO;
using System.Threading.Tasks;

namespace Client.Services
{
    public class FileDialog
    {
        static Visual? _visual;
        public FileDialog(Visual? visual)
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
