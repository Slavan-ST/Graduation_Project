using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI.Fody.Helpers;

namespace Client.Controls
{
    public class EditableTextBlock : TemplatedControl
    {
        public static readonly StyledProperty<string> InsideTextPropety = AvaloniaProperty.Register<EditableTextBlock, string>(nameof(InsideText));

        public string InsideText
        {
            get
            {
                return GetValue(InsideTextPropety);
            }
            set
            {
                SetValue(InsideTextPropety, value);
            }
        }
    }
}
