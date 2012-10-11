using System;
using MessageScheduler.Jobs;
using Quartz;

namespace MessageScheduler.Schedulers
{
    public class AchPaymentProcessingScheduler
        : JobScheduler
    {
        public override void Schedule(IScheduler scheduler) {
            // construct jobs 
            var map = new JobDataMap();
            map.Put("msg", "Some message!");

            IJobDetail startAchPayments = JobBuilder.Create<StartAchPaymentProcessingJob>()
                .WithIdentity("StartACHPayments", "Processing")
                .WithDescription("Starts processing of ACH payments")
                .StoreDurably(true)
                .UsingJobData(map)
                .Build();

            var schedule = SchedulingExtensions.DailyAt(4);

            ITrigger achPaymentsTrigger = TriggerBuilder.Create()
                .WithIdentity("StartAchPaymentsTriggerDaily", "DailyTriggers")
                .WithCronSchedule(schedule, c => c.InTimeZone(TimeZoneInfo.Local))
                .Build();

            scheduler.Clear();
            scheduler.ScheduleJob(startAchPayments, achPaymentsTrigger);
        }
    }
}