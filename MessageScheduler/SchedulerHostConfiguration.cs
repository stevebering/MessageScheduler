using System.ComponentModel;

namespace MessageScheduler
{
    public class SchedulerHostConfiguration
    {
        [DisplayName("quartz.scheduler.instanceName")]
        public string InstanceName { get; set; }

        public Threadpool Threadpool { get; set; }

        public Exporter Exporter { get; set; }

        public JobStore JobStore { get; set; }
    }
}