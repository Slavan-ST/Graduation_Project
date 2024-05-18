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
    public class StatusViewModel : ViewModelBase
    {
        [Reactive]
        public bool IsLoading { get; set; } = false;
        [Reactive]
        public List<Status>? Statuses { get; set; }
        [Reactive]
        public Status? SelectedStatus { get; set; }

        public ICommand? NewStatus { get; set; }
        public ICommand? Delete { get; set; }
        public ICommand? Save { get; set; }

        public StatusViewModel(IScreen? screen = null) : base(screen)
        {
            FillAsync();
            Save = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                SaveInAPIAsync();
                IsLoading = false;
            });
            NewStatus = ReactiveCommand.Create(async () =>
            {
                try
                {
                    IsLoading = true;
                    var objAdd = new Status();
                    int? id = await StatusAPI.PostStatusAsync(objAdd);
                    if (id == null)
                    {
                        IsLoading = false;
                        return;
                    }
                    objAdd.Id = (int)id;

                    Statuses ??= [];
                    var temp = new List<Status>(Statuses)
                    {
                        objAdd
                    };

                    Statuses = new List<Status>(temp);
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            });
            Delete = ReactiveCommand.Create(async (int id) =>
            {
                IsLoading = true;

                Statuses ??= [];

                var temp = new List<Status>(Statuses);
                var objRemove = temp.Where(x => x.Id == id).FirstOrDefault();

                if (objRemove == null)
                {
                    IsLoading = false;
                    return;
                }


                var code =await StatusAPI.DeleteStatusAsync(objRemove.Id);
                if (code == System.Net.HttpStatusCode.Conflict)
                {
                    //тут можно сообщение об ошибке впихать, что-то вроде того что есть что-то с чем связана эта запись
                    return;
                }
                temp.Remove(objRemove);
                Statuses = new List<Status>(temp);
                IsLoading = false;
            });
        }

        async void SaveInAPIAsync()
        {
            IsLoading = true;
            if (Statuses == null)
            {
                return;
            }
            foreach (var obj in Statuses)
            {
                await API.StatusAPI.PostStatusAsync(obj);
            }
            IsLoading = false;
        }

        async void FillAsync()
        {
            IsLoading = true;
            var statuses = await StatusAPI.GetStatusesAsync();
            if (statuses == null)
            {
                IsLoading = false;
                return;
            }
            Statuses = statuses.ToList();
            IsLoading = false;
        }
    }
}
