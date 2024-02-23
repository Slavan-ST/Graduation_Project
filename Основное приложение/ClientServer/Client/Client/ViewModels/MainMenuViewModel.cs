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
using Client.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Client.ViewModels
{
    public class MainMenuViewModel : ReactiveObject, IScreen
    {
        //роутер на котором завязана навигация
        public RoutingState Router { get; } = new RoutingState();

        // Пример команды перехода
        public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

        public MainMenuViewModel()
        {

            GoNext = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new NewsViewModel(this)));

            HideSideBar = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new NewsViewModel(this)));

            OpenSideBar = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new NewsViewModel(this)));

            ToMain = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new NewsViewModel(this)));

            ToDutyChart = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new NewsViewModel(this)));

            ToEvents = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new NewsViewModel(this)));

            ToEventsList = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new NewsViewModel(this)));

            ToFaq = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new NewsViewModel(this)));

            ToListStudents = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new NewsViewModel(this)));

            ToPurityChart = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new NewsViewModel(this)));

            ToStatement = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new NewsViewModel(this)));

            ToProfile = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new NewsViewModel(this)));

            Exit = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new NewsViewModel(this)));
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

        #region SideBar Commands

        /// <summary>
        /// Команда перехода на главную 
        /// </summary>
        public ICommand ToMain { get; set; }

        #region Commands Students

        /// <summary>
        /// Команда перехода на окно профиля
        /// </summary>
        public ICommand ToProfile { get; set; }
        /// <summary>
        /// Комманда перехода на окно составления заявления
        /// </summary>
        public ICommand ToStatement { get; set; }
        /// <summary>
        /// Комманда перехода на окно расписания мероприятий
        /// </summary>
        public ICommand ToEvents { get; set; }

        #endregion

        #region Commands Workers

        /// <summary>
        /// Комманда перехода на окно графика мероприятий (для сотрудников)
        /// </summary>
        public ICommand ToEventsList { get; set; }
        /// <summary>
        /// Комманда перехода на окно графика дежурств
        /// </summary>
        public ICommand ToDutyChart { get; set; }
        /// <summary>
        /// Комманда перехода на окно со списком студентов
        /// </summary>
        public ICommand ToListStudents { get; set; }
        /// <summary>
        /// Комманда перехода на окно экрана чистоты
        /// </summary>
        public ICommand ToPurityChart { get; set; }

        #endregion

        /// <summary>
        /// Комманда перехода на окно справки (FAQ)
        /// </summary>
        public ICommand ToFaq { get; set; }
        /// <summary>
        /// Комманда выхода из учетной записи и переход на окно авторизации
        /// </summary>
        public ICommand Exit { get; set; }

        #endregion

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
        public bool IsOpenSideBar { get; set; } = false;
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
