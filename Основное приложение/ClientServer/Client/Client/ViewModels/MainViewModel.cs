using ReactiveUI;
using System;
using System.Reactive;

namespace Client.ViewModels;

public class MainViewModel : ViewModelBaseNavigator
{
    public MainViewModel(IScreen? screen = null) : base(screen)
    {
        Router.Navigate.Execute(new FaqViewModel(this));
    }

    public MainViewModel() : base()
    {
        Router.Navigate.Execute(new FaqViewModel(this));
    }

}
