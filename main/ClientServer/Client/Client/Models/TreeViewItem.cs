using System.Collections.ObjectModel;

namespace Client.Models
{
    public class TreeViewItem
    {
        public ObservableCollection<TreeViewItem>? Answers { get; set; }
        public string Title { get; set; }

        public TreeViewItem(string title)
        {
            Title = title;
        }

        public TreeViewItem(string title, ObservableCollection<TreeViewItem> subNodes)
        {
            Title = title;
            Answers = subNodes;
        }
    }
}
