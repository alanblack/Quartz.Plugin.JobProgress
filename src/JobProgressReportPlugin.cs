using Quartz.Listener;
using Quartz.Spi;


namespace Quartz.Plugin.JobProgress;

public class JobProgressReportPlugin : JobListenerSupport, ISchedulerPlugin
{
    private IScheduler _scheduler = null!;
    private List<ProgressMonitor> _monitors = new List<ProgressMonitor>();

    public JobProgressReportPlugin()
    {
        Console.WriteLine("JobProgressReportPlugin ctor()");
    }

    public override string Name => "JobProgressReportPlugin";

    public Task Initialize(string pluginName, IScheduler scheduler, CancellationToken cancellationToken = default)
    {
        _scheduler = scheduler;
        _scheduler.ListenerManager.AddJobListener(this);

        return Task.CompletedTask;
    }

    public Task Shutdown(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task Start(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public override Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        var monitor = new ProgressMonitor(context);
        var progressReporter = (ProgressReporter)context.JobInstance;

        progressReporter.Subscribe(monitor);

        _monitors.Add(monitor);
        return Task.CompletedTask;
    }

}
