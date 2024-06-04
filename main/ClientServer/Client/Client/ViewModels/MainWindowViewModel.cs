using ReactiveUI;

namespace Client.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        public MainWindowViewModel()
        {
            Router.Navigate.Execute(new AuthViewModel(this));
        }
        public RoutingState Router { get; } = new RoutingState();
    }
}
