using Client.ViewModels;
using ReactiveUI;
using System;
using System.Reactive;

namespace Client.ViewModels;

public class MainViewModel : ViewModelBase, IScreen
{
    //роутер на котором завязана навигация
    public RoutingState Router { get; } = new RoutingState();

    // Пример команды перехода
    public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

    public MainViewModel(IScreen screen) : base(screen)
    {
        GoNext = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new MainMenuViewModel(this))
            );
    }
}
