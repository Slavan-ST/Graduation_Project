using Client.ViewModels;
using ReactiveUI;
using System.Reactive;

namespace Client.ViewModels;

public class MainViewModel : ReactiveObject, IScreen
{
    //роутер на котором завязана навигация
    public RoutingState Router { get; } = new RoutingState();

    // Пример команды перехода
    public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

    public MainViewModel()
    {
        GoNext = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new MainMenuViewModel(this))
            );
    }
}
