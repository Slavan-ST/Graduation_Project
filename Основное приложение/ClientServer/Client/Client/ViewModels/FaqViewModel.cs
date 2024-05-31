using Client.Models;
using Client.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;

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

        public FaqViewModel(IScreen? screen = null) : base(screen)
        {
            /* 
                Новые записи добавлять в виде:
                
                new TreeViewItem("Вопрос1", new ObservableCollection<TreeViewItem>
                    {
                        new TreeViewItem("Ответ1")
                    }),
             
             */

            QuestAnswer = new ObservableCollection<TreeViewItem>
            {
                new TreeViewItem("Что делать если забыл пароль?", new ObservableCollection<TreeViewItem>
                    {
                        new TreeViewItem("Обратиться к администратору системы"),
                    }),

                new TreeViewItem("Пароль возможно восстановить самостоятельно?", new ObservableCollection<TreeViewItem>
                    {
                        new TreeViewItem("Нет. Это сделано для большей безопасности данных пользователей")
                    }),
                new TreeViewItem("Появилась надпись сервер не отвечает. Что делать?", new ObservableCollection<TreeViewItem>
                    {
                        new TreeViewItem("Обратиться к администратору системы. Возможно идут технические работы")
                    })

            };
        }
    }
}
