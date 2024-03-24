using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels;
using ReactiveUI;

namespace Client.Views
{
    public partial class RecordStudentsView : ReactiveUserControl<RecordStudentsViewModel>
    {
        public RecordStudentsView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
            if (DataContext is RecordStudentsViewModel)
            {
                //clear out any existing columns
                while (ExampleDatagrid.Columns.Count > 0) { ExampleDatagrid.Columns.RemoveAt(ExampleDatagrid.Columns.Count - 1); }
                //assign the datatable to the grid
                ExampleDatagrid.ItemsSource = (DataContext as RecordStudentsViewModel).PeopleTable.DefaultView;

                // create the grid columns based on the datatables columns
                foreach (System.Data.DataColumn x in (DataContext as RecordStudentsViewModel).PeopleTable.Columns)
                {
                    if (x.DataType == typeof(bool))
                    {
                        ExampleDatagrid.Columns.Add(new DataGridCheckBoxColumn { Header = x.ColumnName, Binding = new Avalonia.Data.Binding($"Row.ItemArray[{x.Ordinal}]") });
                    }
                    else
                    {
                        ExampleDatagrid.Columns.Add(new DataGridTextColumn { Header = x.ColumnName, Binding = new Avalonia.Data.Binding($"Row.ItemArray[{x.Ordinal}]") });
                    }
                }
            }
        }
    }
}
