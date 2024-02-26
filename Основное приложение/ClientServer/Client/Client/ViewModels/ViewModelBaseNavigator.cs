using ReactiveUI;
using Splat;

namespace Client.ViewModels;

public class ViewModelBaseNavigator : ViewModelBase, IScreen
{

    public ViewModelBaseNavigator(IScreen? screen = null):base(screen)
    {
        HostScreen = screen ?? Locator.Current.GetService<IScreen>()!;
    }

    public RoutingState Router { get; set; } = new RoutingState();
}
