using Quartz.Plugin.JobProgress.Data;


namespace Quartz.Plugin.JobProgress;

public class ProgressReporter : IObservable<ProgressData>
{

    private List<IObserver<ProgressData>> _observers = new List<IObserver<ProgressData>>();

    public IDisposable Subscribe(IObserver<ProgressData> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
        return new Unsubscriber(_observers, observer);
    }


    public void ReportProgress(int percent, object? data=null)
    {
        var progress = new ProgressData(percent, data);
        foreach(var observer in _observers)
        {
            observer.OnNext(progress);
        }
    }

    private class Unsubscriber : IDisposable
    {
        private readonly List<IObserver<ProgressData>> _observers;
        private readonly IObserver<ProgressData> _observer;

        public Unsubscriber(List<IObserver<ProgressData>> observers, IObserver<ProgressData> observer)
        {
            _observers = observers;
            _observer = observer;
        }
        public void Dispose()
        {
            if(_observer != null && _observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }
}


