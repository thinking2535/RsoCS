using System;

namespace rso
{
    namespace core
    {
        public struct TimePoint
        {
            public long Ticks;

            public TimePoint(long Ticks_)
            {
                Ticks = Ticks_;
            }
            public TimePoint(DateTime DateTime_)
            {
                Ticks = DateTime_.ToTimePointTicks();
            }
            public DateTime ToDateTime()
            {
                return (DateTimeExtension.BaseDateTime() + new TimeSpan(Ticks)).ToLocalTime();
            }
            public static TimePoint Now
            {
                get
                {
                    return new TimePoint(DateTime.Now);
                }
            }
            public override string ToString()
            {
                return ToDateTime().ToString();
            }
            public override bool Equals(object Obj_)
            {
                var p = (TimePoint)Obj_;

                return (this == p);
            }
            public override int GetHashCode()
            {
                return (int)Ticks;
            }
            public static bool operator ==(TimePoint lhs_, TimePoint rhs_)
            {
                return (lhs_.Ticks == rhs_.Ticks);
            }
            public static bool operator !=(TimePoint lhs_, TimePoint rhs_)
            {
                return (lhs_.Ticks != rhs_.Ticks);
            }
            public static bool operator <(TimePoint lhs_, TimePoint rhs_)
            {
                return (lhs_.Ticks < rhs_.Ticks);
            }
            public static bool operator >(TimePoint lhs_, TimePoint rhs_)
            {
                return (lhs_.Ticks > rhs_.Ticks);
            }
            public static bool operator <=(TimePoint lhs_, TimePoint rhs_)
            {
                return (lhs_.Ticks <= rhs_.Ticks);
            }
            public static bool operator >=(TimePoint lhs_, TimePoint rhs_)
            {
                return (lhs_.Ticks >= rhs_.Ticks);
            }

            public static TimePoint operator +(TimePoint lhs_, TimeSpan rhs_)
            {
                return new TimePoint(lhs_.Ticks + rhs_.Ticks);
            }
            public static TimePoint operator -(TimePoint lhs_, TimeSpan rhs_)
            {
                return new TimePoint(lhs_.Ticks - rhs_.Ticks);
            }
            public static TimeSpan operator -(TimePoint lhs_, TimePoint rhs_)
            {
                return TimeSpan.FromTicks(lhs_.Ticks - rhs_.Ticks);
            }
            public static TimePoint FromTicks(long Ticks_)
            {
                return new TimePoint(Ticks_);
            }
            public static TimePoint FromDateTime(DateTime DateTime_)
            {
                return new TimePoint(DateTime_);
            }
        }
    }
}