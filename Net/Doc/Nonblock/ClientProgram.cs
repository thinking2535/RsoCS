using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket _Socket = null;
            IPAddress ipAddr = IPAddress.Parse("183.101.158.62");
            IPEndPoint serverEndPoint = new IPEndPoint(ipAddr, 40111);
            int ConnectRepeatCnt = 3;

            var bytesBuff = new byte[1024];

            for (;;)
            {
                if (_Socket == null)
                {
                    try
                    {
                        _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        _Socket.Blocking = false;
                        _Socket.Connect(serverEndPoint);
                    }
                    catch (SocketException e_)
                    {
                        switch(e_.SocketErrorCode)
                        {
                            case SocketError.WouldBlock:
                                bool Connected = false;
                                for(int i=0; i<ConnectRepeatCnt; ++i)
                                {
                                    if (Connected = _Socket.Poll(1000000, SelectMode.SelectWrite)) // Wait until connection is complete
                                        break;
                                }
                                if (!Connected)
                                {
                                    _Socket.Close();
                                    _Socket = null;
                                    Console.WriteLine("Connect Fail");
                                    Thread.Sleep(1000);
                                    continue;
                                }
                                break;
                            default:
                                Console.WriteLine("Socket ErrorCode : {0}", e_.SocketErrorCode);
                                _Socket.Close();
                                _Socket = null;
                                Thread.Sleep(1000);
                                break;
                        }
                    }
                }

                String _buf = "ok";
                Byte[] _data = Encoding.Default.GetBytes(_buf);
                try
                {
                    _Socket.Send(_data);
                }
                catch
                {
                    _Socket.Close();
                    _Socket = null;
                    continue;
                }


                try
                {
                    int byteBytesRecvd = _Socket.Receive(bytesBuff, 0, bytesBuff.Length, SocketFlags.None); // noneblock 이고 아직 수신할 데이터가 없으면 뭘 반환?, 0 이라면 끊겼을때와 구분은 어떻게?
                    if (byteBytesRecvd > 0)
                    {
                        string msg = Encoding.UTF8.GetString(bytesBuff, 0, byteBytesRecvd);
                        Console.WriteLine("We've got [{0}]", msg);
                    }
                    else
                    {
                        _Socket.Close();
                        _Socket = null;
                    }
                }
                catch (SocketException e_)
                {
                    switch(e_.SocketErrorCode)
                    {
                        case SocketError.IOPending:
                            break;
                        case SocketError.WouldBlock:
                            break;
                        default:
                            Console.WriteLine("Socket ErrorCode : {0}", e_.SocketErrorCode);
                            _Socket.Close();
                            _Socket = null;
                            break;
                    }
                }

                // Thread.Sleep(500);
            }
        }
    }
}
