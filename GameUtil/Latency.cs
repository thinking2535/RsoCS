using rso.core;
using System;

namespace rso.gameutil
{
    public class CLatency
    {
        readonly Duration _maxLatency;
        readonly Duration _subDuration = new Duration(100000);
        Duration _offset = Duration.zero;
        Duration _latency = Duration.zero;

        public CLatency(Duration maxLatency)
        {
            _maxLatency = maxLatency;
        }
        public void recv(TimePoint time, TimePoint remoteTime)
        {
            var duration = remoteTime - time;
            if (_offset < duration)
                _offset = duration;

            duration = (time + _offset) - remoteTime;
            if (duration > _maxLatency)
                _latency = _maxLatency;
            else if (_latency - duration > _subDuration)
                _latency -= _subDuration;
            else
                _latency = duration;
        }
        public bool proc(TimePoint time, TimePoint remoteTime)
        {
            return time + _offset - _latency >= remoteTime;
        }
        public Duration getOffset()
        {
            return _offset;
        }
        public Duration getLatency()
        {
            return _latency;
        }
    }
}