using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ClientAvalonia.Services;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAvalonia.Data
{
    public class DataFromServer
    {
        public async void AddImageInUser(string name, Connection connection)
        {
            var topLevel = TopLevel.GetTopLevel(new Window());
            var files = await topLevel!.StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
            {
                Title = "Select image",
                AllowMultiple = false
            });

            if (files.Count >= 1)
            {
                Bitmap bitmap = new Bitmap(files[0].Path.LocalPath);
                Temp.MainViewModel.Image = bitmap;

                using var stream = new MemoryStream();
                bitmap!.Save(stream);
                byte[] bytes = stream.GetBuffer();
                List<string> paramsName = new List<string>()
                {
                    "@image"
                };
                HeaderClient header = new HeaderClient(
                    "UPDATE",
                    "text",
                    "UPDATE Users set Image = @image where FIO ='Guest2';",
                    paramsName
                    );
                Query query = new Query(header, bytes);
                await connection.SendMessageAsync(query);
            }
        }
    }
}
