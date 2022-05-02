using rso.http;
using System;
using System.IO;
using System.Threading;

namespace HttpTest
{
    class Program
    {
        static void DownloadChangedCallback(int SessionIndex_, string ObjectName_, long Received_, long Total_)
        {
            Console.WriteLine("SessionIndex : " + SessionIndex_.ToString() + " ObjectName : " + ObjectName_ + " Received : " + Received_.ToString() + " Total : " + Total_.ToString());
        }
        static void DownloadCompletedCallback(int SessionIndex_, EHttpRet Ret_, string ObjectName_, byte[] Buffer_)
        {
            Console.WriteLine("SessionIndex : " + SessionIndex_.ToString() + " EHttpRet : " + Ret_.ToString() + " ObjectName : " + ObjectName_ + " Total : " + Buffer_.Length.ToString());
        }
        static void Main(String[] args)
        {
            var h = new CHttp(4, DownloadChangedCallback, DownloadCompletedCallback);
            //h.Push(new SInObj("http://madplanet-patch.s3.ca-central-1.amazonaws.com", "/ServerInfo.txt"));
            h.Push(new SInObj("http://madplanet-patch.s3.ca-central-1.amazonaws.com", "/data/test2.msi"));

            while (true)
            {
                h.Proc();
                Thread.Sleep(1);
            }
        }
    }
}
