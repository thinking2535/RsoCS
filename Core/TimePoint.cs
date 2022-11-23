using System;

namespace rso
{
    namespace core
    {
        public struct TimePoint : IComparable<TimePoint>
        {
            public static DateTime baseDateTime
            {
                get => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }
            public static TimePoint now => new TimePoint(DateTime.UtcNow);

            public static TimePoint fromTicks(Int64 ticks)
            {
                return new TimePoint(ticks);
            }
            public static TimePoint fromDateTime(DateTime dateTime)
            {
                return new TimePoint(dateTime);
            }
            public static bool operator ==(TimePoint self, TimePoint other)
            {
                return (self.ticks == other.ticks);
            }
            public static bool operator !=(TimePoint self, TimePoint other)
            {
                return (self.ticks != other.ticks);
            }
            public static bool operator <(TimePoint self, TimePoint other)
            {
                return (self.ticks < other.ticks);
            }
            public static bool operator >(TimePoint self, TimePoint other)
            {
                return (self.ticks > other.ticks);
            }
            public static bool operator <=(TimePoint self, TimePoint other)
            {
                return (self.ticks <= other.ticks);
            }
            public static bool operator >=(TimePoint self, TimePoint other)
            {
                return (self.ticks >= other.ticks);
            }

            public static TimePoint operator +(TimePoint self, TimeSpan other)
            {
                return new TimePoint(self.ticks + other.Ticks);
            }
            public static TimePoint operator -(TimePoint self, TimeSpan other)
            {
                return new TimePoint(self.ticks - other.Ticks);
            }
            public static TimePoint operator +(TimePoint self, Duration other)
            {
                return new TimePoint(self.ticks + other.ticks);
            }
            public static TimePoint operator -(TimePoint self, Duration other)
            {
                return new TimePoint(self.ticks - other.ticks);
            }

            public static Duration operator -(TimePoint self, TimePoint other)
            {
                return new Duration(self.ticks - other.ticks);
            }

            public Int64 ticks;

            public TimePoint(Int64 ticks)
            {
                this.ticks = ticks;
            }
            public TimePoint(DateTime dateTime)
            {
                ticks = (dateTime.ToUniversalTime() - baseDateTime).Ticks;
            }
            public TimePoint(string timeString, DateTimeKind dateTimeKind = DateTimeKind.Utc) :
                this(DateTime.SpecifyKind(Convert.ToDateTime(timeString), dateTimeKind))
            {
            }
            public DateTime toDateTime()
            {
                return (baseDateTime + new TimeSpan(ticks)).ToLocalTime();
            }
            public Int32 CompareTo(TimePoint other)
            {
                if (ticks > other.ticks) return 1;
                else if (ticks < other.ticks) return -1;
                else return 0;
            }
            public override string ToString()
            {
                return toDateTime().ToString();
            }
            public override Int32 GetHashCode()
            {
                return (Int32)ticks;
            }
            public override bool Equals(object obj)
            {
                var p = (TimePoint)obj;
                return (this == p);
            }
        }
    }
}