using TA = System.Int32;
using System;
using System.Collections.Generic;
using rso.core;


public class STest : SProto
{
	public TA[] a = new TA[2];
	public STest()
	{
		for (int ia = 0; ia < a.Length; ++ia)
			a[ia] = default(TA);
	}
	public STest(STest Obj_)
	{
		a = Obj_.a;
	}
	public STest(TA[] a_)
	{
		a = a_;
	}
	public override void Push(CStream Stream_)
	{
		Stream_.Pop(ref a);
	}
	public override void Push(JsonDataObject Value_)
	{
		Value_.Pop("a", ref a);
	}
	public override void Pop(CStream Stream_)
	{
		Stream_.Push(a);
	}
	public override void Pop(JsonDataObject Value_)
	{
		Value_.Push("a", a);
	}
	public void Set(STest Obj_)
	{
		a = Obj_.a;
	}
	public override string StdName()
	{
		return 
			SEnumChecker.GetStdName(a);
	}
	public override string MemberName()
	{
		return 
			SEnumChecker.GetMemberName(a, "a");
	}
}
public enum EEnumType
{
	A,
	BB,
	CCC,
}
public class STest2 : SProto
{
	public EEnumType EnumType = default(EEnumType);
	public STest2()
	{
	}
	public STest2(STest2 Obj_)
	{
		EnumType = Obj_.EnumType;
	}
	public STest2(EEnumType EnumType_)
	{
		EnumType = EnumType_;
	}
	public override void Push(CStream Stream_)
	{
		Stream_.Pop(ref EnumType);
	}
	public override void Push(JsonDataObject Value_)
	{
		Value_.Pop("EnumType", ref EnumType);
	}
	public override void Pop(CStream Stream_)
	{
		Stream_.Push(EnumType);
	}
	public override void Pop(JsonDataObject Value_)
	{
		Value_.Push("EnumType", EnumType);
	}
	public void Set(STest2 Obj_)
	{
		EnumType = Obj_.EnumType;
	}
	public override string StdName()
	{
		return 
			"EEnumType";
	}
	public override string MemberName()
	{
		return 
			SEnumChecker.GetMemberName(EnumType, "EnumType");
	}
}
