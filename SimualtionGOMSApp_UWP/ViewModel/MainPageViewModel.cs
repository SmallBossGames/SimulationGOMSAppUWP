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
            TimeErrorPairs = new ObservableCollection<TimeFromErrorPair>();
            /*{
                new TimeFromErrorPair{Error=0.1, Time=6.5},
                new TimeFromErrorPair{Error=0.2, Time=6.5},
                new TimeFromErrorPair{Error=0.3, Time=9.5},
                new TimeFromErrorPair{Error=0.4, Time=2.5},
                new TimeFromErrorPair{Error=0.5, Time=7.5},
            };*/
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
            var parameters = new SimualtionGOMS.Parameters(
                keyboard: SimulationParmeters.Keyboard,
                handMoving: SimulationParmeters.HandMoving,
                menthal: SimulationParmeters.Menthal,
                positioning: SimulationParmeters.Positioning);

            var (outerNodes, mapping) = ConvertOuterNodes(OuterNodes, NodeMappings);
            var graph = SimualtionGOMS.SimulationGOMS.BuildGraph(parameters, outerNodes, mapping);

            return SimualtionGOMS.SimulationGOMS.Simulate(graph, errorPropability);
        }

        public void SimulationRange()
        {
            const double simulationItCount = 1000; 

            if (SimulationParmeters.StepError < 1)
                return;

            var parameters = new SimualtionGOMS.Parameters(
                keyboard: SimulationParmeters.Keyboard,
                handMoving: SimulationParmeters.HandMoving,
                menthal: SimulationParmeters.Menthal,
                positioning: SimulationParmeters.Positioning);
            var (outerNodes, mapping) = ConvertOuterNodes(OuterNodes, NodeMappings);
            var stepError = (SimulationParmeters.MaxError - SimulationParmeters.MinError) / SimulationParmeters.StepError;
            var graph = SimualtionGOMS.SimulationGOMS.BuildGraph(parameters, outerNodes, mapping);

            TimeErrorPairs.Clear();

            for (int i = 0; i <= SimulationParmeters.StepError; i++)
            {
                var avgResult = 0.0;
                var errorPropability = SimulationParmeters.MinError + i * stepError;

                for (var j = 0; j < simulationItCount; j++)
                {
                    avgResult += SimualtionGOMS.SimulationGOMS.Simulate(graph, errorPropability);
                }

                avgResult /= simulationItCount;

                var tePair = new TimeFromErrorPair { Error = errorPropability, Time = avgResult };

                TimeErrorPairs.Add(tePair);
            }
        }

        static private SimualtionGOMS.Token[] ParseGomsTokens(string source)
        {
            var tokens = new List<SimualtionGOMS.Token>(source.Length);
            foreach (var item in source)
            {
                switch (item)
                {
                    case 'M':
                    case 'm':
                        tokens.Add(SimualtionGOMS.Token.menthal);
                        break;
                    case 'H':
                    case 'h':
                        tokens.Add(SimualtionGOMS.Token.handMoving);
                        break;
                    case 'K':
                    case 'k':
                        tokens.Add(SimualtionGOMS.Token.keyboard);
                        break;
                    case 'P':
                    case 'p':
                        tokens.Add(SimualtionGOMS.Token.positioning);
                        break;
                    default:
                        break;
                }
            }
            return tokens.ToArray();
        }

        static private (SimualtionGOMS.OuterNode[], (int, int, double)[]) ConvertOuterNodes(
            Collection<OuterNodeModel> outerNodeModels,
            Collection<NodeMappingModel> mappingModels)
        {
            var outerNodes = new SimualtionGOMS.OuterNode[outerNodeModels.Count];
            var nodeMapping = new (int, int, double)[mappingModels.Count];
           
            var nameIndexMatch = new Dictionary<string, int>();

            for (int i = 0; i < outerNodes.Length; i++)
            {
                var current = outerNodeModels[i];
                outerNodes[i] = new SimualtionGOMS.OuterNode(
                    isEndNode: current.IsEndNode,
                    gomsTokens: ParseGomsTokens(current.GOMSChars));
                nameIndexMatch.Add(current.Name, i);
            }

            for (int i = 0; i < nodeMapping.Length; i++)
            {
                var current = mappingModels[i];
                nodeMapping[i] = (nameIndexMatch[current.FirstNode], nameIndexMatch[current.SecondNode], current.Weight);
            }

            return (outerNodes, nodeMapping);
        }
    }
}
