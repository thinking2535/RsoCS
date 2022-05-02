using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication8
{
    class Program
    {
        private const int BACKLOG = 128;
        static void Main(string[] args)
        {
            Socket servSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            servSock.Bind(new IPEndPoint(IPAddress.Any, 40111));
            servSock.Listen(BACKLOG);

            Socket client = null;
            var bytesBuff = new byte[1024];

            for (;;)
            {
                if (client == null)
                {
                    client = servSock.Accept();
                    client.Blocking = false;
                    IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
                    Console.WriteLine(ip.Address);
                }

                try
                {
                    int byteBytesRecvd = client.Receive(bytesBuff, 0, bytesBuff.Length, SocketFlags.None); // noneblock 이고 아직 수신할 데이터가 없으면 뭘 반환?, 0 이라면 끊겼을때와 구분은 어떻게?
                    if (byteBytesRecvd > 0)
                    {
                        string msg = Encoding.UTF8.GetString(bytesBuff, 0, byteBytesRecvd);
                        Console.WriteLine("We've got [{0}]", msg);

                        // Send ////////////////
                        String _buf = "ok";
                        Byte[] _data = Encoding.Default.GetBytes(_buf);
                        client.Send(_data);
                    }
                    else
                    {
                        client.Close();
                        client = null;
                    }
                }
                catch (SocketException e_)
                {
                    switch (e_.SocketErrorCode)
                    {
                        case SocketError.IOPending:
                            break;
                        case SocketError.WouldBlock:
                            break;
                        default:
                            Console.WriteLine("Socket ErrorCode : {0}", e_.SocketErrorCode);
                            client.Close();
                            client = null;
                            break;
                    }
                }
            }

            servSock.Close();
        }
    }
}
