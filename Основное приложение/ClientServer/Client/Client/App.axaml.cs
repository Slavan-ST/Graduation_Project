using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Client.Models;
using Client.Services;
using Client.ViewModels;
using Client.Views;
using Microsoft.Extensions.DependencyInjection;
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
            desktop.MainWindow = new MainWindow();
            services.AddSingleton<Message>(new Message(desktop.MainWindow));
            services.AddSingleton<Services.FileDialog>(new Services.FileDialog(desktop.MainWindow));
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView();
            services.AddSingleton<Message>(new Message((singleViewPlatform.MainView as UserControl)!));
            services.AddSingleton<Services.FileDialog>(new Services.FileDialog((singleViewPlatform.MainView as UserControl)!));
        }


        Services = services.BuildServiceProvider();
        base.OnFrameworkInitializationCompleted();
    }
    public new static App? Current => Application.Current as App;
    public IServiceProvider? Services { get; private set; }
}
