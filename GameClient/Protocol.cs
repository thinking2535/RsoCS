using TSize = System.Int32;
using TCheckSum = System.UInt64;
using TUID = System.Int64;
using TPeerCnt = System.UInt32;
using TLongIP = System.UInt32;
using TPort = System.UInt16;
using TPacketSeq = System.UInt64;
using TSessionCode = System.Int64;
using System;
using System.Collections.Generic;
using rso.core;


using rso;
public enum EProtoNetCs
{
	GameProto,
	Max,
}
public enum EProtoNetSc
{
	GameProto,
	Max,
}
public class SGameProtoNetCs : SProto
{
	public Boolean b = false;
	public Int16 i16 = 0;
	public Int32 i32 = 0;
	public Int64 i64 = 0;
	public String w = "";
	public DateTime dt = new DateTime();
	public TimePoint tp = new TimePoint();
	public SGameProtoNetCs()
	{
	}
	public SGameProtoNetCs(SGameProtoNetCs Obj_)
	{
		b = Obj_.b;
		i16 = Obj_.i16;
		i32 = Obj_.i32;
		i64 = Obj_.i64;
		w = Obj_.w;
		dt = Obj_.dt;
		tp = Obj_.tp;
	}
	public SGameProtoNetCs(Boolean b_, Int16 i16_, Int32 i32_, Int64 i64_, String w_, DateTime dt_, TimePoint tp_)
	{
		b = b_;
		i16 = i16_;
		i32 = i32_;
		i64 = i64_;
		w = w_;
		dt = dt_;
		tp = tp_;
	}
	public override void Push(CStream Stream_)
	{
		Stream_.Pop(ref b);
		Stream_.Pop(ref i16);
		Stream_.Pop(ref i32);
		Stream_.Pop(ref i64);
		Stream_.Pop(ref w);
		Stream_.Pop(ref dt);
		Stream_.Pop(ref tp);
	}
	public override void Push(JsonDataObject Value_)
	{
		Value_.Pop("b", ref b);
		Value_.Pop("i16", ref i16);
		Value_.Pop("i32", ref i32);
		Value_.Pop("i64", ref i64);
		Value_.Pop("w", ref w);
		Value_.Pop("dt", ref dt);
		Value_.Pop("tp", ref tp);
	}
	public override void Pop(CStream Stream_)
	{
		Stream_.Push(b);
		Stream_.Push(i16);
		Stream_.Push(i32);
		Stream_.Push(i64);
		Stream_.Push(w);
		Stream_.Push(dt);
		Stream_.Push(tp);
	}
	public override void Pop(JsonDataObject Value_)
	{
		Value_.Push("b", b);
		Value_.Push("i16", i16);
		Value_.Push("i32", i32);
		Value_.Push("i64", i64);
		Value_.Push("w", w);
		Value_.Push("dt", dt);
		Value_.Push("tp", tp);
	}
	public void Set(SGameProtoNetCs Obj_)
	{
		b = Obj_.b;
		i16 = Obj_.i16;
		i32 = Obj_.i32;
		i64 = Obj_.i64;
		w = Obj_.w;
		dt = Obj_.dt;
		tp = Obj_.tp;
	}
	public override string StdName()
	{
		return "bool,int16,int32,int64,wstring,datetime,time_point";
	}
	public override string MemberName()
	{
		return "b,i16,i32,i64,w,dt,tp";
	}
}
public class SGameProtoNetSc : SProto
{
	public Boolean b = false;
	public Int16 i16 = 0;
	public Int32 i32 = 0;
	public Int64 i64 = 0;
	public String w = "";
	public DateTime dt = new DateTime();
	public TimePoint tp = new TimePoint();
	public SGameProtoNetSc()
	{
	}
	public SGameProtoNetSc(SGameProtoNetSc Obj_)
	{
		b = Obj_.b;
		i16 = Obj_.i16;
		i32 = Obj_.i32;
		i64 = Obj_.i64;
		w = Obj_.w;
		dt = Obj_.dt;
		tp = Obj_.tp;
	}
	public SGameProtoNetSc(Boolean b_, Int16 i16_, Int32 i32_, Int64 i64_, String w_, DateTime dt_, TimePoint tp_)
	{
		b = b_;
		i16 = i16_;
		i32 = i32_;
		i64 = i64_;
		w = w_;
		dt = dt_;
		tp = tp_;
	}
	public override void Push(CStream Stream_)
	{
		Stream_.Pop(ref b);
		Stream_.Pop(ref i16);
		Stream_.Pop(ref i32);
		Stream_.Pop(ref i64);
		Stream_.Pop(ref w);
		Stream_.Pop(ref dt);
		Stream_.Pop(ref tp);
	}
	public override void Push(JsonDataObject Value_)
	{
		Value_.Pop("b", ref b);
		Value_.Pop("i16", ref i16);
		Value_.Pop("i32", ref i32);
		Value_.Pop("i64", ref i64);
		Value_.Pop("w", ref w);
		Value_.Pop("dt", ref dt);
		Value_.Pop("tp", ref tp);
	}
	public override void Pop(CStream Stream_)
	{
		Stream_.Push(b);
		Stream_.Push(i16);
		Stream_.Push(i32);
		Stream_.Push(i64);
		Stream_.Push(w);
		Stream_.Push(dt);
		Stream_.Push(tp);
	}
	public override void Pop(JsonDataObject Value_)
	{
		Value_.Push("b", b);
		Value_.Push("i16", i16);
		Value_.Push("i32", i32);
		Value_.Push("i64", i64);
		Value_.Push("w", w);
		Value_.Push("dt", dt);
		Value_.Push("tp", tp);
	}
	public void Set(SGameProtoNetSc Obj_)
	{
		b = Obj_.b;
		i16 = Obj_.i16;
		i32 = Obj_.i32;
		i64 = Obj_.i64;
		w = Obj_.w;
		dt = Obj_.dt;
		tp = Obj_.tp;
	}
	public override string StdName()
	{
		return "bool,int16,int32,int64,wstring,datetime,time_point";
	}
	public override string MemberName()
	{
		return "b,i16,i32,i64,w,dt,tp";
	}
}
