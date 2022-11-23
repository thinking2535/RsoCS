using rso.core;
using System;

namespace rso.gameutil
{
    public class PeriodicUpdater
    {
        public readonly Duration period;
        public TimePoint lastUpdatedTime { get; private set; }
        public PeriodicUpdater(Duration period, TimePoint lastUpdatedTime)
        {
            this.period = period;
            this.lastUpdatedTime = lastUpdatedTime;
        }
        public bool update(TimePoint now)
        {
            var elapsed = now - lastUpdatedTime;
            var elapsedPeriodCount = elapsed.ticks / period.ticks;
            if (elapsedPeriodCount <= 0)
                return false;

            lastUpdatedTime += new Duration(period.ticks * elapsedPeriodCount);
            return true;
        }
        public Duration getLeftTimeSpanForUpdate(TimePoint now)
        {
            var leftTimeSpanForUpdate = (lastUpdatedTime + period) - now;
            if (leftTimeSpanForUpdate < Duration.zero)
                return Duration.zero;
            else
                return leftTimeSpanForUpdate;
        }
    }
}
