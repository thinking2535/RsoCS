using System;

namespace rso
{
    namespace core
    {
        public static class DateTimeExtension
        {
            public static DateTime BaseDateTime()
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }
            public static long ToTimePointTicks(this DateTime Time_)
            {
                return (Time_.ToUniversalTime() - BaseDateTime()).Ticks;
            }
            public static TimePoint ToTimePoint(this DateTime Time_)
            {
                return new TimePoint(Time_);
            }
        }
    }
}