using SimualtionGOMSApp_UWP.GOMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimualtionGOMSApp_UWP.SimulationWorkstation
{
    class ErrorFixProcess : ISimulationProcess
    {
        private readonly Node rootNode;
        private readonly Random random;
        private readonly double errorProbability;
        private readonly double math;
        private ISimulationProcess[] simulationProcesses;

        private double executionStartTime;

        public ErrorFixProcess(
            Node rootNode,
            Random random,
            double errorProbability,
            double math,
            ISimulationProcess[] simulationProcesses)
        {
            this.rootNode = rootNode;
            this.random = random;
            this.errorProbability = errorProbability;
            this.math = math;
            this.simulationProcesses = simulationProcesses;

            NextEventTime = random.NextExponential(math);
            ExecutionState = ExecutionState.Stopped;
            ExecutionTime = 0.0;
        }

        public ExecutionState ExecutionState { get; private set; }

        public double NextEventTime { get; private set; }

        public double ExecutionTime { get; private set; }

        public void RestartExecution(double time)
        {
            NextEventTime = time + SimulationGOMS.Simulate(rootNode, errorProbability);
            ExecutionState = ExecutionState.Running;
            RestartAllDependeties();
        }

        public void StartExecution()
        {
            executionStartTime = NextEventTime;

            NextEventTime += SimulationGOMS.Simulate(rootNode, errorProbability);
            ExecutionState = ExecutionState.Running;
            RestartAllDependeties();
        }

        public void StopExecution()
        {
            ExecutionTime += NextEventTime - executionStartTime;
            NextEventTime += random.NextExponential(math);
            ExecutionState = ExecutionState.Stopped;
        }

        private void RestartAllDependeties()
        {
            foreach (var item in simulationProcesses)
            {
                item.RestartExecution(NextEventTime);
            }
        }
    }
}
