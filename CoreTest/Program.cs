using rso.core;
using System;
using System.Collections.Generic;
using System.Globalization;
using TTeamCnt = System.SByte;
using TPoses = System.Collections.Generic.List<SPoint>;

public class SPoint : SProto
{
    public Single X = default(Single);
    public Single Y = default(Single);
    public SPoint()
    {
    }
    public SPoint(SPoint Obj_)
    {
        X = Obj_.X;
        Y = Obj_.Y;
    }
    public SPoint(Single X_, Single Y_)
    {
        X = X_;
        Y = Y_;
    }
    public override void Push(CStream Stream_)
    {
        Stream_.Pop(ref X);
        Stream_.Pop(ref Y);
    }
    public override void Push(JsonDataObject Value_)
    {
        Value_.Pop("X", ref X);
        Value_.Pop("Y", ref Y);
    }
    public override void Pop(CStream Stream_)
    {
        Stream_.Push(X);
        Stream_.Push(Y);
    }
    public override void Pop(JsonDataObject Value_)
    {
        Value_.Push("X", X);
        Value_.Push("Y", Y);
    }
    public void Set(SPoint Obj_)
    {
        X = Obj_.X;
        Y = Obj_.Y;
    }
    public override string StdName()
    {
        return
            SEnumChecker.GetStdName(X) + "," +
            SEnumChecker.GetStdName(Y);
    }
    public override string MemberName()
    {
        return
            SEnumChecker.GetMemberName(X, "X") + "," +
            SEnumChecker.GetMemberName(Y, "Y");
    }
}

namespace CoreTest
{
    public class CTest
    {
    }
    public struct STest
    {
    }
    class Program
    {
        static void PushTest<T>(T Value_)
        {
            if (Value_.GetType().IsEnum)
            {
                var tc = Type.GetTypeCode(typeof(T));

                switch (Type.GetTypeCode(Enum.GetUnderlyingType(typeof(T))))
                {
                    case TypeCode.SByte:
                        PushTest((SByte)(object)Value_);
                        break;

                    case TypeCode.Byte:
                        PushTest((Byte)(object)Value_);
                        break;

                    case TypeCode.Int16:
                        PushTest((Int16)(object)Value_);
                        break;

                    case TypeCode.UInt16:
                        PushTest((UInt16)(object)Value_);
                        break;

                    case TypeCode.Int32:
                        PushTest((Int32)(object)Value_);
                        break;

                    case TypeCode.UInt32:
                        PushTest((UInt32)(object)Value_);
                        break;

                    case TypeCode.Int64:
                        PushTest((Int64)(object)Value_);
                        break;

                    case TypeCode.UInt64:
                        PushTest((UInt64)(object)Value_);
                        break;

                    default:
                        throw new Exception("Invalid Enum Type");
                }
            }
        }
        static void PushTest(SByte Value_)
        {
            Console.WriteLine(Value_.GetType().Name);
        }
        static void PushTest(Byte Value_)
        {
            Console.WriteLine(Value_.GetType().Name);
        }
        static void PushTest(Int16 Value_)
        {
            Console.WriteLine(Value_.GetType().Name);
        }
        static void PushTest(UInt16 Value_)
        {
            Console.WriteLine(Value_.GetType().Name);
        }
        static void PushTest(Int32 Value_)
        {
            Console.WriteLine(Value_.GetType().Name);
        }
        static void PushTest(UInt32 Value_)
        {
            Console.WriteLine(Value_.GetType().Name);
        }
        static void PushTest(Int64 Value_)
        {
            Console.WriteLine(Value_.GetType().Name);
        }
        static void PushTest(UInt64 Value_)
        {
            Console.WriteLine(Value_.GetType().Name);
        }
        enum ETest
        {
            A,
            B,
            C
        }
//        Stream_.Push((SByte) Enum.Parse(EnumType, ValueName_, false));

