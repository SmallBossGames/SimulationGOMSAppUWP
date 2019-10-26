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
        private readonly WokstationParameters parameters;
        private readonly Random random;
        private readonly double errorProbability;
        private readonly ISimulationProcess[] simulationProcesses;

        private double executionStartTime;

        public ErrorFixProcess(
            WokstationParameters parameters,
            Random random,
            double errorProbability,
            ISimulationProcess[] simulationProcesses)
        {
            this.parameters = parameters;
            this.random = random;
            this.errorProbability = errorProbability;
            this.simulationProcesses = simulationProcesses;

            NextEventTime = random.NextExponential(parameters.ExpectedValue);
            ExecutionState = ExecutionState.Stopped;
            ExecutionTime = 0.0;
        }

        public ExecutionState ExecutionState { get; private set; }

        public double NextEventTime { get; private set; }

        public double ExecutionTime { get; private set; }

        public double Losses => ExecutionTime * parameters.LossRatio;

        public void RestartExecution(double time)
        {
            if (ExecutionState != ExecutionState.Running)
                return;

            NextEventTime = time + SimulationGOMS.Simulate(parameters.RootNode, errorProbability);
            ExecutionState = ExecutionState.Running;
            Console.WriteLine("Error");
            RestartAllDependeties();
        }

        public void StartExecution()
        {
            executionStartTime = NextEventTime;

            NextEventTime += SimulationGOMS.Simulate(parameters.RootNode, errorProbability);
            ExecutionState = ExecutionState.Running;
            RestartAllDependeties();
        }

        public void StopExecution()
        {
            ExecutionTime += NextEventTime - executionStartTime;
            NextEventTime += random.NextExponential(parameters.ExpectedValue);
            ExecutionState = ExecutionState.Stopped;
        }

        private void RestartAllDependeties()
        {
            foreach (var item in simulationProcesses)
            {
                if (item != this)
                    item.RestartExecution(NextEventTime);
            }
        }
    }
}
