using System;
using rso.core;

namespace rso
{
    namespace gameutil
    {
        public static class Extension
		{
            public static bool IsInBoost(this STimeBoost timeBoost, TimePoint now)
            {
	            return (now < timeBoost.EndTime);
            }
			public static Duration GetRealDuration(Duration boostedDuration, double speed)
			{
				return new Duration((Int64)(boostedDuration.ticks / speed));
			}
			public static Duration GetBoostedDuration(this STimeBoost timeBoost, TimePoint now)
			{
                return new Duration((Int64)((timeBoost.EndTime - now).ticks * timeBoost.Speed));
			}
            public static Duration GetBoostedDuration(this STimeBoost timeBoost, TimePoint beginTime, TimePoint endTime)
            {
                if (endTime <= beginTime)
                    return Duration.zero;

                if (timeBoost.EndTime < beginTime)
                {
                    return endTime - beginTime;
                }
                else
                {
                    if (timeBoost.EndTime < endTime)
                        return new Duration((Int64)((timeBoost.EndTime - beginTime).ticks * timeBoost.Speed) + (endTime - timeBoost.EndTime).ticks);
                    else
                        return new Duration((Int64)((endTime - beginTime).ticks * timeBoost.Speed));
                }
            }
			public static TimePoint GetBoostedEndTime(this STimeBoost timeBoost, TimePoint beginTime, Duration boostedDuration)
			{
				if (boostedDuration.ticks <= 0)
					return beginTime;

				if (timeBoost.EndTime < beginTime)
				{
					return beginTime + boostedDuration;
				}
				else
				{
					var BoostDuration = new Duration((Int64)((timeBoost.EndTime - beginTime).ticks * timeBoost.Speed));
					if (boostedDuration <= BoostDuration)
						return beginTime + new Duration((Int64)(boostedDuration.ticks / timeBoost.Speed));
					else
						return timeBoost.EndTime + (boostedDuration - BoostDuration);
				}
			}
		    public static bool ChangeBeginTime(this STimeBoost timeBoost, TimePoint now, double speed, ref TimePoint beginTime)
		    {
			    if (timeBoost.IsInBoost(now))
				    return false;

			    if (beginTime <= timeBoost.EndTime)
				    beginTime = now - (timeBoost.EndTime - beginTime) - GetRealDuration(now - timeBoost.EndTime, speed);
			    else
				    beginTime = now - GetRealDuration(now - beginTime, speed);

			    return true;
		    }
            public static string ToString(STimeBoost timeBoost)
            {
                return " EndTime:" + timeBoost.EndTime.ToString() + " Speed:" + timeBoost.Speed;
            }
        }
    }
}
