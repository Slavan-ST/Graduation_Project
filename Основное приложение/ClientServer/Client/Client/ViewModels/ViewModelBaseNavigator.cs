using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive;

namespace Client.ViewModels;

public class ViewModelBaseNavigator : ViewModelBase, IScreen
{
    //роутер на котором завязана навигация
    public RoutingState Router { get; } = new RoutingState();

    public ViewModelBaseNavigator(IScreen screen) : base(screen)
    {

    }

}
