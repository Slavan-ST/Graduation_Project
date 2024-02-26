using Client.ViewModels;
using ReactiveUI;
using System;
using System.Reactive;

namespace Client.ViewModels;

public class MainViewModel : ViewModelBaseNavigator
{
    // Пример команды перехода
    public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

    public MainViewModel(IScreen? screen = null) : base(screen)
    {
        GoNext = ReactiveCommand.CreateFromObservable(() => this.Router.Navigate.Execute(new MainMenuViewModel(this)) );
    }
}
