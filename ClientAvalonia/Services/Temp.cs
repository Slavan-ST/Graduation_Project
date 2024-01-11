using ClientAvalonia.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientAvalonia.Services
{
    public class Temp:ReactiveObject
    {
        //Временное свойство для изменения поля, при получении ответа от сервера
        public static MainViewModel MainViewModel { get; set; } = new MainViewModel();
        public static int CountAnswer { get; set; } = 0;
    }
}
