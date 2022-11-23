using System;

namespace rso.core
{
    public class Duration : IComparable<Duration>
    {
        public static Duration zero = new Duration();

        public static Int64 microsecondsToTicks(Int64 microseconds)
        {
            return microseconds * 10;
        }
        public static Int64 millisecondsToTicks(Int64 milliseconds)
        {
            return milliseconds * 10000;
        }
        public static Int64 secondsToTicks(Int64 seconds)
        {
            return seconds * 10000000;
        }
        public static Int64 minutesToTicks(Int64 minutes)
        {
            return minutes * 600000000;
        }
        public static Int64 hoursToTicks(Int64 hours)
        {
            return hours * 36000000000;
        }
        public static Int64 daysToTicks(Int64 days)
        {
            return days * 864000000000;
        }

        public static Duration fromMicroseconds(Int64 microseconds)
        {
            return new Duration(microsecondsToTicks(microseconds));
        }
        public static Duration fromMilliseconds(Int64 milliseconds)
        {
            return new Duration(millisecondsToTicks(milliseconds));
        }
        public static Duration fromSeconds(Int64 seconds)
        {
            return new Duration(secondsToTicks(seconds));
        }
        public static Duration fromMinutes(Int64 minutes)
        {
            return new Duration(minutesToTicks(minutes));
        }
        public static Duration fromHours(Int64 hours)
        {
            return new Duration(hoursToTicks(hours));
        }
        public static Duration fromDays(Int64 days)
        {
            return new Duration(daysToTicks(days));
        }
        public static Duration fromTimeSpan(TimeSpan timeSpan)
        {
            return new Duration(timeSpan.Ticks);
        }

        public static bool operator ==(Duration self, Duration other)
        {
            return (self.ticks == other.ticks);
        }
        public static bool operator !=(Duration self, Duration other)
        {
            return (self.ticks != other.ticks);
        }
        public static bool operator <(Duration self, Duration other)
        {
            return (self.ticks < other.ticks);
        }
        public static bool operator >(Duration self, Duration other)
        {
            return (self.ticks > other.ticks);
        }
        public static bool operator <=(Duration self, Duration other)
        {
            return (self.ticks <= other.ticks);
        }
        public static bool operator >=(Duration self, Duration other)
        {
            return (self.ticks >= other.ticks);
        }
        public static Duration operator +(Duration self, Duration other)
        {
            return new Duration(self.ticks + other.ticks);
        }
        public static Duration operator -(Duration self, Duration other)
        {
            return new Duration(self.ticks - other.ticks);
        }

        public static bool operator ==(Duration self, TimeSpan other)
        {
            return (self.ticks == other.Ticks);
        }
        public static bool operator !=(Duration self, TimeSpan other)
        {
            return (self.ticks != other.Ticks);
        }
        public static bool operator <(Duration self, TimeSpan other)
        {
            return (self.ticks < other.Ticks);
        }
        public static bool operator >(Duration self, TimeSpan other)
        {
            return (self.ticks > other.Ticks);
        }
        public static bool operator <=(Duration self, TimeSpan other)
        {
            return (self.ticks <= other.Ticks);
        }
        public static bool operator >=(Duration self, TimeSpan other)
        {
            return (self.ticks >= other.Ticks);
        }
        public static Duration operator +(Duration self, TimeSpan other)
        {
            return new Duration(self.ticks + other.Ticks);
        }
        public static Duration operator -(Duration self, TimeSpan other)
        {
            return new Duration(self.ticks - other.Ticks);
        }

        public Int64 ticks;

