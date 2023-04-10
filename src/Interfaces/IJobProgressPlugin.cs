namespace Quartz.Plugin.JobProgress;

public interface IJobProgressPlugin
{
    public void FireProgressUpdate(IJobExecutionContext context, int progress, object? data=null);
}