# Quartz.Plugin.JobProgress

[Quartz.Plugin.JobProgress](https://github.com/alanblack/Quartz.Plugin.JobProgress) is a plugin for [Quartz.NET](https://www.quartz-scheduler.net/) that allows you to report progress in long-running jobs and monitor progress updates using listeners.

## Usage

1. Configure your services in your application's entry point (e.g., Program.cs):

```csharp
ConfigureServices(services =>
{
    services.AddQuartz(q =>
    {
        q.UseJobProgressReporting(options => {
            options.DefaultThreshold = 5;
        });
    });
});
```

2. Create a JobProgressListener by extending the JobProgressListenerSupport class:
```csharp
public class JobProgressListener : JobProgressListenerSupport
{
    public override string Name => "JobProgressListener";

    public override Task JobProgressWasChanged(IJobExecutionContext context, ProgressData progress, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Program.JobProgressWasChanged {context.JobDetail.Key.Name} percent={progress.Progress}");
        return Task.CompletedTask;
    }
}
```

3. Add the JobProgressListener to your scheduler:
```csharp
var jobProgressListener = new JobProgressListener();
scheduler.ListenerManager.AddJobListener(jobProgressListener, GroupMatcher<JobKey>.AnyGroup());
```

4. In your Job class, you can call the `UpdateProgress` method to report progress:
```csharp
public class WorkerJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync($"WorkerJob STARTED"); 

        var progress = 0;
        while(!context.CancellationToken.IsCancellationRequested)
        {
            context.UpdateProgress(progress++, "Your data object. Put in whatever you like");
            await Task.Delay(1000);
        }
        await Console.Out.WriteLineAsync($"WorkerJob STOPPED");    
    }
}
```

## License
This plugin is open-source and available under the MIT License. Feel free to use, modify, and distribute it according to the terms of the license. Contributions are also welcome!

