using SimualtionGOMSApp_UWP.GOMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimualtionGOMSApp_UWP.SimulationWorkstation
{
    static class Workstation
    {
        public static double Simulate(
            in WokstationParameters managementParams,
            in WokstationParameters fixParams,
            double errorProbability,
            double timeInterval)
        {
            var random = new Random();
            var simProcesses = new ISimulationProcess[2];
            var management = new ManagementProcess(managementParams, random, errorProbability);
            var fix = new ErrorFixProcess(fixParams, random, errorProbability, simProcesses);

            simProcesses[0] = management;
            simProcesses[1] = fix;

            var time = 0.0;
            while(true)
            {
                Array.Sort(simProcesses, (x, y) => x.NextEventTime.CompareTo(y.NextEventTime));

                var simulationProcess = simProcesses[0];
                
                time = simulationProcess.NextEventTime;

                if (time >= timeInterval)
                    break;

                switch (simulationProcess.ExecutionState)
                {
                    case ExecutionState.Stopped:
                        simulationProcess.StartExecution();
                        break;
                    case ExecutionState.Running:
                        simulationProcess.StopExecution();
                        break;
                    default:
                        break;
                }
            }

            var loss = management.Losses + fix.Losses;

            return loss;
        }
    }
}
