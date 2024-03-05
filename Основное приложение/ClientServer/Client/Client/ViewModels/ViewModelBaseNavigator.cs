using ReactiveUI;
using Splat;

namespace Client.ViewModels;

public class ViewModelBaseNavigator : ViewModelBase, IScreen
{
    public ViewModelBaseNavigator(IScreen? screen = null):base(screen) {}

    public RoutingState Router { get; set; } = new RoutingState();
}
