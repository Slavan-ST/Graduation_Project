using ServerAvalonia.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAvalonia.Services
{
    public class Temp:ReactiveObject
    {
        public static MainViewModel MainViewModel { get; set; } = new MainViewModel();
    }
}
