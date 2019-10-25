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
            Node managementNode, 
            Node fixNode,
            double mangementMath,
            double fixMath,
            double managementLossRatio,
            double fixLossRatio,
            double errorProbability,
            double timeInterval)
        {
            var random = new Random();
            var simProcesses = new ISimulationProcess[2];
            var management = new ManagementProcess(managementNode, random, errorProbability, mangementMath);
            var fix = new ErrorFixProcess(fixNode, random, errorProbability, fixMath, simProcesses);

            simProcesses[0] = management;
            simProcesses[1] = fix;

            var time = 0.0;
            while(time < timeInterval)
            {
                Array.Sort(simProcesses, (x, y) => x.NextEventTime.CompareTo(y.NextEventTime));

                var simulationProcess = simProcesses[0];
                
                time = simulationProcess.NextEventTime;

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

            var loss = management.ExecutionTime * managementLossRatio + fix.ExecutionTime * fixLossRatio;

            return loss;
        }
    }
}
