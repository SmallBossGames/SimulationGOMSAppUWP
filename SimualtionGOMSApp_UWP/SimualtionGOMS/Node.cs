using System;
using System.Linq;
using System.Collections.Generic;

namespace SimualtionGOMSApp_UWP.SimualtionGOMS
{
    public class Node
    {
        public Node(bool isEndNode, double time)
        {
            IsEndNode = isEndNode;
            Time = time;
            NextNodes = new List<(double, Node)>();
        }

        public bool IsEndNode { get; }
        public double Time { get; }
        public List<(double, Node)> NextNodes { get; }
        public bool IsChecked => Parent != null;

        public Node Parent { get; set; }

        public Node GoNextRandomNode(Random random)
        {
            var uncheckedNodes = NextNodes.Where(x => !x.Item2.IsChecked);
            var uncheckedNodesSum = uncheckedNodes.Sum(x => x.Item1);
            var randomValue = random.NextDouble();
            var sum = 0.0;

            foreach (var (value, node) in NextNodes)
            {
                sum += value / uncheckedNodesSum;
                if (sum >= randomValue)
                    return node;
            }

            return null;
        }

        public static Node BuildGraph(
            in Parameters parameters, 
            OuterNode[] outerNodes, 
            (int, int, double)[] nodesMapping)
        {
            var tempNodes = new Node[outerNodes.Length];
            for (int i = 0; i < tempNodes.Length; i++)
            {
                var isEndNode = outerNodes[i].IsEndNode;
                var time = outerNodes[i].GetTime(parameters);
                tempNodes[i] = new Node(isEndNode, time);
            }

            foreach (var (from, to, prop) in nodesMapping)
            {
                var tuple = (prop, tempNodes[to]);
                tempNodes[from].NextNodes.Add(tuple);
            }

            return tempNodes[0];
        }
    }
}
