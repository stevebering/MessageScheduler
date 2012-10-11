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

            var startAt4Am = DateBuilder.NewDateInTimeZone(TimeZoneInfo.Local)
                .AtHourOfDay(4)
                .AtMinute(0)
                .AtSecond(0)
                .Build();

            ITrigger achPaymentsTrigger = TriggerBuilder.Create()
                .WithIdentity("StartAchPaymentsTriggerDaily", "DailyTriggers")
                .StartAt(startAt4Am)
                .WithSimpleSchedule(x => {
                    x.RepeatForever();
                    x.WithIntervalInHours(24);
                })
                .Build();

            scheduler.Clear();
            scheduler.ScheduleJob(startAchPayments, achPaymentsTrigger);
        }
    }
}