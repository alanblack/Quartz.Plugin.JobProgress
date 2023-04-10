using System.Collections.Concurrent;
using Quartz.Listener;
using Quartz.Plugin.JobProgress.Data;
using Quartz.Spi;


namespace Quartz.Plugin.JobProgress;

public class JobProgressPlugin : JobListenerSupport, ISchedulerPlugin, IJobProgressPlugin
{
    public override string Name => "JobProgressPlugin";
    private IScheduler _scheduler = null!;
    private ConcurrentDictionary<string, ProgressObserver> _observers = new ConcurrentDictionary<string,ProgressObserver>();

    public JobProgressPlugin()
    {
    }

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
        var observer = new ProgressObserver(context);

        var observable = new ProgressObservable();
        observable.Subscribe(observer);
        _observers.TryAdd(context.FireInstanceId, observer);
        return Task.CompletedTask;
    }

    public override Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
    {
        if(_observers.TryRemove(context.FireInstanceId, out var observer))
        {
            Console.WriteLine($"Observer for job {context.JobDetail.Key.Name} removed");
            observer.OnCompleted();
        }
        return Task.CompletedTask;
    }

    public void FireProgressUpdate(IJobExecutionContext context, int progress, object? data=null)
    {
       if(_observers.TryGetValue(context.FireInstanceId, out var observer))
       {
        var progressData = new ProgressData(progress, data);
        observer.OnNext(progressData);
       }
    }
}
