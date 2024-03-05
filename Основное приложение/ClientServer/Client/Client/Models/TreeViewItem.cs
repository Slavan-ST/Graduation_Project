using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
