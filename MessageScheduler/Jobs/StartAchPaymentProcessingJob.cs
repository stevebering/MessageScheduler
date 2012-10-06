using System;
using MessageScheduler.Commands;
using NServiceBus;
using Quartz;

namespace MessageScheduler.Jobs
{
    public class StartAchPaymentProcessingJob : IJob
    {
        public IBus Bus { get; set; }

        public void Execute(IJobExecutionContext context) {
            var msg = new StartAchPaymentProcessing {
                CutoffDate = DateTime.Today.AddDays(1),
            };

            Bus.Send(msg);
        }
    }
}