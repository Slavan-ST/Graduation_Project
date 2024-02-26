using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;

namespace Client.ViewModels;

public class ViewModelBase : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; set; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    /// <summary>
    /// базовая модель
    /// </summary>
    /// <param name="screen">
    /// screen, к которому будет привязана созданная модель, 
    /// т.е. данная модель будет находиться внутри указанного screen
    /// </param>
    public ViewModelBase(IScreen? screen = null)
    {
        HostScreen = screen ?? Locator.Current.GetService<IScreen>()!;
    }

}
