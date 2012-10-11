using System;
using Autofac;
using MessageScheduler.Jobs;
using MessageScheduler.Schedulers;
using NServiceBus;
using Quartz;
using Quartz.Impl;
using Quartz.Job;

namespace MessageScheduler
{
    public static class Global
    {
        public static IContainer Container { get; set; }
    }

    public class EndpointConfiguration
        : IConfigureThisEndpoint
        , AsA_Server
        , IWantCustomInitialization
    {
        public void Init() {
            var builder = new ContainerBuilder();
            InitializeContainer(builder);
            var container = builder.Build();

            Configure.With()
                .AutofacBuilder(container)
                .Log4Net();

            Global.Container = container;
        }

        private void InitializeContainer(ContainerBuilder builder) {
            builder.RegisterType<NoOpJob>().AsSelf();
            builder.RegisterType<StartAchPaymentProcessingJob>().AsSelf();
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
            //var properties = Configure();

            //var schedulerFactory = new StdSchedulerFactory(properties);

            var schedulerFactory = new StdSchedulerFactory();

            // configure job storage
            var scheduler = schedulerFactory.GetScheduler();
            scheduler.JobFactory = new IoCJobFactory(Global.Container);
            scheduler.Clear();

            var achScheduler = new AchPaymentProcessingScheduler();
            achScheduler.Schedule(scheduler);

            //// define the job and ask it to run
            //var map = new JobDataMap();
            //map.Put("msg", "Some message!");

            //var job = JobBuilder.Create<NoOpJob>()
            //    .WithIdentity("localJob")
            //    .WithDescription("default")
            //    .UsingJobData(map)
            //    .Build();

            //var trigger = TriggerBuilder.Create()
            //    .WithIdentity("remotelyAddedTrigger")
            //    .WithCronSchedule("/5 * * ? * *")
            //    .StartNow()
            //    .Build();

            //// schedule the job
            ////scheduler.ScheduleJob(job, trigger);

            scheduler.Start();
        }
    }
}
