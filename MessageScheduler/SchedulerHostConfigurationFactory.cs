using System.Collections.Specialized;
using Quartz.Simpl;

namespace MessageScheduler
{
    public class SchedulerHostConfigurationFactory
    {
        public static SchedulerHostConfiguration Create() {
            var cfg = new SchedulerHostConfiguration {
                InstanceName = "RemoteServerSchedulerClient",
                Threadpool = new Threadpool {
                    Type = typeof(SimpleThreadPool),
                    ThreadCount = 5,
                    ThreadPriority = "Normal",
                },
                Exporter = new Exporter {
                    Type = typeof(RemotingSchedulerExporter),
                    Port = 555,
                    BindName = "QuartzScheduler",
                    ChannelType = "tcp",
                },
                JobStore = new JobStore {
                    Type = typeof(Quartz.Impl.AdoJobStore.JobStoreTX),
                    DriverDelegateType = typeof(Quartz.Impl.AdoJobStore.StdAdoDelegate),
                    DataSource = new DataSource {
                        ConnectionString = @"Data Source=D:\MessageScheduler\MessageScheduler\schedules.sdf;Persist Security Info=false;",
                        Provider = "SqlServerCE-400",
                    }
                }
            };

            return cfg;
        }

        public static NameValueCollection Serialize(SchedulerHostConfiguration cfg) {
            var properties = new NameValueCollection();

            properties.AddConfigurationElement(cfg.InstanceName);

            // set threadpool info 
            properties.AddConfigurationElement(cfg.Threadpool.Type);
            properties.AddConfigurationElement(cfg.Threadpool.ThreadCount);
            properties.AddConfigurationElement(cfg.Threadpool.ThreadPriority);

            // set remoting exporter
            properties.AddConfigurationElement(cfg.Exporter.Type);
            properties.AddConfigurationElement(cfg.Exporter.Port);
            properties.AddConfigurationElement(cfg.Exporter.BindName);
            properties.AddConfigurationElement(cfg.Exporter.ChannelType);

            // set jobstore properties
            properties.AddConfigurationElement(cfg.JobStore.Type);
            properties.AddConfigurationElement(cfg.JobStore.DriverDelegateType);
            properties.Add("quartz.jobStore.dataSource", "quartzDataSource");
            properties.AddConfigurationElement(cfg.JobStore.DataSource.ConnectionString);
            properties.AddConfigurationElement(cfg.JobStore.DataSource.Provider);

            return properties;
        }
    }
}