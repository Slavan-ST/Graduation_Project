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
    [TemplatePart("EditableTextBlockPresenter", typeof(ItemsControl))]
    public class EditableTextBlock : TemplatedControl
    {

        static EditableTextBlock()
        {
            Application.Current?.Resources.MergedDictionaries
                .Add(new ResourceInclude(new Uri("avares://Client/Controls/EditableTextBlock.axaml"))
                {
                    Source = new Uri("avares://Client/Controls/EditableTextBlock.axaml")
                });
        }
        public EditableTextBlock()
        {

        }

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
