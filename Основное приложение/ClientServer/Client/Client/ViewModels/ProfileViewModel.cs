using Client.ViewModels.Base;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;

        public ProfileViewModel(IScreen? screen = null) : base(screen)
        {
            IsLoading = true;
            // в загрузке
            IsLoading = false;
        }
        public ProfileViewModel(IScreen? screen = null, Student? student = null) : base(screen)
        {
            IsLoading = true;
            // в загрузке
            IsLoading = false;
        }

        [Reactive]
        public string TestText { get; set; } = "test";
        [Reactive]
        public string Fio { get; set; } = "test";
        [Reactive]
        public string NumberRoom { get; set; } = "test";
        [Reactive]
        public string MarkClear { get; set; } = "test";
        [Reactive]
        public string StatusUser { get; set; } = "test";
    }
}
