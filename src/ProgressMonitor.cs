using Quartz.Plugin.JobProgress.Data;
using Quartz.Plugin.JobProgress.Interfaces;
using Quartz.Plugin.JobProgress.Listener;

namespace Quartz.Plugin.JobProgress;

sealed class ProgressMonitor : IObserver<ProgressData>
{
    private readonly IJobExecutionContext _context;
    private readonly IScheduler _scheduler;

    public ProgressMonitor(IJobExecutionContext context)
    {
        _context = context;
        _scheduler = _context.Scheduler;
    }
    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(ProgressData value)
    {
        Console.WriteLine("ProgressMonitor detected an update");

        var jobListeners = _scheduler.ListenerManager.GetJobListeners().Where(x => x is IJobProgressListener).OfType<IJobProgressListener>().ToList();

        

        jobListeners.ForEach(x => x.JobProgressWasChanged(_context, value, default));
    }
}
