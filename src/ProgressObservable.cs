using Quartz.Plugin.JobProgress.Data;


namespace Quartz.Plugin.JobProgress;

public class ProgressObservable : IObservable<ProgressData>
{
    public IDisposable Subscribe(IObserver<ProgressData> observer)
    {
        return new Unsubscriber(observer);
    }
    
    private class Unsubscriber : IDisposable
    {
        private readonly IObserver<ProgressData> _observer;

        public Unsubscriber(IObserver<ProgressData> observer)
        {
            _observer = observer;
        }
        public void Dispose()
        {
        }
    }
}


