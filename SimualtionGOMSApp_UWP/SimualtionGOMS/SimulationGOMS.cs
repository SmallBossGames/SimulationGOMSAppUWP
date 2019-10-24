﻿using System;
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
            return Simulate(BuildGraph(parameters, outerNodes, nodesMapping), errorProbability);
        }

        public static double Simulate(Node rootNode, double errorProbability)
        {
            var graphMovingStack = new Stack<Node>();
            var timeSum = 0.0;
            var random = new Random();

            graphMovingStack.Push(rootNode);

            while (true)
            {
                var currentNode = graphMovingStack.Peek();
                timeSum += currentNode.Time;

                var errorTest = random.NextDouble();
                if (errorTest < errorProbability)
                {
                    if(graphMovingStack.Count > 1)
                    {
                        _ = graphMovingStack.Pop();
                    }
                    continue;
                }

                if (currentNode.IsEndNode)
                    break;

                var nextNode = currentNode.GoNextRandomNode(random);

                graphMovingStack.Push(nextNode);
            }

            return timeSum;

            /*var currentNode = new Node(false, 0.0);
            currentNode.NextNodes.Add((1.0, rootNode));

            var timeSum = 0.0;
            var random = new Random();

            var graphMovingStack = new Stack<Node>();

            while (true)
            {
                timeSum += currentNode.Time;

                var errorTest = random.NextDouble();
                if (errorTest < errorProbability)
                {
                    if (graphMovingStack.Count!=0)
                    {
                        currentNode = graphMovingStack.Pop();
                    }
                    continue;
                }

                if (currentNode.IsEndNode)
                    break;

                var nextNode = currentNode.GoNextRandomNode(random);

                if (nextNode == null)
                    throw new NullReferenceException();

                nextNode.Parent = currentNode;
                currentNode = nextNode;
            }

            return timeSum;*/
        }

        public static Node BuildGraph(
            in Parameters parameters,
            OuterNode[] outerNodes,
            (int, int, double)[] nodesMapping)
        {
            if (outerNodes == null || nodesMapping == null)
                throw new NullReferenceException();

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
