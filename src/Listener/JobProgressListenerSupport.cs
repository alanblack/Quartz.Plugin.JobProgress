using Quartz.Plugin.JobProgress.Data;

namespace Quartz.Plugin.JobProgress.Listener;

public abstract class JobProgressListenerSupport : IJobProgressListener
{
    public abstract string Name { get; }

    public virtual Task JobProgressWasChanged(IJobExecutionContext context, ProgressData progress, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public virtual Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public virtual Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public virtual Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
