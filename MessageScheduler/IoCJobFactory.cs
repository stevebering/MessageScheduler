using System;
using Autofac;
using Quartz;
using Quartz.Spi;

namespace MessageScheduler
{
    public class IoCJobFactory : IJobFactory
    {
        private readonly IContainer _locator;

        public IoCJobFactory(IContainer locator) {
            this._locator = locator;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler) {
            try {
                IJobDetail jobDetail = bundle.JobDetail;
                Type jobType = jobDetail.JobType;

                return (IJob)_locator.Resolve(jobType);
            }
            catch (Exception e) {
                var se = new SchedulerException("Problem instantiating class of type", e);
                throw se;
            }
        }
    }
}