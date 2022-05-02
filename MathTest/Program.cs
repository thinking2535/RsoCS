using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rso;
using rso.math;


namespace UnityTest
{
    class Program
    {
        static void Main(String[] args)
        {
            var t = new CTracer(10, 11, 0, 10000, 12);

            Console.WriteLine((long)t);

            // test fresnel
            //for(double d = 0.0; d < 10.0; d += 0.1)
            //{
            //    Console.WriteLine(alglib.errorfunction(d) + "   " + CMath.Erf(d));
            //}
        }
    }
}
