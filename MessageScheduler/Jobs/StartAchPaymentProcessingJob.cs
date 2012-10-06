using System;
using NServiceBus;
using Quartz;

namespace MessageScheduler.Jobs
{
    public class StartAchPaymentProcessingJob : IJob
    {
        public IBus Bus { get; set; }

        public void Execute(IJobExecutionContext context) {
            var m = new Meracord.ACH.PaymentBatchManager.InternalMessages.StartACHPaymentBatch() {
                ACHDebitCutoffDate = DateTime.Today.AddDays(1),
                DebitsPerBatchStartedCount = 1000,
                PaymentBatchGUID = Guid.NewGuid(),
                RequestedBy = "scheduling_service",
                RequestedDate = DateTime.UtcNow,
                SessionID = 1,
            };

            Bus.Send(m);
        }
    }
}