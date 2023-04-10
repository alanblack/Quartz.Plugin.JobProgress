namespace Quartz.Plugin.JobProgress.Data;

public class ProgressData
{
    public ProgressData() {}
    public ProgressData(int progress, object? data=null)
    {
        Progress = progress;
        Data = data;
    }

    private int progress;
    private object? _data;

    public int Progress { get => progress; set => progress = value; }
    public object? Data { get => _data; set => _data = value; }
}
