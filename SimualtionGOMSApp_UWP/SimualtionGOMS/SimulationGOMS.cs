using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimualtionGOMSApp_UWP.SimualtionGOMS
{
    public static class SimulationGOMS
    {
        public static double Simulate(
            in Parameters parameters,
            OuterNode[] outerNodes,
            (int, int, double)[] nodesMapping,
            double errorProbability
            )
        {
            var currentNode = new Node(false, 0.0);
            currentNode.NextNodes.Add((1.0, Node.BuildGraph(parameters, outerNodes, nodesMapping)));

            var timeSum = 0.0;
            var random = new Random();

            while (true)
            {
                timeSum += currentNode.Time;

                var errorTest = random.NextDouble();
                if(errorTest < errorProbability)
                {
                    if (currentNode.Parent != null)
                    {
                        var parent = currentNode.Parent;
                        currentNode.Parent = null;
                        currentNode = parent;
                    }
                    continue;
                }

                if (currentNode.IsEndNode)
                    break;

                var nextNode = currentNode.GoNextRandomNode(random);

                if (nextNode == null)
                    throw new ArgumentException("Something wrong with graph");

                nextNode.Parent = currentNode;
                currentNode = nextNode;
            }

            return timeSum;
        }
    }
}
