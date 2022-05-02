using TCheckSum = System.UInt64;
using TSize = System.Int32;
using TUID = System.Int64;
using TPeerCnt = System.UInt32;
using TData = System.SByte;
using TLongIP = System.UInt32;
using TPort = System.UInt16;
using TPacketSeq = System.UInt64;
using System;
using System.Collections.Generic;
using rso;
using rso.proto;


namespace rso
{
	using Base;
	namespace net
	{
		public class SHeader : IProto
		{
			public TSize Size;
			public TCheckSum CheckSum;
			public TPacketSeq PacketSeq;
			public SHeader()
			{
			}
			public SHeader(SHeader Obj_)
			{
				Size = Obj_.Size;
				CheckSum = Obj_.CheckSum;
				PacketSeq = Obj_.PacketSeq;
			}
			public SHeader(TSize Size_,TCheckSum CheckSum_,TPacketSeq PacketSeq_ )
			{
				Size = Size_;
				CheckSum = CheckSum_;
				PacketSeq = PacketSeq_;
			}
			public bool Push(CStream Stream_)
			{
				if( !(Stream_.Pop(ref Size)) ) return false;
				if( !(Stream_.Pop(ref CheckSum)) ) return false;
				if( !(Stream_.Pop(ref PacketSeq)) ) return false;
				return true;
			}
			public bool Pop(CStream Stream_o)
			{
				if( !(Stream_o.Push(Size)) ) return false;
				if( !(Stream_o.Push(CheckSum)) ) return false;
				if( !(Stream_o.Push(PacketSeq)) ) return false;
				return true;
			}
		}
		public class _SIPPort : IProto
		{
			public TLongIP IP;
			public TPort Port;
			public _SIPPort()
			{
			}
			public _SIPPort(_SIPPort Obj_)
			{
				IP = Obj_.IP;
				Port = Obj_.Port;
			}
			public _SIPPort(TLongIP IP_,TPort Port_ )
			{
				IP = IP_;
				Port = Port_;
			}
			public bool Push(CStream Stream_)
			{
				if( !(Stream_.Pop(ref IP)) ) return false;
				if( !(Stream_.Pop(ref Port)) ) return false;
				return true;
			}
			public bool Pop(CStream Stream_o)
			{
				if( !(Stream_o.Push(IP)) ) return false;
				if( !(Stream_o.Push(Port)) ) return false;
				return true;
			}
		}
		public enum ENetState
		{
			Ok,
			Normal,
			SocketFail,
			ConnectFail,
			SocketOptFail,
			Max,
			Null
		}
	}
}
