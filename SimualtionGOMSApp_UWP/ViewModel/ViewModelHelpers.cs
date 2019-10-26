using SimualtionGOMSApp_UWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using SimualtionGOMSApp_UWP.SimulationWorkstation;

namespace SimualtionGOMSApp_UWP.ViewModel
{
    public static class ViewModelHelpers
    {
        static public GOMS.OuterNode[] ConvertOuterNodes(Collection<OuterNodeModel> outerNodeModels)
        {
            Contract.Requires(outerNodeModels != null);

            var outerNodes = new GOMS.OuterNode[outerNodeModels.Count];

            for (int i = 0; i < outerNodes.Length; i++)
            {
                var current = outerNodeModels[i];
                outerNodes[i] = new GOMS.OuterNode(
                    isEndNode: current.IsEndNode,
                    gomsTokens: ParseGomsTokens(current.GOMSChars));
            }

            return outerNodes;
        }

        static public GOMS.Token[] ParseGomsTokens(string source)
        {
            if (string.IsNullOrEmpty(source))
                return Array.Empty<GOMS.Token>();

            var tokens = new List<GOMS.Token>(source.Length);
            foreach (var item in source)
            {
                switch (item)
                {
                    case 'M':
                    case 'm':
                        tokens.Add(GOMS.Token.menthal);
                        break;
                    case 'H':
                    case 'h':
                        tokens.Add(GOMS.Token.handMoving);
                        break;
                    case 'K':
                    case 'k':
                        tokens.Add(GOMS.Token.keyboard);
                        break;
                    case 'P':
                    case 'p':
                        tokens.Add(GOMS.Token.positioning);
                        break;
                    default:
                        break;
                }
            }
            return tokens.ToArray();
        }

        static public GOMS.Parameters ConvertGOMSParameters(SimulationParmetersModel model)
        {
            Contract.Requires(model != null);

            return new GOMS.Parameters(
                keyboard: model.Keyboard,
                handMoving: model.HandMoving,
                menthal: model.Menthal,
                positioning: model.Positioning);
        }

        static public Dictionary<string, int> BuildNameIndexMapper(Collection<OuterNodeModel> outerNodeModels)
        {
            Contract.Requires(outerNodeModels != null);

            var dictionary = new Dictionary<string, int>();
            for (int i = 0; i < outerNodeModels.Count; i++)
            {
                var current = outerNodeModels[i];
                dictionary[current.Name] = i;
            }
            return dictionary;
        }

        static public (int, int, double)[] ConvertNodeMapping(Dictionary<string, int> nameIndexPairs, Collection<NodeMappingModel> mappingModels)
        {
            Contract.Requires(mappingModels != null && nameIndexPairs != null);

            var nodeMapping = new (int, int, double)[mappingModels.Count];

            for (int i = 0; i < nodeMapping.Length; i++)
            {
                var current = mappingModels[i];
                nodeMapping[i] = (nameIndexPairs[current.FirstNode], nameIndexPairs[current.SecondNode], current.Weight);
            }

            return nodeMapping;
        }
    }
}
