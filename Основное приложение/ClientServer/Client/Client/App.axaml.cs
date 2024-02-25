using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Client.Models;
using Client.Services;
using Client.ViewModels;
using Client.Views;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using System;
using System.Diagnostics;

namespace Client;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();


        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };
            services.AddSingleton<Message>(new Message(desktop.MainWindow));
            services.AddSingleton<Services.FileDialog>(new Services.FileDialog(desktop.MainWindow));
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView();
            services.AddSingleton<Message>(new Message((singleViewPlatform.MainView as UserControl)!));
            services.AddSingleton<Services.FileDialog>(new Services.FileDialog((singleViewPlatform.MainView as UserControl)!));
        }


        Locator.CurrentMutable.Register(() => new AuthView(), typeof(IViewFor<AuthViewModel>));
        Locator.CurrentMutable.Register(() => new DutyChartView(), typeof(IViewFor<DutyChartViewModel>));
        Locator.CurrentMutable.Register(() => new EventsListView(), typeof(IViewFor<EventsListViewModel>));
        Locator.CurrentMutable.Register(() => new EventsView(), typeof(IViewFor<EventsViewModel>));
        Locator.CurrentMutable.Register(() => new FaqView(), typeof(IViewFor<FaqViewModel>));
        Locator.CurrentMutable.Register(() => new ListStudentsView(), typeof(IViewFor<ListStudentsViewModel>));
        Locator.CurrentMutable.Register(() => new MainMenuView(), typeof(IViewFor<MainMenuViewModel>));
        Locator.CurrentMutable.Register(() => new NewsView(), typeof(IViewFor<NewsViewModel>));
        Locator.CurrentMutable.Register(() => new ProfileView(), typeof(IViewFor<ProfileViewModel>));
        Locator.CurrentMutable.Register(() => new PurityChartView(), typeof(IViewFor<PurityChartViewModel>));
        Locator.CurrentMutable.Register(() => new SideBarView(), typeof(IViewFor<SideBarViewModel>));
        Locator.CurrentMutable.Register(() => new StatementView(), typeof(IViewFor<StatementViewModel>));

        Locator.CurrentMutable.RegisterConstant<IScreen>(new MainViewModel());
        Services = services.BuildServiceProvider();
        base.OnFrameworkInitializationCompleted();
    }
    public new static App? Current => Application.Current as App;
    public IServiceProvider? Services { get; private set; }
}
