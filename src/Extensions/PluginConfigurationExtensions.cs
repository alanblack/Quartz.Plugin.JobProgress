using Quartz.Util;

namespace Quartz.Plugin.JobProgress;

public static class PluginConfigurationExtensions
{
        public static T UseJobProgressReporting<T>(
            this T configurer,
            Action<JobProgressReportOptions>? configure = null) where T : IPropertyConfigurationRoot
        {
            configurer.SetProperty("quartz.plugin.jobProgressReport.type", typeof(JobProgressPlugin).AssemblyQualifiedNameWithoutVersion());
            configure?.Invoke(new JobProgressReportOptions(configurer));
            return configurer;
        }
}

    public class JobProgressReportOptions : PropertiesSetter
    {
        public JobProgressReportOptions(IPropertySetter parent) : base(parent, "quartz.plugin.jobProgressReport")
        {
        }

    public int MyConfigProperty
        {
            //set => SetProperty("MyConfigProperty", value.ToString());
            get;set;
        }
    }
