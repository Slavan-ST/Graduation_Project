using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Client.Models;
using Client.Services;
using Client.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Client.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public MainMenuViewModel()
        {
            HideSideBar = ReactiveCommand.Create(() =>
            {
                IsOpenSideBar =  false;
                NumColumn = 0;
            });

            OpenSideBar = ReactiveCommand.Create(() =>
            {
                Navigation.WorkPlace = new NewsViewModel();
                IsOpenSideBar =  true;
                if (Navigation.MainWindow?.Bounds.Width > 600)
                {
                    NumColumn = 1;
                }
            });

            ToMain = ReactiveCommand.Create(() =>
            {
                Navigation.WorkPlace = Navigation.News;
            });

            ToDutyChart = ReactiveCommand.Create(() =>
            {
                Navigation.WorkPlace = Navigation.DutyChart;
            });

            ToEvents = ReactiveCommand.Create(() =>
            {
                Navigation.WorkPlace = Navigation.Events;
            });

            ToEventsList = ReactiveCommand.Create(() =>
            {
                Navigation.WorkPlace = Navigation.EventsList;
            });

            ToFaq = ReactiveCommand.Create(() =>
            {
                Navigation.WorkPlace = Navigation.Faq;
            });

            ToListStudents = ReactiveCommand.Create(() =>
            {
                Navigation.WorkPlace = Navigation.ListStudents;
            });

            ToPurityChart = ReactiveCommand.Create(() =>
            {
                Navigation.WorkPlace = Navigation.PurityChart;
            });

            ToStatement = ReactiveCommand.Create(() =>
            {
                Navigation.WorkPlace = Navigation.Statement;
            });

            ToProfile = ReactiveCommand.Create(() =>
            {
                Navigation.WorkPlace = Navigation.Profile;
            });

            Exit = ReactiveCommand.Create(() =>
            {
                Navigation.MainWindow.Content = Navigation.Authification;
            });
        }

        #region Commands

        /// <summary>
        /// Комманда для сокрытия SideBar'a
        /// </summary>
        public ReactiveCommand<Unit, Unit> HideSideBar { get; set; }
        /// <summary>
        /// Комманда для открытия SideBar'a
        /// </summary>
        public ReactiveCommand<Unit, Unit> OpenSideBar { get; set; }

            #region SideBar Commands

            /// <summary>
            /// Команда перехода на главную 
            /// </summary>
            public ReactiveCommand<Unit, Unit> ToMain { get; set; }

                #region Commands Students

                /// <summary>
                /// Команда перехода на окно профиля
                /// </summary>
                public ReactiveCommand<Unit, Unit> ToProfile { get; set; }
                /// <summary>
                /// Комманда перехода на окно составления заявления
                /// </summary>
                public ReactiveCommand<Unit, Unit> ToStatement { get; set; }
                /// <summary>
                /// Комманда перехода на окно расписания мероприятий
                /// </summary>
                public ReactiveCommand<Unit, Unit> ToEvents { get; set; }

            #endregion

                #region Commands Workers

                /// <summary>
                /// Комманда перехода на окно графика мероприятий (для сотрудников)
                /// </summary>
                public ReactiveCommand<Unit, Unit> ToEventsList { get; set; }
                /// <summary>
                /// Комманда перехода на окно графика дежурств
                /// </summary>
                public ReactiveCommand<Unit, Unit> ToDutyChart { get; set; }
                /// <summary>
                /// Комманда перехода на окно со списком студентов
                /// </summary>
                public ReactiveCommand<Unit, Unit> ToListStudents { get; set; }
                /// <summary>
                /// Комманда перехода на окно экрана чистоты
                /// </summary>
                public ReactiveCommand<Unit, Unit> ToPurityChart { get; set; }

                #endregion

            /// <summary>
            /// Комманда перехода на окно справки (FAQ)
            /// </summary>
            public ReactiveCommand<Unit, Unit> ToFaq { get; set; }
            /// <summary>
            /// Комманда выхода из учетной записи и переход на окно авторизации
            /// </summary>
            public ReactiveCommand<Unit, Unit> Exit { get; set; }

        #endregion

        #endregion

        #region Propertyes

        /// <summary>
        /// Рабочее простарнство на интерфейсе
        /// </summary>
        [Reactive]
        public ViewModelBase WorkPlace { get; set; } = Navigation.News = new NewsViewModel();
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
