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
    public class GroupViewModel : ViewModelBase
    {
        [Reactive]
        public bool IsLoading { get; set; } = false;
        [Reactive]
        public List<Group>? Groups { get; set; }
        [Reactive]
        public Group? SelectedGroup { get; set; }

        public ICommand? NewGroup { get; set; }
        public ICommand? Delete { get; set; }
        public ICommand? Save { get; set; }

        public GroupViewModel(IScreen? screen = null) : base(screen)
        {
            FillAsync();
            Save = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                SaveInAPIAsync();
                IsLoading = false;
            });
            NewGroup = ReactiveCommand.Create(async () =>
            {
                try
                {
                    IsLoading = true;
                    var objAdd = new Group();
                    int? id = await GroupAPI.PostGroupAsync(objAdd);
                    if (id == null)
                    {
                        IsLoading = false;
                        return;
                    }
                    objAdd.Id = (int)id;

                    Groups ??= [];
                    var temp = new List<Group>(Groups)
                    {
                        objAdd
                    };

                    Groups = new List<Group>(temp);
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

                Groups ??= [];

                var temp = new List<Group>(Groups);
                var objRemove = temp.Where(x => x.Id == id).FirstOrDefault();

                if (objRemove == null)
                {
                    IsLoading = false;
                    return;
                }


                await GroupAPI.DeleteGroupAsync(objRemove.Id);
                temp.Remove(objRemove);
                Groups = new List<Group>(temp);
                IsLoading = false;
            });
        }

        async void SaveInAPIAsync()
        {
            IsLoading = true;
            if (Groups == null)
            {
                return;
            }
            foreach (var obj in Groups)
            {
                await API.GroupAPI.PostGroupAsync(obj);
            }
            IsLoading = false;
        }

        async void FillAsync()
        {
            IsLoading = true;
            var groups = await GroupAPI.GetGroupsAsync();
            if (groups == null)
            {
                IsLoading = false;
                return;
            }
            Groups = groups.ToList();
            IsLoading = false;
        }
    }
}
