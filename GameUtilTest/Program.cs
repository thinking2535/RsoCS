using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rso;
using rso.core;
using rso.Base;
using rso.physics;
using rso.gameutil;
using rso.net;

namespace GameUtilTest
{
    public class SAccount : SProto
    {
        public String ID = "";
        public String PW = "";
        public SAccount()
        {
        }
        public SAccount(SAccount Obj_)
        {
            ID = Obj_.ID;
            PW = Obj_.PW;
        }
        public SAccount(String ID_, String PW_)
        {
            ID = ID_;
            PW = PW_;
        }
        public override void Push(CStream Stream_)
        {
            Stream_.Pop(ref ID);
            Stream_.Pop(ref PW);
        }
        public override void Push(JsonDataObject Value_)
        {
            Value_.Pop("ID", ref ID);
            Value_.Pop("PW", ref PW);
        }
        public override void Pop(CStream Stream_)
        {
            Stream_.Push(ID);
            Stream_.Push(PW);
        }
        public override void Pop(JsonDataObject Value_)
        {
            Value_.Push("ID", ID);
            Value_.Push("PW", PW);
        }
        public override string StdName()
        {
            return "wstring,wstring";
        }
        public override string MemberName()
        {
            return "ID,PW";
        }
    }
    class Program
    {
        static void Main(String[] args)
        {
#if false
            CControlFixed ControlFixed = new CControlFixed(new SPoint(0.0, 0.0), 10.0, 12.0);
            var Vector = ControlFixed.Down(new SPoint());
            if (Vector == null)
                Console.WriteLine("Null");
            else
                Console.WriteLine(Vector.X + " " + Vector.Y);

            Vector = ControlFixed.Move(new SPoint(10.0, 0.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);

            Vector = ControlFixed.Move(new SPoint(10.0, 10.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);

            Console.WriteLine("-----------------------------------");

            CControlImmovable ControlImmovable = new CControlImmovable(3.0);
            ControlImmovable.Down(new SPoint(7.0, 0.0));

            Vector = ControlImmovable.Move(new SPoint(10.0, 0.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);

            Vector = ControlImmovable.Move(new SPoint(10.0, 1.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlImmovable.Move(new SPoint(10.0, 2.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlImmovable.Move(new SPoint(10.0, 3.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlImmovable.Move(new SPoint(10.0, 4.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlImmovable.Move(new SPoint(10.0, 5.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlImmovable.Move(new SPoint(10.0, 6.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlImmovable.Move(new SPoint(10.0, 7.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlImmovable.Move(new SPoint(10.0, 8.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlImmovable.Move(new SPoint(10.0, 9.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlImmovable.Move(new SPoint(10.0, 10.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);

            for (double d = 10.0; d < 100.0; d += 1.0)
            {
                Vector = ControlImmovable.Move(new SPoint(10.0, d));
                Console.WriteLine(Vector.X + " " + Vector.Y);
            }



            Console.WriteLine("-----------------------------------");

            CControlMovable ControlMovable = new CControlMovable(3.0);
            ControlMovable.Down(new SPoint());

            Vector = ControlMovable.Move(new SPoint(10.0, 0.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);

            Vector = ControlMovable.Move(new SPoint(10.0, 1.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlMovable.Move(new SPoint(10.0, 2.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlMovable.Move(new SPoint(10.0, 3.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlMovable.Move(new SPoint(10.0, 4.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlMovable.Move(new SPoint(10.0, 5.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlMovable.Move(new SPoint(10.0, 6.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlMovable.Move(new SPoint(10.0, 7.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlMovable.Move(new SPoint(10.0, 8.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlMovable.Move(new SPoint(10.0, 9.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);
            Vector = ControlMovable.Move(new SPoint(10.0, 10.0));
            Console.WriteLine(Vector.X + " " + Vector.Y);

            for(double d = 10.0; d < 100.0; d += 1.0)
            {
                Vector = ControlMovable.Move(new SPoint(10.0, d));
                Console.WriteLine(Vector.X + " " + Vector.Y);
            }

#elif false
            // Test TimeBoost

            //var BoostStartTime = new TimePoint(new DateTime(2019, 1, 4, 0, 0, 9));
            //var BoostEndTime = new TimePoint(new DateTime(2019, 1, 4, 0, 0, 11));
            //var TimeBoost = new STimeBoost(BoostStartTime, BoostEndTime, 2.0);
            //Console.WriteLine(CTimeBoost.GetRealTime(TimeBoost,
            //    new TimePoint(new DateTime(2019, 1, 4, 0, 0, 10)),
            //    TimeSpan.FromSeconds(20)));

            var BoostStartTime = new TimePoint(new DateTime(2019, 1, 4, 10, 42, 21));
            var BoostEndTime = new TimePoint(new DateTime(2019, 1, 4, 11, 42, 21));
            var TimeBoost = new STimeBoost(BoostStartTime, BoostEndTime, 10.0);
            Console.WriteLine(TimeBoost.GetEndTime(new TimePoint(new DateTime(2019, 1, 4, 10, 49, 04)), TimeSpan.FromMinutes(240)));


            //var Now = TimePoint.Now;
            //var MidTime = (Now + TimeSpan.FromSeconds(5));
            //var EndTime = (Now + TimeSpan.FromSeconds(10));
            //var Boost = new CTimeBoost(new STimeBoost(new STimer(false, Now, EndTime), 0.5));

            //for (var n = TimePoint.Now; n < MidTime; n = TimePoint.Now)
            //{
            //    Console.WriteLine(Boost.GetElapsedDuration(n).TotalMilliseconds);
            //    Thread.Sleep(100);
            //}

            //Boost.Set(TimePoint.Now, EndTime, 3.0);

            //for (var n = TimePoint.Now; n < EndTime; n = TimePoint.Now)
            //{
            //    Console.WriteLine(Boost.GetElapsedDuration(n).TotalMilliseconds);
            //    Thread.Sleep(100);
            //}

#elif false

            var Option = new COptionJson<SAccount>("Option.ini", true);
            Option.Data = new SAccount("Ř", "Ř");
            Console.WriteLine(Option.Data.ToJsonString());

#elif false

            var keyCnt = new CKeyCntInt<int>();
            keyCnt.Plus(1, 1);
            keyCnt.Plus(1, 1);
            keyCnt.Plus(2, 3);
            keyCnt.Plus(2, -4);
            keyCnt.Multied(2);
            keyCnt.Multied(0);

            foreach (var i in keyCnt)
                Console.WriteLine(i.Key + " " + i.Value);

#elif true

            var r = new CRank<int, int>();
            r.Add(2, 0);
            r.Add(4, 1);

            for (int i = -1; i < 7; ++i)
            {
                var t = r.Get(i);
                if (t == null)
                    Console.WriteLine(i + " null");
                else
                    Console.WriteLine(i + " " + t.Value.Key + " " + t.Value.Value);
            }
#endif
        }
    }
}
