using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rso;
using rso.Base;
using rso.util;

namespace UtilTest
{
    class Program
    {
        static void Main(String[] args)
        {
#if false
            CControlLimiter cl = new CControlLimiter();
            Random rn = new Random();

            while(true)
            {
                int r = rn.Next();
                r %= 31;
                double Theta = (double)r / 1000.0;
                if (cl.Set(Theta))
                    Console.WriteLine(String.Format("Theta{0} !!!!!!!!!!!!!!!!!", Theta));
                else
                    Console.WriteLine(String.Format("Theta{0}", Theta));

                Thread.Sleep(rn.Next() % 100);
            }

#elif true
            var Str = CGUID.Create();
            Console.WriteLine(Str);
            Console.WriteLine(CGUID.Check(Str));
#elif true

            // 속도 측정 /////////////////////
            //var sum = DateTime.Now.Ticks;
            //sum = 0;
            //var s = DateTime.Now.Ticks;
            //for (int i = 0; i < 10000000; ++i)
            //{
            //    sum += DateTime.Now.Ticks;
            //}
            //var e = DateTime.Now.Ticks;
            //Console.WriteLine(e - s);


            //var sum2 = Environment.TickCount;
            //sum2 = 0;
            //s = DateTime.Now.Ticks;
            //for (int i = 0; i < 10000000; ++i)
            //{
            //    sum2 += Environment.TickCount;
            //}
            //e = DateTime.Now.Ticks;
            //Console.WriteLine(e - s);


            // 정밀도 측정 /////////////////////

            int c_Rep = 1000;

            var OldTick = DateTime.Now.Ticks;
            var NowTick = OldTick;
            for(int i=0; i< c_Rep;)
            {
                // Console.WriteLine(Environment.TickCount);
                NowTick = DateTime.Now.Ticks;

                if (NowTick != OldTick)
                {
                    OldTick = NowTick;
                    Console.WriteLine(NowTick);
                    ++i;
                }
            }


            var OldTick2 = Environment.TickCount;
            var NowTick2 = OldTick2;
            for (int i = 0; i < c_Rep;)
            {
                NowTick2 = Environment.TickCount;

                if (NowTick2 != OldTick2)
                {
                    OldTick2 = NowTick2;
                    Console.WriteLine(NowTick2);
                    ++i;
                }
            }

#endif
        }
    }
}
