using Client.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Client.ViewModels;

public class ViewModelBaseNavigator : ViewModelBase, IScreen
{
    public ViewModelBaseNavigator(IScreen? screen = null) : base(screen)
    {

    }

    public ViewModelBaseNavigator() : base()
    {

    }
    [Reactive]
    public RoutingState Router { get; set; } = new RoutingState();
}
