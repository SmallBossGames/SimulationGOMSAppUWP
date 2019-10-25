﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimualtionGOMSApp_UWP.GOMS;


namespace SimualtionGOMSApp_UWP.SimulationWorkstation
{
    public sealed class ManagementProcess : ISimulationProcess
    {
        private readonly Node rootNode;
        private readonly Random random;
        private readonly double errorProbability;
        private readonly double math;

        private double executionStartTime;

        public ManagementProcess(Node rootNode, Random random, double errorProbability, double math)
        {
            this.rootNode = rootNode;
            this.random = random;
            this.errorProbability = errorProbability;
            this.math = math;

            NextEventTime = random.NextExponential(math);
            ExecutionState = ExecutionState.Stopped;
            ExecutionTime = 0.0;
        }

        public double NextEventTime { get; private set; }
        public double ExecutionTime { get; private set; }
        public ExecutionState ExecutionState { get; private set; }

        public void StartExecution()
        {
            executionStartTime = NextEventTime;

            NextEventTime += SimulationGOMS.Simulate(rootNode, errorProbability);
            ExecutionState = ExecutionState.Running;
        }

        public void RestartExecution(double time)
        {
            NextEventTime = time + SimulationGOMS.Simulate(rootNode, errorProbability);
            ExecutionState = ExecutionState.Running;
        }

        public void StopExecution()
        {
            ExecutionTime += NextEventTime - executionStartTime;
            NextEventTime += random.NextExponential(math);
            ExecutionState = ExecutionState.Stopped;
        }
    }

    public enum ExecutionState
    {
        Stopped, Running,
    }
}
