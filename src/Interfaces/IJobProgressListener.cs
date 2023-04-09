using Quartz.Plugin.JobProgress.Data;

namespace Quartz.Plugin.JobProgress.Interfaces;

public interface IJobProgressListener : IJobListener
{
    Task JobProgressWasChanged(IJobExecutionContext context, ProgressData progress, CancellationToken cancellationToken = default);
}
