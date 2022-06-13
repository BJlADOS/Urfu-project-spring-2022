using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.Core.Helpers
{
    public static class DateTimeHelper
    {
        private static readonly DateTime _startUnixTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static long GetUnixTime(DateTime dateTime)
        {
            var localTime = dateTime;
            var unixTime = (localTime - _startUnixTime);
            return unixTime.Ticks / 10000;
        }

        public static DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }

        public static DateTime GetDateTimeFromUnixTime(long unixTime)
        {
            var dateTime = _startUnixTime.AddTicks(unixTime * 10000);
            return dateTime;
        }

        public static int GetSecondsFromMinutes(int minutes)
        {
            var time = new TimeSpan(0, minutes, 0);
            return (int)time.TotalSeconds;
        }
    }
}
