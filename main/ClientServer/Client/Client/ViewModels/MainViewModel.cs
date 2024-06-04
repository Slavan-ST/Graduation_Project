using ReactiveUI;

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
        Router.Navigate.Execute(new AuthViewModel(this));
        //Router.Navigate.Execute(new AuthViewModel(this));
#else
        Router.Navigate.Execute(new AuthViewModel(this));
#endif

    }

}
