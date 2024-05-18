using Client.API;
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
using System.Windows.Input;

namespace Client.ViewModels
{
    public class RoomViewModel : ViewModelBase
    {
        [Reactive]
        public bool IsLoading { get; set; } = false;
        [Reactive]
        public List<Room>? Rooms { get; set; }
        [Reactive]
        public Room? SelectedRoom { get; set; }
        
        public ICommand? NewRoom {  get; set; }
        public ICommand? Delete {  get; set; }
        public ICommand? Save { get; set; }
        public RoomViewModel(IScreen? screen = null) : base(screen)
        {
            FillAsync();
            Save = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                SaveInAPIAsync();
                IsLoading = false;
            });
            NewRoom = ReactiveCommand.Create(async () =>
            {
                try
                {
                    IsLoading = true;
                    var objAdd = new Room();
                    int? id = await RoomAPI.PostRoomAsync(objAdd);
                    if (id == null)
                    {
                        IsLoading = false;
                        return;
                    }
                    objAdd.Id = (int)id;

                    Rooms ??= [];
                    var temp = new List<Room>(Rooms)
                    {
                        objAdd
                    };

                    Rooms = new List<Room>(temp);
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    IsLoading = false;
                }
            });
            Delete = ReactiveCommand.Create(async (int id) =>
            {
                IsLoading = true;

                Rooms ??= [];

                var temp = new List<Room>(Rooms);
                var objRemove = temp.Where(x => x.Id == id).FirstOrDefault();

                if (objRemove == null)
                {
                    IsLoading = false;
                    return;
                }


                var code = await RoomAPI.DeleteRoomAsync(objRemove.Id);
                if (code == System.Net.HttpStatusCode.Conflict)
                {
                    //тут можно сообщение об ошибке впихать, что-то вроде того что есть что-то с чем связана эта запись
                    IsLoading = false;
                    return;
                }
                temp.Remove(objRemove);
                Rooms = new List<Room>(temp);
                IsLoading = false;
            });
        }

        async void SaveInAPIAsync()
        {
            IsLoading = true;
            if (Rooms == null)
            {
                return;
            }
            foreach (var obj in Rooms)
            {
                await API.RoomAPI.PostRoomAsync(obj);
            }
            IsLoading = false;
        }

        async void FillAsync()
        {
            IsLoading = true;
            var statuses = await RoomAPI.GetRoomsAsync();
            if (statuses == null)
            {
                IsLoading = false;
                return;
            }
            Rooms = statuses.ToList();
            IsLoading = false;
        }
    }
}
