using System;
using System.ComponentModel;

namespace MessageScheduler
{
    public class Threadpool
    {
        [DisplayName("quartz.threadPool.type")]
        public Type Type { get; set; }

        [DisplayName("quartz.threadPool.threadCount")]
        public int ThreadCount { get; set; }

        [DisplayName("quartz.threadPool.threadPriority")]
        public string ThreadPriority { get; set; }
    }
}