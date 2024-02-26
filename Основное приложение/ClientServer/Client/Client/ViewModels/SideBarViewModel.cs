using Client.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class SideBarViewModel : ViewModelBase
    {
        #region Commands

        ReactiveCommand<Unit, Unit> Faq {  get; set; }

        #endregion

        public SideBarViewModel(IScreen? screen = null) : base(screen)
        {
            Faq = ReactiveCommand.Create(() =>
            {
                
            });
        }
    }
}
