using TSize = System.Int32;
using TCheckSum = System.UInt64;
using TUID = System.Int64;
using TPeerCnt = System.UInt32;
using TLongIP = System.UInt32;
using TPort = System.UInt16;
using TPacketSeq = System.UInt64;
using TSessionCode = System.Int64;
using SRangeUID = rso.net.SRangeKey<System.Int64>;
using System;
using System.Collections.Generic;
using rso.core;


using rso.net;
public class SOption : SProto
{
	public String DataPath = string.Empty;
	public String DataFileName = string.Empty;
	public SNamePort MasterNamePort = new SNamePort();
	public String ID = string.Empty;
	public String PW = string.Empty;
	public SOption()
	{
	}
	public SOption(SOption Obj_)
	{
		DataPath = Obj_.DataPath;
		DataFileName = Obj_.DataFileName;
		MasterNamePort = Obj_.MasterNamePort;
		ID = Obj_.ID;
		PW = Obj_.PW;
	}
	public SOption(String DataPath_, String DataFileName_, SNamePort MasterNamePort_, String ID_, String PW_)
	{
		DataPath = DataPath_;
		DataFileName = DataFileName_;
		MasterNamePort = MasterNamePort_;
		ID = ID_;
		PW = PW_;
	}
	public override void Push(CStream Stream_)
	{
		Stream_.Pop(ref DataPath);
		Stream_.Pop(ref DataFileName);
		Stream_.Pop(ref MasterNamePort);
		Stream_.Pop(ref ID);
		Stream_.Pop(ref PW);
	}
	public override void Push(JsonDataObject Value_)
	{
		Value_.Pop("DataPath", ref DataPath);
		Value_.Pop("DataFileName", ref DataFileName);
		Value_.Pop("MasterNamePort", ref MasterNamePort);
		Value_.Pop("ID", ref ID);
		Value_.Pop("PW", ref PW);
	}
	public override void Pop(CStream Stream_)
	{
		Stream_.Push(DataPath);
		Stream_.Push(DataFileName);
		Stream_.Push(MasterNamePort);
		Stream_.Push(ID);
		Stream_.Push(PW);
	}
	public override void Pop(JsonDataObject Value_)
	{
		Value_.Push("DataPath", DataPath);
		Value_.Push("DataFileName", DataFileName);
		Value_.Push("MasterNamePort", MasterNamePort);
		Value_.Push("ID", ID);
		Value_.Push("PW", PW);
	}
	public void Set(SOption Obj_)
	{
		DataPath = Obj_.DataPath;
		DataFileName = Obj_.DataFileName;
		MasterNamePort.Set(Obj_.MasterNamePort);
		ID = Obj_.ID;
		PW = Obj_.PW;
	}
	public override string StdName()
	{
		return 
			SEnumChecker.GetStdName(DataPath) + "," + 
			SEnumChecker.GetStdName(DataFileName) + "," + 
			SEnumChecker.GetStdName(MasterNamePort) + "," + 
			SEnumChecker.GetStdName(ID) + "," + 
			SEnumChecker.GetStdName(PW);
	}
	public override string MemberName()
	{
		return 
			SEnumChecker.GetMemberName(DataPath, "DataPath") + "," + 
			SEnumChecker.GetMemberName(DataFileName, "DataFileName") + "," + 
			SEnumChecker.GetMemberName(MasterNamePort, "MasterNamePort") + "," + 
			SEnumChecker.GetMemberName(ID, "ID") + "," + 
			SEnumChecker.GetMemberName(PW, "PW");
	}
}
