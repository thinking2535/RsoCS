using rso;
using rso.balance;
using rso.core;
using rso.net;
using System;
using System.Threading;
using TPeerCnt = System.UInt32;

namespace BalanceTest
{
    class Program
    {
        static void Link(CKey Key_)
        {
            Console.WriteLine("Link:" + Key_.PeerNum.ToString());
        }
        static void LinkFail(TPeerCnt PeerNum_, ENetRet NetRet_)
        {
            Console.WriteLine("LinkFail PeerNum:" + PeerNum_.ToString());
        }
        static void UnLink(CKey Key_, ENetRet NetRet_)
        {
            Console.WriteLine("UnLink PeerNum :" + Key_.PeerNum.ToString());
        }
        static void Recv(CKey Key_, CStream Stream_)
        {
            Console.WriteLine("Recv:" + Key_.PeerNum.ToString());
        }
        static void Main(String[] args)
        {
            var Net = new rso.balance.CClient(Link, LinkFail, UnLink, Recv);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.C:
                            if (!Net.Connect("Data/", new CNamePort("127.0.0.1", 40000)))
                                Console.WriteLine("Connect Fail");
                            break;
                        case ConsoleKey.D:
                            Net.CloseAll();
                            break;
                        default:
                            break;
                    }
                }
                Net.Proc();
                Thread.Sleep(1);
            }
        }
    }
}
