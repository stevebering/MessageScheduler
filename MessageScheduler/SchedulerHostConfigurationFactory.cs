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
                        ConnectionString = @"Data Source=D:\MessageScheduler\MessageScheduler\quartz2-sqlce4.sdf;Persist Security Info=false;",
                        Provider = "SqlServerCe-400",
                    }
                }
            };

            return cfg;
        }

        public static NameValueCollection Serialize(SchedulerHostConfiguration cfg) {
            var properties = new NameValueCollection();

            properties.AddConfigurationElement(x => x.InstanceName, cfg);

            // set threadpool info 
            properties.Add("quartz.threadPool.type", "Quartz.Simpl.SimpleThreadPool, Quartz");
            //properties.AddConfigurationElement(x => x.Type, cfg.Threadpool);
            properties.AddConfigurationElement(x => x.ThreadCount, cfg.Threadpool);
            properties.AddConfigurationElement(x => x.ThreadPriority, cfg.Threadpool);

            // set remoting exporter
            properties.Add("quartz.scheduler.exporter.type", "Quartz.Simpl.RemotingSchedulerExporter, Quartz");
            //properties.AddConfigurationElement(x => x.Type, cfg.Exporter);
            properties.AddConfigurationElement(x => x.Port, cfg.Exporter);
            properties.AddConfigurationElement(x => x.BindName, cfg.Exporter);
            properties.AddConfigurationElement(x => x.ChannelType, cfg.Exporter);

            // set jobstore properties
            properties.Add("quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz");
            properties.Add("quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.StdAdoDelegate, Quartz");
            //properties.AddConfigurationElement(x => x.Type, cfg.JobStore);
            //properties.AddConfigurationElement(x => x.DriverDelegateType, cfg.JobStore);
            properties.Add("quartz.jobStore.dataSource", "quartzDataSource");
            properties.AddConfigurationElement(x => x.ConnectionString, cfg.JobStore.DataSource);
            properties.AddConfigurationElement(x => x.Provider, cfg.JobStore.DataSource);

            return properties;
        }
    }
}