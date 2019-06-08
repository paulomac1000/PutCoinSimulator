using AddingThread;
using GeneratorThread;
using VisualisingThread;

namespace ThreadManager.Interfaces
{
    public interface IManager
    {
        GeneratorWorker GeneratorWorker { get; }
        AddingWorker AddingWorker { get; }
        VisualisingWorker VisualisingWorker { get; }
        void StartWorks();
        void StopWorks();
    }
}
