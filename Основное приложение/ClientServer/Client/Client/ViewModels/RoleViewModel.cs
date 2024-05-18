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
    public class RoleViewModel : ViewModelBase
    {
        [Reactive]
        public bool IsLoading { get; set; } = false;
        [Reactive]
        public List<Role>? Roles { get; set; }
        [Reactive]
        public Role? SelectedRole { get; set; }

        public ICommand? NewRole { get; set; }
        public ICommand? Delete { get; set; }
        public ICommand? Save { get; set; }

        public RoleViewModel(IScreen? screen = null) : base(screen)
        {
            FillAsync();
            Save = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                SaveInAPIAsync();
                IsLoading = false;
            });
            NewRole = ReactiveCommand.Create(async () =>
            {
                try
                {
                    IsLoading = true;
                    var objAdd = new Role();
                    int? id = await RolesAPI.PostAsync(objAdd);
                    if (id == null)
                    {
                        IsLoading = false;
                        return;
                    }
                    objAdd.Id = (int)id;

                    Roles ??= [];
                    var temp = new List<Role>(Roles)
                    {
                        objAdd
                    };

                    Roles = new List<Role>(temp);
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            });
            Delete = ReactiveCommand.Create(async (int id) =>
            {
                try
                {
                    IsLoading = true;

                    Roles ??= [];

                    var temp = new List<Role>(Roles);
                    var objRemove = temp.Where(x => x.Id == id).FirstOrDefault();

                    if (objRemove == null)
                    {
                        IsLoading = false;
                        return;
                    }


                    await RolesAPI.DeleteAsync(objRemove.Id);
                    temp.Remove(objRemove);
                    Roles = new List<Role>(temp);
                    IsLoading = false;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            });
        }

        async void SaveInAPIAsync()
        {
            IsLoading = true;
            if (Roles == null)
            {
                return;
            }
            foreach (var obj in Roles)
            {
                await API.RolesAPI.PostAsync(obj);
            }
            IsLoading = false;
        }

        async void FillAsync()
        {
            IsLoading = true;
            var roles = await RolesAPI.GetAsync();
            if (roles == null)
            {
                IsLoading = false;
                return;
            }
            Roles = roles.ToList();
            IsLoading = false;
        }
    }
}
