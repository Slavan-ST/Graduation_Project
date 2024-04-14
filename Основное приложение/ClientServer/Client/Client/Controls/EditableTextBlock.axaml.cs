using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.Styling;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI.Fody.Helpers;
using System;

namespace Client.Controls
{
    public class EditableTextBlock : UserControl
    {

        public static readonly StyledProperty<string> InsideTextProperty = AvaloniaProperty.Register<EditableTextBlock, string>(nameof(InsideText));

        public string InsideText
        {
            get
            {
                return GetValue(InsideTextProperty);
            }
            set
            {
                SetValue(InsideTextProperty, value);
            }
        }
    }
}
