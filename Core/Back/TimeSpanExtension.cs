using System;

namespace rso
{
    namespace core
    {
        public static class TimeSpanExtension
        {
            public static long TotalMillisecondsLong(this TimeSpan Duration_)
            {
                return Duration_.Ticks / 10000;
            }
            public static long TotalSecondsLong(this TimeSpan Duration_)
            {
                return Duration_.Ticks / 10000000;
            }
            public static long TotalMinutesLong(this TimeSpan Duration_)
            {
                return Duration_.Ticks / 600000000;
            }
            public static long TotalHoursLong(this TimeSpan Duration_)
            {
                return Duration_.Ticks / 36000000000;
            }
            public static long TotalMillisecondsLongCeil(this TimeSpan Duration_)
            {
                if (Duration_.Ticks % 10000 != 0)
                    return (Duration_.TotalMillisecondsLong() + (Duration_.Ticks >= 0 ? 1 : -1));

                return Duration_.TotalMillisecondsLong();
            }
            public static long TotalSecondsLongCeil(this TimeSpan Duration_)
            {
                if (Duration_.Ticks % 10000000 != 0)
                    return (Duration_.TotalSecondsLong() + (Duration_.Ticks >= 0 ? 1 : -1));

                return Duration_.TotalSecondsLong();
            }
            public static long TotalMinutesLongCeil(this TimeSpan Duration_)
            {
                if (Duration_.Ticks % 10000000 != 0)
                    return (Duration_.TotalMinutesLong() + (Duration_.Ticks >= 0 ? 1 : -1));
                else if (Duration_.Ticks % 60 != 0)
                    return (Duration_.TotalMinutesLong() + (Duration_.Ticks >= 0 ? 1 : -1));

                return Duration_.TotalMinutesLong();
            }
            public static long TotalHoursLongCeil(this TimeSpan Duration_)
            {
                if (Duration_.Ticks % 10000000 != 0)
                    return (Duration_.TotalHoursLong() + (Duration_.Ticks >= 0 ? 1 : -1));
                else if (Duration_.Ticks % 3600 != 0)
                    return (Duration_.TotalHoursLong() + (Duration_.Ticks >= 0 ? 1 : -1));

                return Duration_.TotalHoursLong();
            }
        }
    }
}