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

            DateTimeOffset startAt4Am = DateBuilder.TodayAt(4, 0, 0);

            ITrigger achPaymentsTrigger = TriggerBuilder.Create()
                .WithIdentity("StartAchPaymentsTriggerDailyAt4AM", "DailyTriggers")
                .StartAt(startAt4Am)
                .WithSimpleSchedule(x => {
                    x.RepeatForever();
                    x.WithInterval(TimeSpan.FromDays(1));
                })
                .Build();

            scheduler.Clear();
            scheduler.ScheduleJob(startAchPayments, achPaymentsTrigger);
        }
    }
}