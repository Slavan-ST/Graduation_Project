using Client.API;
using Client.ViewModels.Base;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class CleanLineRaidViewModel : ViewModelBase
    {
        [Reactive]
        public Room? SelectedRoom { get; set; } // для comboBox
        [Reactive]
        public string? Mark { get; set; } = "-";
        [Reactive]
        public string? Description { get; set; } = "";
        [Reactive]
        public List<Room>? Rooms { get; set; } // для comboBox, загружается только при старте страницы

        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;

        public CleanLineRaidViewModel(IScreen? screen = null) : base(screen) 
        {
            Initialize();
            Next = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                if (SelectedRoom == null)
                {
                    IsLoading = false;
                    return;
                }

                if (Rooms == null)
                {
                    IsLoading = false;
                    return;
                }
                SelectedRoom = Rooms.Where(x => (x.Id + 1) == SelectedRoom.Id).FirstOrDefault();
                IsLoading = false;
            });

            Mark2 = ReactiveCommand.Create(() =>
            {
                Mark = "2";
            });

            Mark3 = ReactiveCommand.Create(() =>
            {
                Mark = "3";
            });

            Mark4 = ReactiveCommand.Create(() =>
            {
                Mark = "4";
            });

            Mark5 = ReactiveCommand.Create(() =>
            {
                Mark = "5";
            });

            Save = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                CreateLog();
                IsLoading = false;
            });
        }
        async void CreateLog()
        {
            IsLoading = true;
            if (SelectedRoom == null)
            {
                IsLoading = false;
                return;
            }
            if (Mark == null)
            {
                IsLoading = false;
                return;
            }
            PurityRaidLog purityRaidLog = new PurityRaidLog()
            {
                RoomId = SelectedRoom.Id,
                Date = System.DateTime.Now,
                Marker = Mark,
                Description = Description
            };
            await API.PurityRaidLogAPI.PostPurityRaidLog(purityRaidLog);
            IsLoading = false;
        }
        async void Initialize()
        {
            IsLoading = true;
            var rooms = await RoomAPI.GetRoomsAsync();
            if (rooms == null)
            {
                IsLoading = false;
                return;
            }
            Rooms = new List<Room>(rooms);
            SelectedRoom = Rooms.FirstOrDefault();
            IsLoading = false;
        }
        public ICommand Next { get; set; }
        public ICommand Save { get; set; }

        public ICommand Mark2 { get; set; }
        public ICommand Mark3 { get; set; }
        public ICommand Mark4 { get; set; }
        public ICommand Mark5 { get; set; }

    }
}