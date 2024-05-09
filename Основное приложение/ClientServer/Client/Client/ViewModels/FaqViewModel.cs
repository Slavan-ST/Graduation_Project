using Client.Models;
using Client.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class FaqViewModel : ViewModelBase
    {
        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;

        [Reactive]
        public ObservableCollection<TreeViewItem>? QuestAnswer { get; set; }

        [Reactive]
        public ObservableCollection<TreeViewItem>? Nodes { get; set; }

        public FaqViewModel(IScreen? screen = null) : base(screen)
        {
            IsLoading = true;
            QuestAnswer = new ObservableCollection<TreeViewItem>
            {
                new TreeViewItem("Вопрос1", new ObservableCollection<TreeViewItem>
                    {
                        new TreeViewItem("Ответ1")
                    }),

                new TreeViewItem("Вопрос2", new ObservableCollection<TreeViewItem>
                    {
                        new TreeViewItem("Ответ2")
                    }),

                new TreeViewItem("Вопрос3"), 
                new TreeViewItem("Вопрос4"), 
                new TreeViewItem("Вопрос5")

            };
            IsLoading = false;
        }
    }
}
