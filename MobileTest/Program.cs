using System;
using rso.core;
using rso.Base;
using rso.net;
using System.Threading;
using System.Collections.Generic;

namespace NetworkTest
{
    using TPeerCnt = UInt32;

    class Program
    {
        static CPeriod Period = new CPeriod(TimeSpan.FromMilliseconds(1));
        class SData
        {
	        public long SendNum = 0;
	        public long RecvNum = 0;
        }
        static Dictionary<CKey, SData> Clients = new Dictionary<CKey, SData>();
        static rso.mobile.CClient Net = new rso.mobile.CClient(
            Link, LinkFail, UnLink, Recv,
            false, 1024000, 1024000,
             TimeSpan.Zero, TimeSpan.Zero, 60,
             LinkSoft, UnLinkSoft, TimeSpan.FromSeconds(30));

        static void Link(CKey Key_ )
        {
            Clients.Add(Key_, new SData());
            Console.WriteLine("Link:" + Key_.PeerNum);
        }
        static void LinkFail(TPeerCnt PeerNum_, ENetRet NetRet_)
        {
            Console.WriteLine("LinkFail PeerNum:" + PeerNum_);
        }
        static void UnLink(CKey Key_, ENetRet NetRet_)
        {
            Clients.Remove(Key_);
            Console.WriteLine("UnLink");
        }
        static void Recv(CKey Key_, CStream Stream_)
        {
            long Counter = 0;
            Stream_.Pop(ref Counter);

            var itClient = Clients[Key_];
            if (Counter != itClient.RecvNum++)
            {
                Console.WriteLine("RecvData Error");
                throw new Exception();
            }

            if (Counter % 10 == 0)
                Console.WriteLine("Recv Key[" + Key_.PeerCounter + " " + Key_.PeerNum + "] Counter:" + Counter);
        }
        static void LinkSoft(CKey Key_)
        {
            Console.WriteLine("LinkSoft:" + Key_.PeerNum);
        }
        static void UnLinkSoft(CKey Key_, ENetRet NetRet_)
        {
            Console.WriteLine("UnLinkSoft");
        }

        static void Main(String[] args)
        {
            if (Net.Connect(new CNamePort("192.168.0.31", 20000)) == null)
                throw new Exception("Can Not Connect");

            while (true)
            {
                Net.Proc();

                if (Period.CheckAndNextFixed())
                {
                    foreach (var i in Clients)
                        Net.Send(i.Key, i.Value.SendNum++);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }
    }
}
