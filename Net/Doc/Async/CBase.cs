using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rso.proto;
using rso.Base;
using rso.net;
using System.Net;

namespace rso
{
    namespace net
    {
        using TLongIP = UInt32;
        using TPort = UInt16;

        public class SIPPort : _SIPPort
        {
            public SIPPort()
            {
            }
            public SIPPort(TLongIP IP_, TPort Port_) :
                base(IP_, Port_)
            {
            }
            public SIPPort(String IP_, TPort Port_) :
                this(new IPEndPoint(IPAddress.Parse(IP_), (int)Port_))
            {
            }
            public SIPPort(_SIPPort IPPort_)
            {
                IP = IPPort_.IP;
                Port = IPPort_.Port;
            }
            public SIPPort(IPEndPoint EndPoint_) :
                base(BitConverter.ToUInt32(EndPoint_.Address.GetAddressBytes(), 0),
                (TPort)EndPoint_.Port)
            {
            }

            public bool IsNull
            {
                get { return (IP == 0); }
            }

            public void SetNull()
            {
                IP = 0;
                Port = 0;
            }

            public IPEndPoint EndPoint
            {
                get { return new IPEndPoint(IP, Port); }
                set
                {
                    IP = BitConverter.ToUInt32(value.Address.GetAddressBytes(), 0);
                    Port = Convert.ToUInt16(value.Port);
                }
            }

            public override bool Equals(System.Object obj)
            {
                SIPPort p = obj as SIPPort;
                if ((object)p == null)
                {
                    return false;
                }

                return (IP == p.IP && Port == p.Port);
            }
            public bool Equals(SIPPort Obj_)
            {
                return (Obj_ != null && IP == Obj_.IP && Port == Obj_.Port);
            }
            public override int GetHashCode()
            {
                return 0;
            }
        }
        public class CGlobal
        {
            public static TLongIP GetLongIPByName(String Name_)
            {
                try
                {
                    var LongIPs = Dns.GetHostAddresses(Name_);
                    if (LongIPs.Count() == 0)
                        return 0;

                    return (TLongIP)LongIPs.First().Address;
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}