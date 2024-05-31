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
using Avalonia.Controls.Chrome;
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
        public MainMenuViewModel(IScreen? screen = null) : base(screen)
        {
            Initialize();
            UnVisible("Guest");
        }
        public MainMenuViewModel(IScreen? screen = null, string role = "User") : base(screen)
        {
            Initialize();
            UnVisible(role);
        }
        void UnVisible(string role)
        {
            if (role == "Admin")
            {
                IsAdmin = true;
                IsStudents = false;
                IsWorker = false;
                Title = "Регистрация";
                Router.Navigate.Execute(new RegistrationViewModel(this));
            }
            if (role == "Moderator")
            {
                IsAdmin = false;
                IsStudents = false;
                IsWorker = true;
                Title = "Список студентов";
                Router.Navigate.Execute(new ListStudentsViewModel(this));
            }
            if (role == "MegaAdmin")
            {
                IsAdmin = true;
                IsStudents = true;
                IsWorker = true;
                Title = "Вопрос-ответ";
                Router.Navigate.Execute(new EventsViewModel(this));
            }
            if (role == "User")
            {
                IsAdmin = false;
                IsStudents = true;
                IsWorker = false;
                Title = "Мероприятия";
                Router.Navigate.Execute(new EventsViewModel(this));
            }
        }
        void Initialize()
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
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new NewsViewModel(this));
                Title = "Новости";
                IsOpenSideBar = false;
            });

            DutyChart = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new DutyChartListViewModel(this));
                Title = "Дежурства";
                IsOpenSideBar = false;
            });
            Events = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new EventsViewModel(this));
                Title = "Мероприятия";
                IsOpenSideBar = false;
            });

            EventsList = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new EventsListViewModel(this));
                Title = "План мероприятий";
                IsOpenSideBar = false;
            });

            Faq = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new FaqViewModel(this));
                Title = "Вопрос-ответ";
                IsOpenSideBar = false;
            });
            Group = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new GroupViewModel(this));
                Title = "Группы";
                IsOpenSideBar = false;
            });
            Role = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new RoleViewModel(this));
                Title = "Роли";
                IsOpenSideBar = false;
            });
            Room = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new RoomViewModel(this));
                Title = "Комнаты";
                IsOpenSideBar = false;
            });
            Status = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new StatusViewModel(this));
                Title = "Статусы";
                IsOpenSideBar = false;
            });

            ListStudents = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new ListStudentsViewModel(this));
                Title = "Список студентов";
                IsOpenSideBar = false;
            });

            PurityChart = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new PurityChartViewModel(this));
                Title = "Экран чистоты";
                IsOpenSideBar = false;
            });

            Statement = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new StatementViewModel(this));
                Title = "Заявление";
                IsOpenSideBar = false;
            });

            Profile = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new ProfileViewModel(this));
                Title = "Профиль";
                IsOpenSideBar = false;
            });

            Record = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new RecordYearViewModel(this));
                Title = "Годовой журнал";
                IsOpenSideBar = false;
            });

            CleanRaid = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new CleanLineRaidViewModel(this));
                Title = "Рейд чистоты";
                IsOpenSideBar = false;
            });

            DailyCheck = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new DailyCheckViewModel(this));
                Title = "Ежедневная проверка";
                IsOpenSideBar = false;
            });
            Registration = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new RegistrationViewModel(this));
                Title = "Регистрация";
                IsOpenSideBar = false;
            });
            RestorePassword = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new RestorePasswordViewModel(this));
                Title = "Восстановление пароля";
                IsOpenSideBar = false;
            });
            DutySchulde = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                Router.Navigate.Execute(new DutyChartViewModel(this));
                Title = "План дежурств";
                IsOpenSideBar = false;
            });


            Exit = ReactiveCommand.Create(() =>
            {
                Router.Navigate.Dispose();
                // + сюда добавить выход из учётной записи
                HostScreen.Router.Navigate.Execute(new AuthViewModel(HostScreen));
            });

        }

        #region Commands

        /// <summary>
        /// Комманда для сокрытия SideBar'a
        /// </summary>
        public ICommand? HideSideBar { get; set; }
        /// <summary>
        /// Комманда для открытия SideBar'a
        /// </summary>
        public ICommand? OpenSideBar { get; set; }

        public ICommand? DutySchulde { get; set; }
        /// <summary>
        /// Команда перехода на регистрацию пользователей
        /// </summary>
        public ICommand? Registration { get; set; }
        /// <summary>
        /// Команда перехода на окно восстановления пароля
        /// </summary>
        public ICommand? RestorePassword { get; set; }
        /// <summary>
        /// Команда перехода на главную 
        /// </summary>
        public ICommand? Main { get; set; }




        /// <summary>
        /// Команда перехода на окно профиля
        /// </summary>
        public ICommand? Profile { get; set; }
        /// <summary>
        /// Команда перехода на журнал
        /// </summary>
        public ICommand? Record { get; set; }
        /// <summary>
        /// Комманда перехода на окно составления заявления
        /// </summary>
        public ICommand? Statement { get; set; }
        /// <summary>
        /// Комманда перехода на окно расписания мероприятий
        /// </summary>
        public ICommand? Events { get; set; }
        /// <summary>
        /// Комманда перехода на окно ежедневной проверки
        /// </summary>
        public ICommand? DailyCheck { get; set; }

        /// <summary>
        /// Комманда перехода на окно графика мероприятий (для сотрудников)
        /// </summary>
        public ICommand? EventsList { get; set; }
        /// <summary>
        /// Комманда перехода на окно графика дежурств
        /// </summary>
        public ICommand? DutyChart { get; set; }
        /// <summary>
        /// Комманда перехода на окно со списком студентов
        /// </summary>
        public ICommand? ListStudents { get; set; }
        /// <summary>
        /// Комманда перехода на окно экрана чистоты
        /// </summary>
        public ICommand? PurityChart { get; set; }
        /// <summary>
        /// Комманда перехода на окно "рейда" чистоты
        /// </summary>
        public ICommand? CleanRaid { get; set; }
        /// <summary>
        /// Команда перехода на окно с комнатами
        /// </summary>
        [Reactive]
        public ICommand? Room {  get; set; }
        /// <summary>
        /// Команда перехода на окно с статусами
        /// </summary>
        [Reactive]
        public ICommand? Status { get; set; }
        /// <summary>
        /// Команда перехода на окно с группами
        /// </summary>
        [Reactive]
        public ICommand? Group { get; set; }
        /// <summary>
        /// Команда перехода на окно с ролями
        /// </summary>
        [Reactive]
        public ICommand? Role { get; set; }
        /// <summary>
        /// Комманда перехода на окно справки (FAQ)
        /// </summary>
        public ICommand? Faq { get; set; }
        /// <summary>
        /// Комманда выхода из учетной записи и переход на окно авторизации
        /// </summary>
        public ICommand? Exit { get; set; }
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
        /// <summary>
        /// Свойство показывающие относится ли учетная запись пользователя к роли "Администратор"
        /// </summary>
        [Reactive]
        public bool IsAdmin { get; set; } = true;
        /// <summary>
        /// Свойство отображающие имя текущей формы
        /// </summary>
        /// <value>
        /// "Новости" по умолчанию
        /// </value>
        [Reactive]
        public string Title { get; set; } = "Мероприятия";

        #endregion
    }
}
