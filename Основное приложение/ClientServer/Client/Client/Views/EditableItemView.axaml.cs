using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Client.Views
{
    public partial class EditableItemView : ReactiveUserControl<EditableItemViewModel>
    {
        public EditableItemView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
