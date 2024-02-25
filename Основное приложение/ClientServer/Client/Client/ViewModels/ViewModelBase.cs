using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;

namespace Client.ViewModels;

public class ViewModelBase : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    public ViewModelBase(IScreen? screen = null)
    {
        HostScreen = screen ?? Locator.Current.GetService<IScreen>()!;
    }

}