        static void Test<T>(ref T data)
        {
            Console.WriteLine(typeof(T).ToString());

        }
        public class SPoint : SProto
        {
            public Single X = default(Single);
            public Single Y = default(Single);
            public SPoint()
            {
            }
            public SPoint(SPoint Obj_)
            {
                X = Obj_.X;
                Y = Obj_.Y;
            }
            public SPoint(Single X_, Single Y_)
            {
                X = X_;
                Y = Y_;
            }
            public override void Push(CStream Stream_)
            {
                Stream_.Pop(ref X);
                Stream_.Pop(ref Y);
            }
            public override void Push(JsonDataObject Value_)
            {
                Value_.Pop("X", ref X);
                Value_.Pop("Y", ref Y);
            }
            public override void Pop(CStream Stream_)
            {
                Stream_.Push(X);
                Stream_.Push(Y);
            }
            public override void Pop(JsonDataObject Value_)
            {
                Value_.Push("X", X);
                Value_.Push("Y", Y);
            }
            public void Set(SPoint Obj_)
            {
                X = Obj_.X;
                Y = Obj_.Y;
            }
            public override string StdName()
            {
                return
                    SEnumChecker.GetStdName(X) + "," +
                    SEnumChecker.GetStdName(Y);
            }
            public override string MemberName()
            {
                return
                    SEnumChecker.GetMemberName(X, "X") + "," +
                    SEnumChecker.GetMemberName(Y, "Y");
            }
        }
        public class SEngineRect : SProto
        {
            public SPoint Size = new SPoint();
            public SPoint Offset = new SPoint();
            public SPoint Scale = new SPoint();
            public SEngineRect()
            {
            }
            public SEngineRect(SEngineRect Obj_)
            {
                Size = Obj_.Size;
                Offset = Obj_.Offset;
                Scale = Obj_.Scale;
            }
            public SEngineRect(SPoint Size_, SPoint Offset_, SPoint Scale_)
            {
                Size = Size_;
                Offset = Offset_;
                Scale = Scale_;
            }
            public override void Push(CStream Stream_)
            {
                Stream_.Pop(ref Size);
                Stream_.Pop(ref Offset);
                Stream_.Pop(ref Scale);
            }
            public override void Push(JsonDataObject Value_)
            {
                Value_.Pop("Size", ref Size);
                Value_.Pop("Offset", ref Offset);
                Value_.Pop("Scale", ref Scale);
            }
            public override void Pop(CStream Stream_)
            {
                Stream_.Push(Size);
                Stream_.Push(Offset);
                Stream_.Push(Scale);
            }
            public override void Pop(JsonDataObject Value_)
            {
                Value_.Push("Size", Size);
                Value_.Push("Offset", Offset);
                Value_.Push("Scale", Scale);
            }
            public void Set(SEngineRect Obj_)
            {
                Size.Set(Obj_.Size);
                Offset.Set(Obj_.Offset);
                Scale.Set(Obj_.Scale);
            }
            public override string StdName()
            {
                return
                    SEnumChecker.GetStdName(Size) + "," +
                    SEnumChecker.GetStdName(Offset) + "," +
                    SEnumChecker.GetStdName(Scale);
            }
            public override string MemberName()
            {
                return
                    SEnumChecker.GetMemberName(Size, "Size") + "," +
                    SEnumChecker.GetMemberName(Offset, "Offset") + "," +
                    SEnumChecker.GetMemberName(Scale, "Scale");
            }
        }
        public class SStructure : SEngineRect
        {
            public SPoint LocalPosition = new SPoint();
            public SStructure()
            {
            }
            public SStructure(SStructure Obj_) : base(Obj_)
            {
                LocalPosition = Obj_.LocalPosition;
            }
            public SStructure(SEngineRect Super_, SPoint LocalPosition_) : base(Super_)
            {
                LocalPosition = LocalPosition_;
            }
            public override void Push(CStream Stream_)
            {
                base.Push(Stream_);
                Stream_.Pop(ref LocalPosition);
            }
            public override void Push(JsonDataObject Value_)
            {
                base.Push(Value_);
                Value_.Pop("LocalPosition", ref LocalPosition);
            }
            public override void Pop(CStream Stream_)
            {
                base.Pop(Stream_);
                Stream_.Push(LocalPosition);
            }
            public override void Pop(JsonDataObject Value_)
            {
                base.Pop(Value_);
                Value_.Push("LocalPosition", LocalPosition);
            }
            public void Set(SStructure Obj_)
            {
                base.Set(Obj_);
                LocalPosition.Set(Obj_.LocalPosition);
            }
            public override string StdName()
            {
                return
                    base.StdName() + "," +
                    SEnumChecker.GetStdName(LocalPosition);
            }
            public override string MemberName()
            {
                return
                    base.MemberName() + "," +
                    SEnumChecker.GetMemberName(LocalPosition, "LocalPosition");
            }
        }
        public class SPlayerPos : SProto
        {
            public TPoses Poses = new TPoses();
            public SPlayerPos()
            {
            }
            public SPlayerPos(SPlayerPos Obj_)
            {
                Poses = Obj_.Poses;
            }
            public SPlayerPos(TPoses Poses_)
            {
                Poses = Poses_;
            }
            public override void Push(CStream Stream_)
            {
                Stream_.Pop(ref Poses);
            }
            public override void Push(JsonDataObject Value_)
            {
                Value_.Pop("Poses", ref Poses);
            }
            public override void Pop(CStream Stream_)
            {
                Stream_.Push(Poses);
            }
            public override void Pop(JsonDataObject Value_)
            {
                Value_.Push("Poses", Poses);
            }
            public void Set(SPlayerPos Obj_)
            {
                Poses = Obj_.Poses;
            }
            public override string StdName()
            {
                return
                    SEnumChecker.GetStdName(Poses);
            }
            public override string MemberName()
            {
                return
                    SEnumChecker.GetMemberName(Poses, "Poses");
            }
        }
        public class SStructureMove : SProto
        {
            public List<SEngineRect> Colliders = new List<SEngineRect>();
            public SPoint BeginPos = new SPoint();
            public SPoint EndPos = new SPoint();
            public Single Velocity = default(Single);
            public Single Delay = default(Single);
            public SStructureMove()
            {
            }
            public SStructureMove(SStructureMove Obj_)
            {
                Colliders = Obj_.Colliders;
                BeginPos = Obj_.BeginPos;
                EndPos = Obj_.EndPos;
                Velocity = Obj_.Velocity;
                Delay = Obj_.Delay;
            }
            public SStructureMove(List<SEngineRect> Colliders_, SPoint BeginPos_, SPoint EndPos_, Single Velocity_, Single Delay_)
            {
                Colliders = Colliders_;
                BeginPos = BeginPos_;
                EndPos = EndPos_;
                Velocity = Velocity_;
                Delay = Delay_;
            }
            public override void Push(CStream Stream_)
            {
                Stream_.Pop(ref Colliders);
                Stream_.Pop(ref BeginPos);
                Stream_.Pop(ref EndPos);
                Stream_.Pop(ref Velocity);
                Stream_.Pop(ref Delay);
            }
            public override void Push(JsonDataObject Value_)
            {
                Value_.Pop("Colliders", ref Colliders);
                Value_.Pop("BeginPos", ref BeginPos);
                Value_.Pop("EndPos", ref EndPos);
                Value_.Pop("Velocity", ref Velocity);
                Value_.Pop("Delay", ref Delay);
            }
            public override void Pop(CStream Stream_)
            {
                Stream_.Push(Colliders);
                Stream_.Push(BeginPos);
                Stream_.Push(EndPos);
                Stream_.Push(Velocity);
                Stream_.Push(Delay);
            }
            public override void Pop(JsonDataObject Value_)
            {
                Value_.Push("Colliders", Colliders);
                Value_.Push("BeginPos", BeginPos);
                Value_.Push("EndPos", EndPos);
                Value_.Push("Velocity", Velocity);
                Value_.Push("Delay", Delay);
            }
            public void Set(SStructureMove Obj_)
            {
                Colliders = Obj_.Colliders;
                BeginPos.Set(Obj_.BeginPos);
                EndPos.Set(Obj_.EndPos);
                Velocity = Obj_.Velocity;
                Delay = Obj_.Delay;
            }
            public override string StdName()
            {
                return
                    SEnumChecker.GetStdName(Colliders) + "," +
                    SEnumChecker.GetStdName(BeginPos) + "," +
                    SEnumChecker.GetStdName(EndPos) + "," +
                    SEnumChecker.GetStdName(Velocity) + "," +
                    SEnumChecker.GetStdName(Delay);
            }
            public override string MemberName()
            {
                return
                    SEnumChecker.GetMemberName(Colliders, "Colliders") + "," +
                    SEnumChecker.GetMemberName(BeginPos, "BeginPos") + "," +
                    SEnumChecker.GetMemberName(EndPos, "EndPos") + "," +
                    SEnumChecker.GetMemberName(Velocity, "Velocity") + "," +
                    SEnumChecker.GetMemberName(Delay, "Delay");
            }
        }
        public class SMapMulti : SProto
        {
            public String PrefabName = string.Empty;
            public Dictionary<TTeamCnt, SPlayerPos> PlayerPoses = new Dictionary<TTeamCnt, SPlayerPos>();
            public SPoint PropPosition = new SPoint();
            public List<SStructure> Structures = new List<SStructure>();
            public List<SStructureMove> StructureMoves = new List<SStructureMove>();
            public SMapMulti()
            {
            }
            public SMapMulti(SMapMulti Obj_)
            {
                PrefabName = Obj_.PrefabName;
                PlayerPoses = Obj_.PlayerPoses;
                PropPosition = Obj_.PropPosition;
                Structures = Obj_.Structures;
                StructureMoves = Obj_.StructureMoves;
            }
            public SMapMulti(String PrefabName_, Dictionary<TTeamCnt, SPlayerPos> PlayerPoses_, SPoint PropPosition_, List<SStructure> Structures_, List<SStructureMove> StructureMoves_)
            {
                PrefabName = PrefabName_;
                PlayerPoses = PlayerPoses_;
                PropPosition = PropPosition_;
                Structures = Structures_;
                StructureMoves = StructureMoves_;
            }
            public override void Push(CStream Stream_)
            {
                Stream_.Pop(ref PrefabName);
                Stream_.Pop(ref PlayerPoses);
                Stream_.Pop(ref PropPosition);
                Stream_.Pop(ref Structures);
                Stream_.Pop(ref StructureMoves);
            }
            public override void Push(JsonDataObject Value_)
            {
                Value_.Pop("PrefabName", ref PrefabName);
                Value_.Pop("PlayerPoses", ref PlayerPoses);
                Value_.Pop("PropPosition", ref PropPosition);
                Value_.Pop("Structures", ref Structures);
                Value_.Pop("StructureMoves", ref StructureMoves);
            }
            public override void Pop(CStream Stream_)
            {
                Stream_.Push(PrefabName);
                Stream_.Push(PlayerPoses);
                Stream_.Push(PropPosition);
                Stream_.Push(Structures);
                Stream_.Push(StructureMoves);
            }
            public override void Pop(JsonDataObject Value_)
            {
                Value_.Push("PrefabName", PrefabName);
                Value_.Push("PlayerPoses", PlayerPoses);
                Value_.Push("PropPosition", PropPosition);
                Value_.Push("Structures", Structures);
                Value_.Push("StructureMoves", StructureMoves);
            }
            public void Set(SMapMulti Obj_)
            {
                PrefabName = Obj_.PrefabName;
                PlayerPoses = Obj_.PlayerPoses;
                PropPosition.Set(Obj_.PropPosition);
                Structures = Obj_.Structures;
                StructureMoves = Obj_.StructureMoves;
            }
            public override string StdName()
            {
                return
                    SEnumChecker.GetStdName(PrefabName) + "," +
                    SEnumChecker.GetStdName(PlayerPoses) + "," +
                    SEnumChecker.GetStdName(PropPosition) + "," +
                    SEnumChecker.GetStdName(Structures) + "," +
                    SEnumChecker.GetStdName(StructureMoves);
            }
            public override string MemberName()
            {
                return
                    SEnumChecker.GetMemberName(PrefabName, "PrefabName") + "," +
                    SEnumChecker.GetMemberName(PlayerPoses, "PlayerPoses") + "," +
                    SEnumChecker.GetMemberName(PropPosition, "PropPosition") + "," +
                    SEnumChecker.GetMemberName(Structures, "Structures") + "," +
                    SEnumChecker.GetMemberName(StructureMoves, "StructureMoves");
            }
        }
        //static void Pop<T>(T t)
        //{
        //    Console.WriteLine(typeof(T).ToString());
        //}
        //static void Pop<T>(List<T> t)
        //{
        //    Console.WriteLine("List");
        //}
        //static void Pop<T>(T[] t)
        //{
        //    Console.WriteLine("Array");
        //}
        static void Main(string[] args)
        {
            {
                var stream = new CStream();
                stream.Push(Hours.zero);
            }
            return;

#if true


#elif true // Json


            var jsonObjectCollection = (JsonDataObject)JsonParser.Parse("{\"a\" : FalSe}");

            Console.WriteLine(jsonObjectCollection["a"].GetBool());

            //foreach (var i in jsonObjectCollection)
            //{
            //    var a = (JsonDataArray)i;
            //    foreach (var jj in a)
            //    {
            //        var n = (JsonDataNumber)jj;
            //        var Number = n.GetUInt32();
            //        Console.WriteLine(Number);
            //    }
            //}
            return;

            string JsonStr = @"

[3],
[4]

";

        var j = JsonParser.Parse(JsonStr);
        Console.WriteLine(j.ToString());

//            string JsonStr = @"{
//    """" : """",
//  ""boolName"" : true,
//  ""boolName2"" : True,
//  ""boolName3"" : false,
//  ""boolName4"" : False,
//    ""name"" : ""string: ,@'  t value"",
//    ""name2"":12.34 ,
//    ""name3"" :5667,
//    ""objname"" :{},
//    ""array"": [123,2234,""ok"",false,true,{},[]]
//}";

//            var j = JsonParser.Parse(JsonStr);

            // 중첩 컨테이너 가능할 때까지 아래 테스트 불가.
            //var pa = new SPoint[2];
            //for (int i = 0; i < pa.Length; ++i)
            //    pa[i] = new SPoint(1, 2);
            //j.Push("PointArray", pa);

            //var sa = new string[2];
            //for (int i = 0; i < sa.Length; ++i)
            //    sa[i] = i.ToString();
            //j.Push("StringArray", sa);

            //var sl = new List<string>();
            //for (int i = 0; i < 2; ++i)
            //    sl.Add(i.ToString());
            //j.Push("StringList", sl);

            //var hs = new HashSet<string>();
            //for (int i = 0; i < 2; ++i)
            //    hs.Add(i.ToString());
            //j.Push("StringHashSet", hs);

            //var mss = new MultiSet<string>();
            //for (int i = 0; i < 2; ++i)
            //    mss.Add(i.ToString());
            //j.Push("StringMultiSet", mss);

            //var pd = new Dictionary<string, SPoint>();
            //for (int i = 0; i < 2; ++i)
            //    pd.Add(i.ToString(), new SPoint(1, 1));
            //j.Push("PointDictionary", pd);

            //var pmm = new CMultiMap<string, SPoint>();
            //for (int i = 0; i < 2; ++i)
            //    pmm.Add(i.ToString(), new SPoint(i, i));
            //j.Push("PointMultiMap", pmm);

            //var l = new List<int>();
            //l.Add(1000);
            //l.Add(2000);
            //j.Push("ListList", l);

            //var dd = new Dictionary<int, int>();
            //dd.Add(1000, 1000);
            //dd.Add(2000, 2000);
            //var dhs = new HashSet<Dictionary<int, int>>();
            //for (int i = 0; i < 2; ++i)
            //    dhs.Add(dd);
            //j.Push("DictionaryHashSet", dhs);

//            Console.WriteLine(j.ToString());

#elif false // MultiMap
            int v;

            Console.WriteLine("MultiSet");
            var ms = new MultiSet<int>();
            ms.Add(3);
            ms.Add(3);
            ms.Add(3);
            ms.Add(2);
            ms.Add(4);
            Console.WriteLine("Count : " + ms.Count);
            ms.RemoveLast();
            ms.RemoveLast();
            ms.RemoveLast();
            ms.RemoveLast();

            Console.WriteLine("All Datas");
            foreach (var i in ms)
                Console.WriteLine(i);

            Console.WriteLine("MultiMap");
            var mm = new MultiMap<int, int>();
            mm.Add(3, 4);
            mm.Add(3, 5);
            mm.Add(3, 6);
            mm.Add(2, 2);

            Console.WriteLine("All Datas : " + mm.Count);
            foreach (var i in mm)
                Console.WriteLine(i);

            Console.WriteLine("ToArray");
            var a = mm.ToArray(3);
            foreach (var i in a)
                Console.WriteLine(i);

            var it = mm.First();
            Console.WriteLine("First");
            Console.WriteLine(it);
            mm.RemoveFirst();
            Console.WriteLine("First Removed Count : " + mm.Count);
            foreach (var i in mm)
                Console.WriteLine(i);

#elif true // Resize

            var l = new List<int>();
            l.Resize(4);
#endif
        }
    }
}
