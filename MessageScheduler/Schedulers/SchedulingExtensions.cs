namespace MessageScheduler.Schedulers
{
    public static class SchedulingExtensions
    {
        // cron format expressions: http://quartz-scheduler.org/api/2.0.0/org/quartz/CronTrigger.html

        // "0 15 10 ? * MON-FRI"	 	Fire at 10:15am every Monday, Tuesday, Wednesday, Thursday and Friday
        public static string DailyAt(int hour) {
            return string.Format("0 0 {0} ? * MON-FRI", hour);
        }

        public static string DailyAt(int hour, int minute, int second) {
            return string.Format("{2} {1} {0} ? * MON-FRI", hour, minute, second);
        }

        //"0 15 10 ? * 6L"	 	Fire at 10:15am on the last Friday of every month
        public static string LastFridayOfMonthAt(int hour) {
            return string.Format("0 0 {0} ? * 6L", hour);
        }

        //"0 15 10 3 * *"	 	Fire at 10:15am on the third day of every month
        public static string OnDayOfMonthAt(int dayOfMonth, int hour) {
            return string.Format("0 0 {0} {1} * *", hour, dayOfMonth);
        }
    }
}