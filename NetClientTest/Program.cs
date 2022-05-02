using System;
using rso.core;
using rso.net;
using System.Threading;

namespace NetworkTest
{
    using TPeerCnt = UInt32;
    using TLongIP = UInt32;

    class Program
    {
        static CKey _Key = null;
        const int c_UserCnt = 1500;
        static CClient net = new CClient(
            Link, LinkFail, UnLink, Recv,
            false, 1024000, 1024000,
            new TimeSpan(100000000), new TimeSpan(100000000), 60);

        static void Link(CKey Key_)
        {
            _Key = Key_;
            Console.WriteLine("Link:" + Key_.PeerNum);
        }
        static void LinkFail(TPeerCnt PeerNum_, ENetRet NetRet_)
        {
            Console.WriteLine("LinkFail PeerNum:" + PeerNum_);
        }
        static void UnLink(CKey Key_, ENetRet NetRet_)
        {
            _Key = null;
            Console.WriteLine("UnLink");
        }
        static void Recv(CKey Key_, CStream Stream_)
        {
            int i = 0;
            Stream_.Pop(ref i);
            Console.WriteLine("Recv:" + Key_.PeerNum + "value:" + i.ToString());
        }

        static void Main(String[] args)
        {
            Console.WriteLine("Started");

            if (net.Connect(new CNamePort("ec2-13-209-140-35.ap-northeast-2.compute.amazonaws.com", 31313)) == null)
                throw new Exception("Can Not Connect");

            Console.WriteLine("Connecting");

            while (_Key == null)
            {
                net.Proc();
                Thread.Sleep(10);
            }
            Console.WriteLine("Connected");

            for (int i = 0; i< 1000; ++i)
            {
                net.Send(_Key, i);
                net.Proc();
            }
            Console.WriteLine("SendDone !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            while (true)
            {
                net.Proc();
                Thread.Sleep(10);
            }
        }
    }
}
