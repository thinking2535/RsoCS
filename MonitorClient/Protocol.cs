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


namespace rso
{
	namespace monitor
	{
		using rso.net;
		public class SCommand : SProto
		{
			public String Name = string.Empty;
			public String Command = string.Empty;
			public SCommand()
			{
			}
			public SCommand(SCommand Obj_)
			{
				Name = Obj_.Name;
				Command = Obj_.Command;
			}
			public SCommand(String Name_, String Command_)
			{
				Name = Name_;
				Command = Command_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Name);
				Stream_.Pop(ref Command);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Name", ref Name);
				Value_.Pop("Command", ref Command);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Name);
				Stream_.Push(Command);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Name", Name);
				Value_.Push("Command", Command);
			}
			public void Set(SCommand Obj_)
			{
				Name = Obj_.Name;
				Command = Obj_.Command;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Name) + "," + 
					SEnumChecker.GetStdName(Command);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Name, "Name") + "," + 
					SEnumChecker.GetMemberName(Command, "Command");
			}
		}
		public class SOption : SProto
		{
			public String ID = string.Empty;
			public String PW = string.Empty;
			public Int32 ServerNo = default(Int32);
			public List<SNamePort> Servers = new List<SNamePort>();
			public String LocalDirectory = string.Empty;
			public String RemoteDirectory = string.Empty;
			public List<SCommand> Commands = new List<SCommand>();
			public SOption()
			{
			}
			public SOption(SOption Obj_)
			{
				ID = Obj_.ID;
				PW = Obj_.PW;
				ServerNo = Obj_.ServerNo;
				Servers = Obj_.Servers;
				LocalDirectory = Obj_.LocalDirectory;
				RemoteDirectory = Obj_.RemoteDirectory;
				Commands = Obj_.Commands;
			}
			public SOption(String ID_, String PW_, Int32 ServerNo_, List<SNamePort> Servers_, String LocalDirectory_, String RemoteDirectory_, List<SCommand> Commands_)
			{
				ID = ID_;
				PW = PW_;
				ServerNo = ServerNo_;
				Servers = Servers_;
				LocalDirectory = LocalDirectory_;
				RemoteDirectory = RemoteDirectory_;
				Commands = Commands_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ID);
				Stream_.Pop(ref PW);
				Stream_.Pop(ref ServerNo);
				Stream_.Pop(ref Servers);
				Stream_.Pop(ref LocalDirectory);
				Stream_.Pop(ref RemoteDirectory);
				Stream_.Pop(ref Commands);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ID", ref ID);
				Value_.Pop("PW", ref PW);
				Value_.Pop("ServerNo", ref ServerNo);
				Value_.Pop("Servers", ref Servers);
				Value_.Pop("LocalDirectory", ref LocalDirectory);
				Value_.Pop("RemoteDirectory", ref RemoteDirectory);
				Value_.Pop("Commands", ref Commands);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ID);
				Stream_.Push(PW);
				Stream_.Push(ServerNo);
				Stream_.Push(Servers);
				Stream_.Push(LocalDirectory);
				Stream_.Push(RemoteDirectory);
				Stream_.Push(Commands);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ID", ID);
				Value_.Push("PW", PW);
				Value_.Push("ServerNo", ServerNo);
				Value_.Push("Servers", Servers);
				Value_.Push("LocalDirectory", LocalDirectory);
				Value_.Push("RemoteDirectory", RemoteDirectory);
				Value_.Push("Commands", Commands);
			}
			public void Set(SOption Obj_)
			{
				ID = Obj_.ID;
				PW = Obj_.PW;
				ServerNo = Obj_.ServerNo;
				Servers = Obj_.Servers;
				LocalDirectory = Obj_.LocalDirectory;
				RemoteDirectory = Obj_.RemoteDirectory;
				Commands = Obj_.Commands;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ID) + "," + 
					SEnumChecker.GetStdName(PW) + "," + 
					SEnumChecker.GetStdName(ServerNo) + "," + 
					SEnumChecker.GetStdName(Servers) + "," + 
					SEnumChecker.GetStdName(LocalDirectory) + "," + 
					SEnumChecker.GetStdName(RemoteDirectory) + "," + 
					SEnumChecker.GetStdName(Commands);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ID, "ID") + "," + 
					SEnumChecker.GetMemberName(PW, "PW") + "," + 
					SEnumChecker.GetMemberName(ServerNo, "ServerNo") + "," + 
					SEnumChecker.GetMemberName(Servers, "Servers") + "," + 
					SEnumChecker.GetMemberName(LocalDirectory, "LocalDirectory") + "," + 
					SEnumChecker.GetMemberName(RemoteDirectory, "RemoteDirectory") + "," + 
					SEnumChecker.GetMemberName(Commands, "Commands");
			}
		}
	}
}
