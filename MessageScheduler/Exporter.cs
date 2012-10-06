using System;
using System.ComponentModel;

namespace MessageScheduler
{
    public class Exporter
    {
        [DisplayName("quartz.scheduler.exporter.type")]
        public Type Type { get; set; }

        [DisplayName("quartz.scheduler.exporter.port")]
        public int Port { get; set; }

        [DisplayName("quartz.scheduler.exporter.bindName")]

        public string BindName { get; set; }
        [DisplayName("quartz.scheduler.exporter.channelType")]
        public string ChannelType { get; set; }
    }

    public class JobStore
    {
        [DisplayName("quartz.jobStore.type")]
        public Type Type { get; set; }

        [DisplayName("quartz.jobStore.driverDelegateType")]
        public Type DriverDelegateType { get; set; }

        public DataSource DataSource { get; set; }
    }

    public class DataSource
    {
        [DisplayName("quartz.dataSource.quartzDataSource.connectionString")]
        public string ConnectionString { get; set; }

        [DisplayName("quartz.dataSource.quartzDataSource.provider")]
        public string Provider { get; set; }
    }
}