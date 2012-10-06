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

            properties.AddConfigurationElement(x => x.InstanceName, cfg);

            // set threadpool info 
            properties.AddConfigurationElement(x => x.Threadpool.Type, cfg);
            properties.AddConfigurationElement(x => x.Threadpool.ThreadCount, cfg);
            properties.AddConfigurationElement(x => x.Threadpool.ThreadPriority, cfg);

            // set remoting exporter
            properties.AddConfigurationElement(x => x.Exporter.Type, cfg);
            properties.AddConfigurationElement(x => x.Exporter.Port, cfg);
            properties.AddConfigurationElement(x => x.Exporter.BindName, cfg);
            properties.AddConfigurationElement(x => x.Exporter.ChannelType, cfg);

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