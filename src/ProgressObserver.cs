using Quartz.Plugin.JobProgress.Data;
using Quartz.Plugin.JobProgress.Listener;

namespace Quartz.Plugin.JobProgress;

sealed class ProgressObserver : IObserver<ProgressData>
{
    private readonly IJobExecutionContext _context;
    private readonly IScheduler _scheduler;

    public ProgressData CurrentProgressData { get; set; } = new ProgressData();

    public ProgressObserver(IJobExecutionContext context)
    {
        _context = context;
        _scheduler = _context.Scheduler;
    }
    public void OnNext(ProgressData value)
    {
        CurrentProgressData = value;
        var jobListeners = _scheduler.ListenerManager.GetJobListeners().Where(x => x is IJobProgressListener).OfType<IJobProgressListener>().ToList();
        jobListeners.ForEach(x => x.JobProgressWasChanged(_context, value, default));
    }
    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }
}
