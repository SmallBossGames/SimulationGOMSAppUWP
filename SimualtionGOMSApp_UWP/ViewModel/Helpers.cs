using SimualtionGOMSApp_UWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace SimualtionGOMSApp_UWP.ViewModel
{
    public static class Helpers
    {
        static public (GOMS.OuterNode[], (int, int, double)[]) ConvertOuterNodes(
            Collection<OuterNodeModel> outerNodeModels,
            Collection<NodeMappingModel> mappingModels)
        {
            Contract.Requires(outerNodeModels != null && mappingModels != null);

            var outerNodes = new GOMS.OuterNode[outerNodeModels.Count];
            var nodeMapping = new (int, int, double)[mappingModels.Count];

            var nameIndexMatch = new Dictionary<string, int>();

            for (int i = 0; i < outerNodes.Length; i++)
            {
                var current = outerNodeModels[i];
                outerNodes[i] = new GOMS.OuterNode(
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
    }
}
