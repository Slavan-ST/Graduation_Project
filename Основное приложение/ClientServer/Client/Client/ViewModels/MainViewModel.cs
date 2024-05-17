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

#if DEBUG
        //Router.Navigate.Execute(new MainMenuViewModel(this));
        Router.Navigate.Execute(new AuthViewModel(this));
#else
        Router.Navigate.Execute(new MainMenuViewModel(this));
#endif

    }

}
