using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimualtionGOMSApp_UWP.GOMS;


namespace SimualtionGOMSApp_UWP.SimulationWorkstation
{
    public sealed class ManagementProcess : ISimulationProcess
    {
        private readonly WokstationParameters parameters;
        private readonly Random random;
        private readonly double errorProbability;

        private double executionStartTime;

        public ManagementProcess(WokstationParameters parameters, Random random, double errorProbability)
        {
            this.parameters = parameters;
            this.random = random;
            this.errorProbability = errorProbability;

            NextEventTime = random.NextExponential(parameters.ExpectedValue);
            ExecutionState = ExecutionState.Stopped;
            ExecutionTime = 0.0;
        }

        public double NextEventTime { get; private set; }

        public double ExecutionTime { get; private set; }

        public ExecutionState ExecutionState { get; private set; }

        public double Losses => ExecutionTime * parameters.LossRatio;

        public void StartExecution()
        {
            executionStartTime = NextEventTime;

            NextEventTime += SimulationGOMS.Simulate(parameters.RootNode, errorProbability);
            ExecutionState = ExecutionState.Running;
        }

        public void RestartExecution(double time)
        {
            if (ExecutionState != ExecutionState.Running)
                return;

            NextEventTime = time + SimulationGOMS.Simulate(parameters.RootNode, errorProbability);
            ExecutionState = ExecutionState.Running;
        }

        public void StopExecution()
        {
            ExecutionTime += NextEventTime - executionStartTime;
            NextEventTime += random.NextExponential(parameters.ExpectedValue);
            ExecutionState = ExecutionState.Stopped;
        }
    }
}
