namespace Quartz.Plugin.JobProgress.Data;

public class ProgressData
{
    public ProgressData() {}
    public ProgressData(int percent, object? data=null)
    {
        Percent = percent;
        Data = data;
    }

    private int _percent;
    private object? _data;

    public int Percent { get => _percent; set => _percent = value; }
    public object? Data { get => _data; set => _data = value; }
}
