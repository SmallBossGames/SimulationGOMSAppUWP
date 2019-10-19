using SimualtionGOMSApp_UWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimualtionGOMSApp_UWP.ViewModel
{
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            TimeErrorPairs = new ObservableCollection<TimeFromErrorPair>() 
            {
                new TimeFromErrorPair{Error=0.1, Time=6.5},
                new TimeFromErrorPair{Error=0.2, Time=6.5},
                new TimeFromErrorPair{Error=0.3, Time=9.5},
                new TimeFromErrorPair{Error=0.4, Time=2.5},
                new TimeFromErrorPair{Error=0.5, Time=7.5},
            };
            OuterNodes = new ObservableCollection<OuterNodeModel>()
            {
                new OuterNodeModel{Name = "Проверка", GOMSChars=string.Empty, IsEndNode=false},
                new OuterNodeModel{Name = "Проверка", GOMSChars=string.Empty, IsEndNode=false},
                new OuterNodeModel{Name = "Проверка", GOMSChars=string.Empty, IsEndNode=false},
                new OuterNodeModel{Name = "Проверка", GOMSChars=string.Empty, IsEndNode=false},
                new OuterNodeModel{Name = "Проверка", GOMSChars=string.Empty, IsEndNode=false},
                new OuterNodeModel{Name = "Проверка", GOMSChars=string.Empty, IsEndNode=false},
                new OuterNodeModel{Name = "Проверка", GOMSChars=string.Empty, IsEndNode=false},
            };
            NodeMappings = new ObservableCollection<NodeMappingModel>()
            {
                new NodeMappingModel{FirstNode = "Проверка", SecondNode = "Проверка", Weight=5},
                new NodeMappingModel{FirstNode = "Проверка", SecondNode = "Проверка", Weight=5},
                new NodeMappingModel{FirstNode = "Проверка", SecondNode = "Проверка", Weight=5},
                new NodeMappingModel{FirstNode = "Проверка", SecondNode = "Проверка", Weight=5},
                new NodeMappingModel{FirstNode = "Проверка", SecondNode = "Проверка", Weight=5},
                new NodeMappingModel{FirstNode = "Проверка", SecondNode = "Проверка", Weight=5},
                new NodeMappingModel{FirstNode = "Проверка", SecondNode = "Проверка", Weight=5},
            };
        }

        public ObservableCollection<TimeFromErrorPair> TimeErrorPairs { get; }
        public ObservableCollection<OuterNodeModel> OuterNodes { get; set; }
        public ObservableCollection<NodeMappingModel> NodeMappings { get; set; }
    }
}
