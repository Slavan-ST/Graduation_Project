using Client.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace maket.ViewModels
{
    public class SideBarViewModel : ViewModelBase
    {
        #region Commands

        ReactiveCommand<Unit, Unit> Faq {  get; set; }

        #endregion

        public SideBarViewModel() 
        {
            Faq = ReactiveCommand.Create(() =>
            {
                
            });
        }
    }
}
