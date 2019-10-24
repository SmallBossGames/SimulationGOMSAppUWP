using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimualtionGOMSApp_UWP.SimualtionGOMS;

namespace SimualtionGOMSApp_UWP.SimulationWorkstation
{
    public sealed class ManagementProcess
    {
        private readonly Parameters parameters;
        private readonly OuterNode[] outerNodes;
        private readonly (int, int, double) outerNodesMapping;
        private readonly int rootIndex;

        public double NextEventTime { get; private set; }
        public ExecutionState ExecutionState { get; set; }
        
        public void StartExecution()
        {
            
        }

        public void RestartExecution(double time)
        {
            
        }

        public void StopExecution()
        {

        }

        /*private double SimulateGOMS()
        {
            SimulationGOMS.Simulate(parameters, outerNodes, outerNodesMapping, )
        }*/
    }

    public enum ExecutionState
    {
        Stopped, Running,
    }
}
