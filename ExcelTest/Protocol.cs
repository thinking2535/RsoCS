using System;
using System.Collections.Generic;
using rso.core;


public class STest : SProto
{
	public Int32[] statuses = new Int32[2];
	public STest()
	{
		for (int istatuses = 0; istatuses < statuses.Length; ++istatuses)
			statuses[istatuses] = default(Int32);
	}
	public STest(STest Obj_)
	{
		statuses = Obj_.statuses;
	}
	public STest(Int32[] statuses_)
	{
		statuses = statuses_;
	}
	public override void Push(CStream Stream_)
	{
		Stream_.Pop(ref statuses);
	}
	public override void Push(JsonDataObject Value_)
	{
		Value_.Pop("statuses", ref statuses);
	}
	public override void Pop(CStream Stream_)
	{
		Stream_.Push(statuses);
	}
	public override void Pop(JsonDataObject Value_)
	{
		Value_.Push("statuses", statuses);
	}
	public void Set(STest Obj_)
	{
		statuses = Obj_.statuses;
	}
	public override string StdName()
	{
		return 
			SEnumChecker.GetStdName(statuses);
	}
	public override string MemberName()
	{
		return 
			SEnumChecker.GetMemberName(statuses, "statuses");
	}
}
