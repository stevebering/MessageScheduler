using System.Collections.Specialized;
using Autofac;
using MessageScheduler.Schedulers;
using NServiceBus;
using Quartz.Impl;

namespace MessageScheduler
{
    public class EndpointConfiguration
        : IConfigureThisEndpoint
        , AsA_Server
        , IWantCustomInitialization
    {
        public void Init() {
            var container = new ContainerBuilder().Build();

            Configure.With()
                .AutofacBuilder(container);
        }
    }

    public class Scheduler
        : IWantToRunAtStartup
    {
        public void Run() {
            var host = new SchedulerHost();
            host.Start();
        }

        public void Stop() {

        }
    }

    public class SchedulerHost
    {
        public void Start() {
            var properties = Configure();

            var schedulerFactory = new StdSchedulerFactory(properties);

            // configure job storage
            var scheduler = schedulerFactory.GetScheduler();
            scheduler.Start();

            var achScheduler = new AchPaymentProcessingScheduler();
            achScheduler.Schedule(scheduler);

            scheduler.Start();
        }

        private NameValueCollection Configure() {
            var configuration = SchedulerHostConfigurationFactory.Create();
            return SchedulerHostConfigurationFactory.Serialize(configuration);
        }
    }
}
