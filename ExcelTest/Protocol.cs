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
	public String name = string.Empty;
	public STest2()
	{
	}
	public STest2(STest2 Obj_)
	{
		EnumType = Obj_.EnumType;
		name = Obj_.name;
	}
	public STest2(EEnumType EnumType_, String name_)
	{
		EnumType = EnumType_;
		name = name_;
	}
	public override void Push(CStream Stream_)
	{
		Stream_.Pop(ref EnumType);
		Stream_.Pop(ref name);
	}
	public override void Push(JsonDataObject Value_)
	{
		Value_.Pop("EnumType", ref EnumType);
		Value_.Pop("name", ref name);
	}
	public override void Pop(CStream Stream_)
	{
		Stream_.Push(EnumType);
		Stream_.Push(name);
	}
	public override void Pop(JsonDataObject Value_)
	{
		Value_.Push("EnumType", EnumType);
		Value_.Push("name", name);
	}
	public void Set(STest2 Obj_)
	{
		EnumType = Obj_.EnumType;
		name = Obj_.name;
	}
	public override string StdName()
	{
		return 
			"EEnumType" + "," + 
			SEnumChecker.GetStdName(name);
	}
	public override string MemberName()
	{
		return 
			SEnumChecker.GetMemberName(EnumType, "EnumType") + "," + 
			SEnumChecker.GetMemberName(name, "name");
	}
}
