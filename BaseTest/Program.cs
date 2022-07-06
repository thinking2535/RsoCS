using System;
using rso.core;
using rso.Base;

namespace BaseTest
{
    class Program
    {
#if false
        static void Main(String[] args)
        {
            //var mm = new CMultiMap<int, CTest>();
            //mm.Add(1, new CTest());
            //mm.Add(1, new CTest());
            //mm.Clear();
            //mm.Add(1, new CTest());
            //mm.Add(1, new CTest());

            //Console.WriteLine(mm.Count);

            //foreach (var a in mm)
            //{
            //    Console.WriteLine(a.Key + " " + a.Value);
            //}

            // 1. BaseTest 폴더에 있는 Test.xlsx 편집
            //    - 첫번째 Column 의 첫번째 문자가 $ 이면 그 Row(행) 은 주석으로 간주
            //    - 주석이 아닌 첫번째 Row 는 Column 의 데이터 추출 옵션과 타입을 의미
            //     * 추출 옵션은 아무거나 유일한 이름을 사용하면 됨( 예: client )
            //      추출 옵션은 Exporter 로 추출시에 해당 이름을 가진 필드만 추출하기 위함

            //     * 데이터 타입
            //        i1  : 1바이트 int 형
            //        i2  : 2바이트 int 형
            //        i4  : 4바이트 int 형
            //        i8  : 8바이트 int 형
            //        s   : 문자열
            //        f   : Single 형
            //        d   : Double 형

            // 2. ExportClient.bat 실행 하여 엑셀파일 Export
            //    - 내부파일 편집 가능 ( 엑셀 파일 추가 삭제 )

            // 3. 프로그램에서 읽어들일 구조체 프로토콜 파일 작성
            //    - 확장자는 .prt 여야함
            //    - 예를 들면 BaseTest 폴더에 있는  Protocol.prt 를 작성
            //       네임스페이스, 구조체, typedef, 컨테이너 등 가능

            // 4. ProtoGen.bat 을 실행하여 프로토콜 파일을 cs 파일로 생성
            //     ( 예: Protocol.prt 를 참조하여 Protocol.cs 를 생성 )

            // 5. 아래 코드를 참고하여 만들어진 엑셀 Export 파일 ( 예: Test.dat )를
            //   CStream 클래스의 LoadFile 로 읽어들여
            //   생성된 Protocol.cs 파일내부에 정의된 구조체(예:SDataPack)에 로드하여 사용


            //Console.WriteLine("========프로토콜 생성 및 엑셀 로딩 테스트========");

            //var StreamList = new CStream();
            //if (!StreamList.LoadFile("Test.dat"))
            //    throw new Exception();

            //var DataPackList = new SDataPackList();

            //if (!StreamList.Pop(DataPackList))
            //    throw new Exception();

            //foreach (var Data in DataPackList.Datas)
            //{
            //    Console.WriteLine(Data.StringValue + " " + Data.IntValue);
            //}


            //var StreamSet = new CStream();
            //if (!StreamSet.LoadFile("Set.dat"))
            //    throw new Exception();

            //var DataPackSet = new SDataPackSet();

            //if (!StreamSet.Pop(DataPackSet))
            //    throw new Exception();

            //foreach (var Data in DataPackSet.Datas)
            //{
            //    Console.WriteLine(Data);
            //}


            //var StreamMap = new CStream();
            //if (!StreamMap.LoadFile("Test.dat"))
            //    throw new Exception();

            //var DataPackMap = new SDataPackMap();

            //if (!StreamMap.Pop(DataPackMap))
            //    throw new Exception();

            //foreach (var Data in DataPackMap.Datas)
            //{
            //    Console.WriteLine(Data.Key + " " + Data.Value);
            //}

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();


            //Console.WriteLine("========제이슨 테스트=========");


            //JsonTextParser Parser = new JsonTextParser();

            //// 정수
            //JsonObjectCollection SetupJson = (JsonObjectCollection)Parser.Parse(
            //    "{\"Var\" : 3}"
            //);
            //Console.WriteLine(
            //    SetupJson["Var"].GetValue()
            //    );

            //// 문자열
            //SetupJson = (JsonObjectCollection)Parser.Parse(
            //    "{\"Var\" : \"문자열\"}"
            //);
            //Console.WriteLine(
            //    SetupJson["Var"].GetValue()
            //    );

            //// 배열
            //SetupJson = (JsonObjectCollection)Parser.Parse(
            //    "{\"Var\" : [1,2,3]}"
            //);
            //Console.WriteLine(
            //    ((JsonArrayCollection)SetupJson["Var"])[0]
            //    );


            //Console.WriteLine("\n\n배열 foreach 활용");
            //SetupJson = (JsonObjectCollection)Parser.Parse(
            //    "{\"Var\" : [1,2,3]}"
            //);
            //foreach (var Val in ((JsonArrayCollection)SetupJson["Var"]))
            //Console.WriteLine(Val);



            //Console.WriteLine("\n\n오브젝트 배열");
            //SetupJson = (JsonObjectCollection)Parser.Parse(
            //    "{\"Var\" : [{\"a\":1},{\"b\":2}]}"
            //);
            //Console.WriteLine(
            //    ((JsonObjectCollection)((JsonArrayCollection)SetupJson["Var"])[0])["a"].GetValue() 
            //    );
            //Console.WriteLine(
            //    ((JsonObjectCollection)((JsonArrayCollection)SetupJson["Var"])[1])["b"].GetValue()
            //    );
        }
#elif true
        public class CTest
        {
        }
        static void Main(String[] args)
        {

            CList<CTest> l = new CList<CTest>();
            var it = l.Add(new CTest());
            var it2 = it;
            l.Remove(it2);

        }

#elif true
        public class SKey : SProto
        {
            public int PeerNum = 0;
            public int PeerCounter = 0;
            public SKey()
            {
            }
            public SKey(SKey Obj_)
            {
                PeerNum = Obj_.PeerNum;
                PeerCounter = Obj_.PeerCounter;
            }
            public SKey(int PeerNum_, int PeerCounter_)
            {
                PeerNum = PeerNum_;
                PeerCounter = PeerCounter_;
            }
            public override void Push(CStream Stream_)
            {
                Stream_.Pop(ref PeerNum);
                Stream_.Pop(ref PeerCounter);
            }
            public override void Push(JsonDataObject Value_)
            {
                Value_.Pop("PeerNum", ref PeerNum);
                Value_.Pop("PeerCounter", ref PeerCounter);
            }
            public override void Pop(CStream Stream_)
            {
                Stream_.Push(PeerNum);
                Stream_.Push(PeerCounter);
            }
            public override void Pop(JsonDataObject Value_)
            {
                Value_.Push("PeerNum", PeerNum);
                Value_.Push("PeerCounter", PeerCounter);
            }
            public override string StdName()
            {
                return "uint32,uint32";
            }
            public override string MemberName()
            {
                return "PeerNum,PeerCounter";
            }
        }
        static void Main(String[] args)
        {
            var Option = new COptionStream<SKey>("Option.ini", false);
            Option.Data.PeerNum = 'q';
            Option.Save();
            Console.WriteLine(Option.Data);
        }
#elif true
        static void Main(String[] args)
        {
        }
#elif true
        static void Main(String[] args)
        {
            string str = "SData";
            var ctype = Type.GetType(str);
            var ob = Activator.CreateInstance(ctype);
            Console.WriteLine(ob.GetType().ToString());
            var m = ctype.GetFields();
            for (int i = 0; i < m.Length; ++i)
            {
                m[i].SetValue(ob, 3);

                break;
            }

            IProto I = ob as IProto;
            var s = new CStream();
            s.Push(I);
            s.SaveFile("ok.bin");

            var s2 = new CStream("ok.bin");

            SData d2 = new SData();
            s2.Pop(d2);
            Console.WriteLine("ok");
        }

#endif
    }
}
