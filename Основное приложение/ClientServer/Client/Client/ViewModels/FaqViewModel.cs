using Client.Models;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class FaqViewModel : ViewModelBase
    {
        [Reactive]
        public ObservableCollection<Node>? QuestAnswer { get; set; }

        [Reactive]
        public ObservableCollection<Node>? Nodes { get; set; }

        public FaqViewModel() 
        {
            QuestAnswer = new ObservableCollection<Node>
            {
                new Node("Вопрос1", new ObservableCollection<Node>
                    {
                        new Node("Ответ1")
                    }),

                new Node("Вопрос2", new ObservableCollection<Node>
                    {
                        new Node("Ответ2")
                    }),

                new Node("Вопрос3"), 
                new Node("Вопрос4"), 
                new Node("Вопрос5")

            };
        }
    }
}
