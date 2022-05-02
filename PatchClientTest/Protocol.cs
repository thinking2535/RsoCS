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
	public String ServerName = string.Empty;
	public String DataPath = string.Empty;
	public String DataFileName = string.Empty;
	public SNamePort MasterNamePort = new SNamePort();
	public String BalanceDataPath = string.Empty;
	public SOption()
	{
	}
	public SOption(SOption Obj_)
	{
		ServerName = Obj_.ServerName;
		DataPath = Obj_.DataPath;
		DataFileName = Obj_.DataFileName;
		MasterNamePort = Obj_.MasterNamePort;
		BalanceDataPath = Obj_.BalanceDataPath;
	}
	public SOption(String ServerName_, String DataPath_, String DataFileName_, SNamePort MasterNamePort_, String BalanceDataPath_)
	{
		ServerName = ServerName_;
		DataPath = DataPath_;
		DataFileName = DataFileName_;
		MasterNamePort = MasterNamePort_;
		BalanceDataPath = BalanceDataPath_;
	}
	public override void Push(CStream Stream_)
	{
		Stream_.Pop(ref ServerName);
		Stream_.Pop(ref DataPath);
		Stream_.Pop(ref DataFileName);
		Stream_.Pop(ref MasterNamePort);
		Stream_.Pop(ref BalanceDataPath);
	}
	public override void Push(JsonDataObject Value_)
	{
		Value_.Pop("ServerName", ref ServerName);
		Value_.Pop("DataPath", ref DataPath);
		Value_.Pop("DataFileName", ref DataFileName);
		Value_.Pop("MasterNamePort", ref MasterNamePort);
		Value_.Pop("BalanceDataPath", ref BalanceDataPath);
	}
	public override void Pop(CStream Stream_)
	{
		Stream_.Push(ServerName);
		Stream_.Push(DataPath);
		Stream_.Push(DataFileName);
		Stream_.Push(MasterNamePort);
		Stream_.Push(BalanceDataPath);
	}
	public override void Pop(JsonDataObject Value_)
	{
		Value_.Push("ServerName", ServerName);
		Value_.Push("DataPath", DataPath);
		Value_.Push("DataFileName", DataFileName);
		Value_.Push("MasterNamePort", MasterNamePort);
		Value_.Push("BalanceDataPath", BalanceDataPath);
	}
	public void Set(SOption Obj_)
	{
		ServerName = Obj_.ServerName;
		DataPath = Obj_.DataPath;
		DataFileName = Obj_.DataFileName;
		MasterNamePort.Set(Obj_.MasterNamePort);
		BalanceDataPath = Obj_.BalanceDataPath;
	}
	public override string StdName()
	{
		return 
			SEnumChecker.GetStdName(ServerName) + "," + 
			SEnumChecker.GetStdName(DataPath) + "," + 
			SEnumChecker.GetStdName(DataFileName) + "," + 
			SEnumChecker.GetStdName(MasterNamePort) + "," + 
			SEnumChecker.GetStdName(BalanceDataPath);
	}
	public override string MemberName()
	{
		return 
			SEnumChecker.GetMemberName(ServerName, "ServerName") + "," + 
			SEnumChecker.GetMemberName(DataPath, "DataPath") + "," + 
			SEnumChecker.GetMemberName(DataFileName, "DataFileName") + "," + 
			SEnumChecker.GetMemberName(MasterNamePort, "MasterNamePort") + "," + 
			SEnumChecker.GetMemberName(BalanceDataPath, "BalanceDataPath");
	}
}
