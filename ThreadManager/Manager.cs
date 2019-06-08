using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AddingThread;
using Common.Interfaces;
using GeneratorThread;
using ThreadManager.Interfaces;
using VisualisingThread;

namespace ThreadManager
{
    public class Manager : IManager
    {
        private readonly IEnumerable<IWork> workThreads;
        private readonly List<CancellationTokenSource> cancellationTokenSources;
        private List<Task> tasks;

        public GeneratorWorker GeneratorWorker => workThreads.First(w => w.Name == nameof(GeneratorWorker)) as GeneratorWorker;
        public AddingWorker AddingWorker => workThreads.First(w => w.Name == nameof(AddingWorker)) as AddingWorker;
        public VisualisingWorker VisualisingWorker => workThreads.First(w => w.Name == nameof(VisualisingWorker)) as VisualisingWorker;

        public Manager()
        {
            cancellationTokenSources = new List<CancellationTokenSource>();
            tasks = new List<Task>();
            workThreads = new List<IWork>
            {
                new GeneratorWorker(),
                new AddingWorker(),
                new VisualisingWorker()
            };
        }

        public void StartWorks()
        {
            foreach (var thread in workThreads)
            {
                var cancellationTokenSource = new CancellationTokenSource();
                tasks.Add(Task.Run(() => thread.Work(), cancellationTokenSource.Token));
                cancellationTokenSources.Add(cancellationTokenSource);
            }
        }

        public void StopWorks()
        {
            foreach (var cancellationTokenSource in cancellationTokenSources)
            {
                cancellationTokenSource.Cancel();
            }
            foreach (var task in tasks)
            {
                task.Dispose();
            }
            tasks = new List<Task>();
        }
    }
}
