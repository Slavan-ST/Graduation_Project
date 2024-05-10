using ReactiveUI;
using System;
using System.Reactive;

namespace Client.ViewModels;

public class MainViewModel : ViewModelBaseNavigator
{
    public MainViewModel(IScreen? screen = null) : base(screen)
    {
        Initialize();
    }

    public MainViewModel() : base()
    {
        Initialize();
    }
    void Initialize()
    {
        //Router.Navigate.Execute(new AuthViewModel(this));
        Router.Navigate.Execute(new MainMenuViewModel(this));
    }

}
