using Quartz.Plugin.JobProgress.Data;

namespace Quartz.Plugin.JobProgress;

public interface IJobProgressListener : IJobListener
{
    Task JobProgressWasChanged(IJobExecutionContext context, ProgressData progress, CancellationToken cancellationToken = default);
}
