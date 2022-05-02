using System;
using rso.unity;
using UnityEngine;

namespace UnityTest
{
    public class CLinear
    {
        float _Slope = 0.0f;
        float _StartValue = 0.0f;
        float _StartTime = Time.time;
        public void Set(float Duration_, float StartValue_, float EndValue_)
        {
            _Slope = (EndValue_ - StartValue_) / Duration_;
            _StartValue = StartValue_;
            _StartTime = Time.time;
        }
        public float Get()
        {
            return (_Slope * (Time.time - _StartTime) + _StartValue);
        }
    }

    class Program
    {
        static void Main(String[] args)
        {
            float Duration = 1.0f;
            float Start = Time.time;

            const int to = 1000;
            var s = Environment.TickCount;
            var e = Environment.TickCount;

            s = Environment.TickCount;
            for (int i = 0; i < to; ++i)
            {
                Mathf.Lerp(0.0f, 1.0f, (Time.time - Start) / Duration);
            }
            e = Environment.TickCount;
            Console.WriteLine(e - s);

            var Linear = new CLinear();
            Linear.Set(Duration, 0.0f, 1.0f);

            s = Environment.TickCount;
            for (int i = 0; i < to; ++i)
            {
                Linear.Get();
            }
            e = Environment.TickCount;
            Console.WriteLine(e - s);
        }
    }
}
