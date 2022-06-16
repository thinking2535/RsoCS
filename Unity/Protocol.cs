using System;
using System.Collections.Generic;
using rso.core;


namespace rso
{
	namespace unity
	{
		using rso.physics;
		public class SBoxCollider2D : STransform
		{
			public SRectCollider2D RectCollider2D = new SRectCollider2D();
			public SBoxCollider2D()
			{
			}
			public SBoxCollider2D(SBoxCollider2D Obj_) : base(Obj_)
			{
				RectCollider2D = Obj_.RectCollider2D;
			}
			public SBoxCollider2D(STransform Super_, SRectCollider2D RectCollider2D_) : base(Super_)
			{
				RectCollider2D = RectCollider2D_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref RectCollider2D);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("RectCollider2D", ref RectCollider2D);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(RectCollider2D);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("RectCollider2D", RectCollider2D);
			}
			public void Set(SBoxCollider2D Obj_)
			{
				base.Set(Obj_);
				RectCollider2D.Set(Obj_.RectCollider2D);
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(RectCollider2D);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(RectCollider2D, "RectCollider2D");
			}
		}
	}
}
