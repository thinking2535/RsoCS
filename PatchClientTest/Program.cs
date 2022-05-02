using rso.core;
using rso.gameutil;
using rso.http;
using rso.net;
using rso.patch;
using System;
using System.Threading;

namespace PatchClientTest
{
    using TPeerCnt = UInt32;
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
        static void DownloadChanged(string ObjectName_, long Received_, long Total_)
        {
            Console.WriteLine("DownloadChanged ObjectName:" + ObjectName_ + " Received:" + Received_.ToString() + " Total:" + Total_.ToString());
        }
        static void DownloadCompleted(EHttpRet Ret_, string ObjectName_)
        {
            Console.WriteLine("DownloadCompleted HttpRet:" + Ret_.ToString() + " ObjectName:" + ObjectName_);
        }
        static void Main(String[] args)
        {
            COptionJson<SOption> _Option = new COptionJson<SOption>("Option.ini", true);

            var Net = new rso.patch.CClient(
                _Option.Data.DataFileName, _Option.Data.ServerName, true, _Option.Data.DataPath,
                Link, LinkFail, UnLink, DownloadChanged, DownloadCompleted);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.C:
                            if (!Net.Connect(_Option.Data.BalanceDataPath, new CNamePort(_Option.Data.MasterNamePort)))
                                Console.WriteLine("Connect Fail");
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
