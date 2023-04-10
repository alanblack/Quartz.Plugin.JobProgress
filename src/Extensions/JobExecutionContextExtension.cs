using Quartz.Plugin.JobProgress;

namespace Quartz.Plugin.JobProgress;

public static class JobExecutionContextExtension
{
    public static void UpdateProgress(this IJobExecutionContext context, int progress, object? data=null)
    {
        var plugin = context.Scheduler.ListenerManager
        .GetJobListeners()
        .FirstOrDefault(x => x is IJobProgressPlugin) as IJobProgressPlugin;

        plugin?.FireProgressUpdate(context, progress, data);
    }
}