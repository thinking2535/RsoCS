using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace rso
{
    namespace core
    {
        public class CStream
        {
            int _Head = 0;
            int _Tail = 0;
            int _SavedHead = 0;
            int _SavedTail = 0;
            CVector<byte> _Bytes = new CVector<byte>();
            public CStream()
            {
            }
            public CStream(SProto Proto_)
            {
                Push(Proto_);
            }
            public CStream(string FileName_)
            {
                LoadFile(FileName_);
            }
            public CStream(byte[] Data_)
            {
                Push(Data_);
            }
            ~CStream()
            {
                Dispose();
            }
            public void Dispose()
            {
                Clear();
            }
            public override string ToString()
            {
                return Encoding.Unicode.GetString(_Bytes.Data, _Head, Size - 2);
            }
            public CStream Pop(ref bool Value_)
            {
                if (_Bytes.Size - _Head < 1)
                    throw new Exception("Pop Fail Invalid Parameter");

                Value_ = Convert.ToBoolean(_Bytes[_Head]);

                if (Size == 1)
                    Clear();
                else
                    ++_Head;

                return this;
            }
            public CStream Pop(ref sbyte Value_)
            {
                if (Size < 1)
                    throw new Exception("Pop Fail Invalid Parameter");

                Value_ = (sbyte)_Bytes[_Head];

                if (Size == 1)
                    Clear();
                else
                    ++_Head;

                return this;
            }
            public CStream Pop(ref byte Value_)
            {
                if (Size < 1)
                    throw new Exception("Pop Fail Invalid Parameter");

                Value_ = _Bytes[_Head];

                if (Size == 1)
                    Clear();
                else
                    ++_Head;

                return this;
            }
            public CStream Pop(ref short Value_)
            {
                if (Size < 2)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(_Bytes.Data, _Head, 2);
                    Value_ = BitConverter.ToInt16(_Bytes.Data, _Head);
                }
                else
                {
                    Value_ = BitConverter.ToInt16(_Bytes.Data, _Head);
                }

                if (Size == 2)
                    Clear();
                else
                    _Head += 2;

                return this;
            }
            public CStream Pop(ref ushort Value_)
            {
                if (Size < 2)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(_Bytes.Data, _Head, 2);
                    Value_ = BitConverter.ToUInt16(_Bytes.Data, _Head);
                }
                else
                {
                    Value_ = BitConverter.ToUInt16(_Bytes.Data, _Head);
                }

                if (Size == 2)
                    Clear();
                else
                    _Head += 2;

                return this;
            }
            public CStream Pop(ref int Value_)
            {
                if (Size < 4)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(_Bytes.Data, _Head, 4);
                    Value_ = BitConverter.ToInt32(_Bytes.Data, _Head);
                }
                else
                {
                    Value_ = BitConverter.ToInt32(_Bytes.Data, _Head);
                }

                if (Size == 4)
                    Clear();
                else
                    _Head += 4;

                return this;
            }
            public CStream Pop(ref uint Value_)
            {
                if (Size < 4)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(_Bytes.Data, _Head, 4);
                    Value_ = BitConverter.ToUInt32(_Bytes.Data, _Head);
                }
                else
                {
                    Value_ = BitConverter.ToUInt32(_Bytes.Data, _Head);
                }

                if (Size == 4)
                    Clear();
                else
                    _Head += 4;

                return this;
            }
            public CStream Pop(ref long Value_)
            {
                if (Size < 8)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(_Bytes.Data, _Head, 8);
                    Value_ = BitConverter.ToInt64(_Bytes.Data, _Head);
                }
                else
                {
                    Value_ = BitConverter.ToInt64(_Bytes.Data, _Head);
                }

                if (Size == 8)
                    Clear();
                else
                    _Head += 8;

                return this;
            }
            public CStream Pop(ref ulong Value_)
            {
                if (Size < 8)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(_Bytes.Data, _Head, 8);
                    Value_ = BitConverter.ToUInt64(_Bytes.Data, _Head);
                }
                else
                {
                    Value_ = BitConverter.ToUInt64(_Bytes.Data, _Head);
                }

                if (Size == 8)
                    Clear();
                else
                    _Head += 8;

                return this;
            }
            public CStream Pop(ref float Value_)
            {
                if (Size < 4)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(_Bytes.Data, _Head, 4);
                    Value_ = BitConverter.ToSingle(_Bytes.Data, _Head);
                }
                else
                {
                    Value_ = BitConverter.ToSingle(_Bytes.Data, _Head);
                }

                if (Size == 4)
                    Clear();
                else
                    _Head += 4;

                return this;
            }
            public CStream Pop(ref double Value_)
            {
                if (Size < 8)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(_Bytes.Data, _Head, 8);
                    Value_ = BitConverter.ToDouble(_Bytes.Data, _Head);
                }
                else
                {
                    Value_ = BitConverter.ToDouble(_Bytes.Data, _Head);
                }

                if (Size == 8)
                    Clear();
                else
                    _Head += 8;

                return this;
            }
            public CStream Pop(ref string Value_)
            {
                int StrSize = 0; // Not Lenth
                Pop(ref StrSize);
                Value_ = Encoding.Unicode.GetString(_Bytes.Data, _Head, StrSize);
                Pop(StrSize);

                return this;
            }
            public CStream Pop(TimePoint Value_)
            {
                Pop(ref Value_.Ticks);

                return this;
            }
            public CStream Pop(ref DateTime Value_)
            {
                short year = 0;
                ushort month = 0;
                ushort day = 0;
                ushort hour = 0;
                ushort min = 0;
                ushort sec = 0;
                uint fraction = 0;

                Pop(ref year);
                Pop(ref month);
                Pop(ref day);
                Pop(ref hour);
                Pop(ref min);
                Pop(ref sec);
                Pop(ref fraction);

                Value_ = new DateTime(year, month, day, hour, min, sec);

                return this;
            }
            public void Pop(int Size_)
            {
                if (Size_ < Size)
                    _Head += Size_;
                else
                    Clear();
            }
            public CStream Pop(CStream Stream_)
            {
                int StreamSize = 0;
                Pop(ref StreamSize);
                Stream_.Push(_Bytes.Data, _Head, StreamSize);
                Pop(StreamSize);
                return this;
            }
            public CStream Push(byte Value_)
            {
                if (_Tail + 1 > _Bytes.Size)
                    _Bytes.PushBack(Value_);
                else
                    _Bytes[_Tail] = Value_;

                ++_Tail;

                return this;
            }
            public CStream Push(bool Value_)
            {
                Push(Convert.ToByte(Value_));

                return this;
            }
            public CStream Push(sbyte Value_)
            {
                Push((byte)Value_);

                return this;
            }
            public CStream Push(short Value_)
            {
                byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(ushort Value_)
            {
                byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(int Value_)
            {
                byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(uint Value_)
            {
                byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(long Value_)
            {
                byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(ulong Value_)
            {
                byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(float Value_)
            {
                byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(double Value_)
            {
                byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(string Value_)
            {
                Push((int)(Value_.Length * 2));
                Push(Encoding.Unicode.GetBytes(Value_));

                return this;
            }
            public CStream Push(TimePoint Value_)
            {
                Push(Value_.Ticks);

                return this;
            }
            public CStream Push(DateTime Value_)
            {
                Push((short)Value_.Year);
                Push((ushort)Value_.Month);
                Push((ushort)Value_.Day);
                Push((ushort)Value_.Hour);
                Push((ushort)Value_.Minute);
                Push((ushort)Value_.Second);
                Push((uint)0);

                return this;
            }
            public void Push(byte[] Data_, int Index_, int Length_)
            {
                if (Length_ <= 0)
                    return;

                if (_Tail + Length_ > _Bytes.Size)
                    _Bytes.Resize(_Tail + Length_);

                Array.Copy(Data_, Index_, _Bytes.Data, _Tail, Length_);

                _Tail += Length_;
            }
            public CStream Push(byte[] Data_)
            {
                Push(Data_, 0, Data_.Length);

                return this;
            }
            public CStream Push(CStream Stream_)
            {
                Push((int)Stream_.Size);
                Push(Stream_._Bytes.Data, Stream_._Head, Stream_._Tail);

                return this;
            }
            public int Head
            {
                get
                {
                    return _Head;
                }
            }
            public int Tail
            {
                get
                {
                    return _Tail;
                }
                set
                {
                    if (value > _Bytes.Size)
                        throw (new Exception("Invalid Stream Tail"));

                    _Tail = value;
                }
            }
            public byte[] Data
            {
                get
                {
                    return _Bytes.Data;
                }
            }
            public int Size
            {
                get
                {
                    return (_Tail - _Head);
                }
            }
            void _SetAtStream(int Index_, byte[] Data_)
            {
                for (int i = 0; i < Data_.Length; ++i)
                    _Bytes[Index_ + i] = Data_[i];
            }
            public void SetAt(int Index_, byte Value_)
            {
                _Bytes[Index_] = Value_;
            }
            public void SetAt(int Index_, int Value_)
            {
                byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                _SetAtStream(Index_, Bytes);
            }
            public void SetAt(int Index_, ulong Value_)
            {
                byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                _SetAtStream(Index_, Bytes);
            }
            public void LoadFile(string FileName_)
            {
                Clear();
                Push(File.ReadAllBytes(FileName_));
            }
            public void SaveFile(string FileName_)
            {
                File.WriteAllBytes(FileName_, _Bytes.Data);
            }
            public void LoadState()
            {
                _Head = _SavedHead;
                _Tail = _SavedTail;
            }
            public void SaveState()
            {
                _SavedHead = _Head;
                _SavedTail = _Tail;
            }
            public void Clear()
            {
                _Head = _Tail = 0;
            }
            public CStream Push<T>(T Data_)
            {
                // switch 말고 다른 방법은 없는가?
                switch (typeof(T).ToString())
                {
                    case "System.bool":
                        Push((bool)(object)Data_);
                        break;
                    case "System.sbyte":
                        Push((sbyte)(object)Data_);
                        break;
                    case "System.byte":
                        Push((byte)(object)Data_);
                        break;
                    case "System.short":
                        Push((short)(object)Data_);
                        break;
                    case "System.ushort":
                        Push((ushort)(object)Data_);
                        break;
                    case "System.int":
                        Push((int)(object)Data_);
                        break;
                    case "System.uint":
                        Push((uint)(object)Data_);
                        break;
                    case "System.long":
                        Push((long)(object)Data_);
                        break;
                    case "System.ulong":
                        Push((ulong)(object)Data_);
                        break;
                    case "System.Single":
                        Push((float)(object)Data_);
                        break;
                    case "System.Double":
                        Push((double)(object)Data_);
                        break;
                    case "System.String":
                        Push((string)(object)Data_);
                        break;
                    case "rso.core.TimePoint":
                        Push((TimePoint)(object)Data_);
                        break;
                    case "System.DateTime":
                        Push((DateTime)(object)Data_);
                        break;
                    case "rso.core.CStream":
                        Push((CStream)(object)Data_);
                        break;
                    default:
                        ((SProto)(object)Data_).Pop(this);
                        break;
                }

                return this;
            }
            public CStream Pop<T>(ref T Data_)
            {
                // switch 말고 다른 방법은 없는가?
                switch (typeof(T).ToString())
                {
                    case "System.bool":
                        {
                            bool Data = false;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.sbyte":
                        {
                            sbyte Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.byte":
                        {
                            byte Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.short":
                        {
                            short Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.ushort":
                        {
                            ushort Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.int":
                        {
                            int Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.uint":
                        {
                            uint Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.long":
                        {
                            long Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.ulong":
                        {
                            ulong Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.Single":
                        {
                            float Data = 0.0f;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.Double":
                        {
                            double Data = 0.0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    // string 은 new 불가로 제네릭 함수에서 처리 불가
                    //case "System.String":
                    //{
                    //    string Data = "";
                    //    Pop(ref Data);
                    //    Data_ = (T)(object)Data;
                    //}
                    //break;
                    case "rso.core.TimePoint":
                        Pop((TimePoint)(object)Data_);
                        break;
                    case "System.DateTime":
                        {
                            var Data = (DateTime)(object)Data_;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "rso.core.CStream":
                        Pop((CStream)(object)Data_);
                        break;
                    default:
                        ((SProto)(object)Data_).Push(this);
                        break;
                }

                return this;
            }
            public CStream Push<TValue>(TValue[] Data_)
            {
                foreach (var i in Data_)
                    Push(i);

                return this;
            }
            public CStream Pop(string[] Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                for (int i = 0; i < Data_.Length; ++i)
                    Pop(ref Data_[i]);

                return this;
            }
            public CStream Pop<TValue>(TValue[] Data_) where TValue : new()
            {
                for (int i = 0; i < Data_.Length; ++i)
                    Pop(ref Data_[i]);

                return this;
            }
            public CStream Push<TValue>(List<TValue> Data_)
            {
                Push(Data_.Count);
                foreach (var i in Data_)
                    Push(i);

                return this;
            }
            public CStream Pop(List<string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    string Value = "";
                    Pop(ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public CStream Pop<TValue>(List<TValue> Data_) where TValue : new()
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    var Value = new TValue();
                    Pop(ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public CStream Push<TValue>(HashSet<TValue> Data_)
            {
                Push(Data_.Count);
                foreach (var i in Data_)
                    Push(i);

                return this;
            }
            public CStream Pop(HashSet<string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    string Value = "";
                    Pop(ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public CStream Pop<TValue>(HashSet<TValue> Data_) where TValue : new()
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    var Value = new TValue();
                    Pop(ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public CStream Push<TKey, TValue>(KeyValuePair<TKey, TValue> Data_)
            {
                Push(Data_.Key);
                Push(Data_.Value);

                return this;
            }
            public CStream Push<TKey, TValue>(Dictionary<TKey, TValue> Data_)
            {
                Push(Data_.Count);
                foreach (var i in Data_)
                    Push(i);

                return this;
            }
            public CStream Pop(Dictionary<string, string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    string Key = "";
                    string Value = "";
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TValue>(Dictionary<string, TValue> Data_) where TValue : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    string Key = "";
                    var Value = new TValue();
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TKey>(Dictionary<TKey, string> Data_) where TKey : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    var Key = new TKey();
                    string Value = "";
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TKey, TValue>(Dictionary<TKey, TValue> Data_) where TKey : new() where TValue : new()
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    var Key = new TKey();
                    var Value = new TValue();
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Push<TKey>(CMultiSet<TKey> Data_)
            {
                Push(Data_.Count);
                foreach (var i in Data_)
                    Push(i);

                return this;
            }
            public CStream Pop(CMultiSet<string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    string Key = "";
                    Pop(ref Key);
                    Data_.Add(Key);
                }

                return this;
            }
            public CStream Pop<TKey>(CMultiSet<TKey> Data_) where TKey : new() // Pop<TKey> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    var Key = new TKey();
                    Pop(ref Key);
                    Data_.Add(Key);
                }

                return this;
            }
            public CStream Push<TKey, TValue>(CMultiMap<TKey, TValue> Data_)
            {
                Push(Data_.Count);
                foreach (var i in Data_)
                    Push(i);

                return this;
            }
            public CStream Pop(CMultiMap<string, string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    string Key = "";
                    string Value = "";
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TValue>(CMultiMap<string, TValue> Data_) where TValue : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    string Key = "";
                    var Value = new TValue();
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TKey>(CMultiMap<TKey, string> Data_) where TKey : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    var Key = new TKey();
                    string Value = "";
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TKey, TValue>(CMultiMap<TKey, TValue> Data_) where TKey : new() where TValue : new()
            {
                int Cnt = 0;
                Pop(ref Cnt);
                for (int i = 0; i < Cnt; ++i)
                {
                    var Key = new TKey();
                    var Value = new TValue();
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
        }
    }
}