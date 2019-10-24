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
        //public bool IsChecked => Parent != null;

        //public Node Parent { get; set; }

        public Node GoNextRandomNode(Random random)
        {
            if (random == null)
                throw new NullReferenceException();

            var nodesSum = NextNodes.Sum(x => x.Item1);
            var randomValue = random.NextDouble();
            var sum = 0.0;

            foreach (var (value, node) in NextNodes)
            {
                sum += value / nodesSum;
                if (sum >= randomValue)
                    return node;
            }

            return null;
        }
    }
}
