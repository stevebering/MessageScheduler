using Quartz;

namespace MessageScheduler.Schedulers
{
    public abstract class JobScheduler
    {
        public abstract void Schedule(IScheduler scheduler);
    }
}