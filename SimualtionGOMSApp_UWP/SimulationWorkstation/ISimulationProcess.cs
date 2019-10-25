namespace SimualtionGOMSApp_UWP.SimulationWorkstation
{
    public interface ISimulationProcess
    {
        ExecutionState ExecutionState { get; }
        double NextEventTime { get; }
        double ExecutionTime { get; }

        void RestartExecution(double time);
        void StartExecution();
        void StopExecution();
    }
}