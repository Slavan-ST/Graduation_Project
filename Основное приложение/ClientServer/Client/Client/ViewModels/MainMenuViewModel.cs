using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Client.Models;
using Client.Services;
using Client.ViewModels.Base;
using Client.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace Client.ViewModels
{
    public class MainMenuViewModel : ViewModelBaseNavigator
    {
        // Пример команды перехода
        //public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

        public MainMenuViewModel(IScreen? screen = null):base(screen)
        {     

            HideSideBar = ReactiveCommand.Create(() =>
            {
                IsOpenSideBar = false;
            });

            OpenSideBar = ReactiveCommand.Create(() =>
            {
                IsOpenSideBar = true;
            });


            Main = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Execute(new NewsViewModel(this));
            });


            DutyChart = ReactiveCommand.Create(() => 
            { 
                Router.Navigate.Execute(new DutyChartViewModel(this));
            });
            Events = ReactiveCommand.Create(() => 
            {
                Router.Navigate.Execute(new EventsViewModel(this));
            });

            EventsList = ReactiveCommand.Create(() => 
            {
                Router.Navigate.Execute(new EventsListViewModel(this)); 
            });

            Faq = ReactiveCommand.Create(() => 
            { 
                Router.Navigate.Execute(new FaqViewModel(this));
            });

            ListStudents = ReactiveCommand.Create(() => 
            { 
                Router.Navigate.Execute(new ListStudentsViewModel(this)); 
            });

            PurityChart = ReactiveCommand.Create(() =>
            { 
                Router.Navigate.Execute(new PurityChartViewModel(this)); 
            });

            Statement = ReactiveCommand.Create(() => 
            { 
                Router.Navigate.Execute(new StatementViewModel(this)); 
            });

            Profile = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Execute(new ProfileViewModel(this));
            });
            Record = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Execute(new RecordStudentsViewModel(this));
                IsOpenSideBar = false;
            });



            Exit = ReactiveCommand.Create(() => 
            {

                // + сюда добавить выход из учётной записи
                HostScreen.Router.Navigate.Execute(new AuthViewModel(HostScreen)); 
            });
        }

        #region Commands

        /// <summary>
        /// Комманда для сокрытия SideBar'a
        /// </summary>
        public ICommand HideSideBar { get; set; }
        /// <summary>
        /// Комманда для открытия SideBar'a
        /// </summary>
        public ICommand OpenSideBar { get; set; }


        /// <summary>
        /// Команда перехода на главную 
        /// </summary>
        public ICommand Main { get; set; }




        /// <summary>
        /// Команда перехода на окно профиля
        /// </summary>
        public ICommand Profile { get; set; }
        /// <summary>
        /// Команда перехода на журнал
        /// </summary>
        public ICommand Record { get; set; }
        /// <summary>
        /// Комманда перехода на окно составления заявления
        /// </summary>
        public ICommand Statement { get; set; }
        /// <summary>
        /// Комманда перехода на окно расписания мероприятий
        /// </summary>
        public ICommand Events { get; set; }



        /// <summary>
        /// Комманда перехода на окно графика мероприятий (для сотрудников)
        /// </summary>
        public ICommand EventsList { get; set; }
        /// <summary>
        /// Комманда перехода на окно графика дежурств
        /// </summary>
        public ICommand DutyChart { get; set; }
        /// <summary>
        /// Комманда перехода на окно со списком студентов
        /// </summary>
        public ICommand ListStudents { get; set; }
        /// <summary>
        /// Комманда перехода на окно экрана чистоты
        /// </summary>
        public ICommand PurityChart { get; set; }




        /// <summary>
        /// Комманда перехода на окно справки (FAQ)
        /// </summary>
        public ICommand Faq { get; set; }
        /// <summary>
        /// Комманда выхода из учетной записи и переход на окно авторизации
        /// </summary>
        public ICommand Exit { get; set; }
        #endregion

        #region Propertyes

        /// <summary>
        /// Рабочее простарнство на интерфейсе
        /// </summary>
        [Reactive]
        public ViewModelBase? WorkPlace { get; set; }
        /// <summary>
        /// Свойство определяющие открыт или закрыт SideBar
        /// </summary>
        /// <value>
        /// <para>true = Открыт</para>
        /// <para>false = Закрыт</para>
        /// </value>
        [Reactive]
        public bool IsOpenSideBar { get; set; } = true;
        /// <summary>
        /// Свойство определяющие столбец WorkPlace
        /// </summary>
        [Reactive]
        public int NumColumn { get; set; } = 0;
        /// <summary>
        /// Свойство определюящие главное ли это окно
        /// </summary>
        /// <value>
        /// <para>true = Главная</para>
        /// <para>false = Не главная (можно отобразить кнопку в SideBar'e)</para>
        /// </value>
        [Reactive]
        public bool IsMain { get; set; } = true;
        /// <summary>
        /// Свойство показывающие относится ли учетная запись пользователя к роли "Студента"
        /// </summary>
        [Reactive]
        public bool IsStudents { get; set; } = true;
        /// <summary>
        /// Свойство показывающие относится ли учетная запись пользователя к роли "Сотрудник"
        /// </summary>
        [Reactive]
        public bool IsWorker { get; set; } = true;

        #endregion
    }
}