        public Duration()
        {
            ticks = 0;
        }
        public Duration(Int64 ticks)
        {
            this.ticks = ticks;
        }
        public Duration(TimeSpan timeSpan)
        {
            ticks = timeSpan.Ticks;
        }
        public TimeSpan toTimeSpan()
        {
            return TimeSpan.FromTicks(ticks);
        }
        public override string ToString()
        {
            return toTimeSpan().ToString();
        }
        public override Int32 GetHashCode()
        {
            return (Int32)ticks;
        }
        public override bool Equals(object other)
        {
            var p = (Duration)other;
            return (this == p);
        }
        public Int32 CompareTo(Duration other)
        {
            if (ticks > other.ticks) return 1;
            else if (ticks < other.ticks) return -1;
            else return 0;
        }
        public virtual Int64 value => ticks;
        public Int64 totalMicroseconds => ticks / 10;
        public Int64 totalMilliseconds => ticks / 10000;
        public Int64 totalSeconds => ticks / 10000000;
        public Int64 totalCeilingSeconds => totalSeconds + (ticks % 10000000 > 0 ? 1 : 0);
        public Int64 seconds => totalSeconds % 60;
        public Int64 ceilingSeconds => seconds + (ticks % 10000000 > 0 ? 1 : 0);
        public Int64 totalMinutes => ticks / 600000000;
        public Int64 minutes => totalMinutes % 60;
        public Int64 totalHours => ticks / 36000000000;
        public Int64 hours => totalHours % 24;
        public Int64 totalDays => ticks / 864000000000;
    }

    public class Microseconds : Duration
    {
        Microseconds(Int64 ticks) :
            base(ticks)
        {
        }
        public Microseconds(TimeSpan timeSpan) :
            base(timeSpan)
        {
        }
        public override Int64 value => totalMicroseconds;
        public static Microseconds fromTicks(Int64 ticks)
        {
            return new Microseconds(ticks);
        }
        public static Microseconds fromValue(Int64 value)
        {
            return new Microseconds(microsecondsToTicks(value));
        }
    }
    public class Milliseconds : Duration
    {
        Milliseconds(Int64 ticks) :
            base(ticks)
        {
        }
        public Milliseconds(TimeSpan timeSpan) :
            base(timeSpan)
        {
        }
        public override Int64 value => totalMilliseconds;
        public static Milliseconds fromTicks(Int64 ticks)
        {
            return new Milliseconds(ticks);
        }
        public static Milliseconds fromValue(Int64 value)
        {
            return new Milliseconds(millisecondsToTicks(value));
        }
    }
    public class Seconds : Duration
    {
        Seconds(Int64 ticks) :
            base(ticks)
        {
        }
        public Seconds(TimeSpan timeSpan) :
            base(timeSpan)
        {
        }
        public override Int64 value => totalSeconds;
        public static Seconds fromTicks(Int64 ticks)
        {
            return new Seconds(ticks);
        }
        public static Seconds fromValue(Int64 value)
        {
            return new Seconds(secondsToTicks(value));
        }
    }
    public class Minutes : Duration
    {
        Minutes(Int64 ticks) :
            base(ticks)
        {
        }
        public Minutes(TimeSpan timeSpan) :
            base(timeSpan)
        {
        }
        public override Int64 value => totalMinutes;
        public static Minutes fromTicks(Int64 ticks)
        {
            return new Minutes(ticks);
        }
        public static Minutes fromValue(Int64 value)
        {
            return new Minutes(minutesToTicks(value));
        }
    }
    public class Hours : Duration
    {
        Hours(Int64 ticks) :
            base(ticks)
        {
        }
        public Hours(TimeSpan timeSpan) :
            base(timeSpan)
        {
        }
        public override Int64 value => totalHours;
        public static Hours fromTicks(Int64 ticks)
        {
            return new Hours(ticks);
        }
        public static Hours fromValue(Int64 value)
        {
            return new Hours(hoursToTicks(value));
        }
    }
    public class Days : Duration
    {
        Days(Int64 ticks) :
            base(ticks)
        {
        }
        public Days(TimeSpan timeSpan) :
            base(timeSpan)
        {
        }
        public override Int64 value => totalDays;
        public static Days fromTicks(Int64 ticks)
        {
            return new Days(ticks);
        }
        public static Days fromValue(Int64 value)
        {
            return new Days(daysToTicks(value));
        }
    }
}