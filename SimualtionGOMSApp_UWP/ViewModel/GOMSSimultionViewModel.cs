using SimualtionGOMSApp_UWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimualtionGOMSApp_UWP.ViewModel
{
    public sealed class GOMSSimultionViewModel
    {
        public GOMSSimultionViewModel()
        {
            TimeErrorPairs = new ObservableCollection<TimeFromErrorPair>();
            OuterNodes = new ObservableCollection<OuterNodeModel>()
            {
                new OuterNodeModel{Name = "Первый", GOMSChars="MHPHK", IsEndNode=false},
                new OuterNodeModel{Name = "Второй", GOMSChars="MHPHK", IsEndNode=false},
                new OuterNodeModel{Name = "Третий", GOMSChars="MHPHK", IsEndNode=false},
                new OuterNodeModel{Name = "Четвёртый", GOMSChars="MHPHK", IsEndNode=true},
                new OuterNodeModel{Name = "Пятый", GOMSChars="MHPHK", IsEndNode=true},
                new OuterNodeModel{Name = "Шестой", GOMSChars="MHPHK", IsEndNode=true},
            };
            NodeMappings = new ObservableCollection<NodeMappingModel>()
            {
                new NodeMappingModel{FirstNode = "Первый", SecondNode = "Второй", Weight = 0.5 },
                new NodeMappingModel{FirstNode = "Первый", SecondNode = "Третий", Weight = 0.5},
                new NodeMappingModel{FirstNode = "Второй", SecondNode = "Четвёртый", Weight = 0.5},
                new NodeMappingModel{FirstNode = "Второй", SecondNode = "Третий", Weight=0.5},
                new NodeMappingModel{FirstNode = "Третий", SecondNode = "Шестой", Weight=1.0},
                new NodeMappingModel{FirstNode = "Пятый", SecondNode = "Второй", Weight=1.0},
                new NodeMappingModel{FirstNode = "Шестой", SecondNode = "Пятый", Weight=1.0},
            };
            SimulationParmeters = new SimulationParmetersModel();
        }

        public ObservableCollection<TimeFromErrorPair> TimeErrorPairs { get; }
        public ObservableCollection<OuterNodeModel> OuterNodes { get; }
        public ObservableCollection<NodeMappingModel> NodeMappings { get; }
        public SimulationParmetersModel SimulationParmeters { get; }

        public int SelectedOtherNodeIndex { get; set; }
        public int SelectedNodeMapIndex { get; set; }

        public double Simulation(double errorPropability)
        {
            var parameters = new GOMS.Parameters(
                keyboard: SimulationParmeters.Keyboard,
                handMoving: SimulationParmeters.HandMoving,
                menthal: SimulationParmeters.Menthal,
                positioning: SimulationParmeters.Positioning);

            var (outerNodes, mapping) = Helpers.ConvertOuterNodes(OuterNodes, NodeMappings);
            var graph = GOMS.SimulationGOMS.BuildGraph(parameters, outerNodes, mapping);

            return GOMS.SimulationGOMS.Simulate(graph, errorPropability);
        }

        public void SimulationRange()
        {
            const double simulationItCount = 1000; 

            if (SimulationParmeters.StepError < 1)
                return;

            var parameters = new GOMS.Parameters(
                keyboard: SimulationParmeters.Keyboard,
                handMoving: SimulationParmeters.HandMoving,
                menthal: SimulationParmeters.Menthal,
                positioning: SimulationParmeters.Positioning);
            var (outerNodes, mapping) = Helpers.ConvertOuterNodes(OuterNodes, NodeMappings);
            var stepError = (SimulationParmeters.MaxError - SimulationParmeters.MinError) / SimulationParmeters.StepError;
            var graph = GOMS.SimulationGOMS.BuildGraph(parameters, outerNodes, mapping);

            TimeErrorPairs.Clear();

            for (int i = 0; i <= SimulationParmeters.StepError; i++)
            {
                var avgResult = 0.0;
                var errorPropability = SimulationParmeters.MinError + i * stepError;

                for (var j = 0; j < simulationItCount; j++)
                {
                    avgResult += GOMS.SimulationGOMS.Simulate(graph, errorPropability);
                }

                avgResult /= simulationItCount;

                var tePair = new TimeFromErrorPair { Error = errorPropability, Time = avgResult };

                TimeErrorPairs.Add(tePair);
            }
        }

        
    }
}
