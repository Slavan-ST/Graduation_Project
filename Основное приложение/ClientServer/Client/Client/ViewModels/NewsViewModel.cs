using Client.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Client.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;
        public NewsViewModel(IScreen? screen = null) : base(screen)
        {

        }
    }
}
