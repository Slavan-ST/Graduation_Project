using Avalonia.Controls;
using Client.ViewModels;
using Client.Views;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public static class Navigation
    {
        #region AllViewModel

            #region CommonProperiesVM

            /// <summary>
            /// Основное окно
            /// </summary>
            public static Window? MainWindow { get; set; }
            /// <summary>
            /// ViewModel основного представления
            /// </summary>
            public static ViewModelBase MainMenu { get; set; } = new MainMenuViewModel();
            /// <summary>
            /// Модель представления рабочей зоны (по умолчанию новостной блок)
            /// </summary>
            public static ViewModelBase WorkPlace { get; set; } = new NewsViewModel();

        #endregion

        #region UserConrols

        /// <summary>
        /// Модель представления авторизации
        /// </summary>
        public static ViewModelBase Authification { get; set; } = new AuthViewModel();
        /// <summary>
        /// Модель представления новостного блока
        /// </summary>
        public static ViewModelBase? News { get; set; }
        /// <summary>
        /// Модель представления профиля
        /// </summary>
        public static ViewModelBase Profile { get; set; } = new ProfileViewModel();
        /// <summary>
        /// Модель представления состовяления завления
        /// </summary>
        public static ViewModelBase Statement { get; set; } = new StatementViewModel();
        /// <summary>
        /// Модель представления ближайших мероприятий
        /// </summary>
        public static ViewModelBase Events { get; set; } = new EventsViewModel();
        /// <summary>
        /// Модель представления списка мероприятий (для сотрудников)
        /// </summary>
        public static ViewModelBase EventsList { get; set; } = new EventsListViewModel();
        /// <summary>
        /// Модель представления графика дежурств
        /// </summary>
        public static ViewModelBase DutyChart { get; set; } = new DutyChartViewModel();
        /// <summary>
        /// Модель представления списка студентов
        /// </summary>
        public static ViewModelBase ListStudents { get; set; } = new ListStudentsViewModel();
        /// <summary>
        /// Модель представления экрана чистоты
        /// </summary>
        public static ViewModelBase PurityChart { get; set; } = new PurityChartViewModel();
        /// <summary>
        /// Модель представления часто задаваемых вопросов
        /// </summary>
        public static ViewModelBase Faq { get; set; } = new FaqViewModel();

        #endregion

        #endregion

    }
}
