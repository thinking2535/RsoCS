using System;
using System.Collections.Generic;

namespace rso
{
    namespace core
    {
        public class JsonDataArray : JsonData
        {
            protected List<JsonData> _Array = new List<JsonData>();
            public int Count
            {
                get { return _Array.Count; }
            }
            public override string ToString(string Name_, string Indentation_)
            {
                string Str = Indentation_ + JsonGlobal.GetNameString(Name_) + "[\n";
                Indentation_ = Indentation_.PushIndentation();

                if (_Array.Count > 0)
                    Str += _Array[0].ToString("", Indentation_);

                for (int i = 1; i < _Array.Count; ++i)
                {
                    Str += ",\n";
                    Str += _Array[i].ToString("", Indentation_);
                }
                Str += "\n";

                Indentation_ = Indentation_.PopIndentation();
                Str += Indentation_;
                Str += "]";

                return Str;
            }
            protected JsonDataArray Push(bool Data_)
            {
                _Array.Add(new JsonDataBool(Data_));
                return this;
            }
            protected JsonDataArray Push(char Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            protected JsonDataArray Push(sbyte Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            protected JsonDataArray Push(byte Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            protected JsonDataArray Push(short Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            protected JsonDataArray Push(ushort Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            protected JsonDataArray Push(int Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            protected JsonDataArray Push(uint Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            protected JsonDataArray Push(long Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            protected JsonDataArray Push(ulong Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            protected JsonDataArray Push(float Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            protected JsonDataArray Push(double Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            protected JsonDataArray Push(string Data_)
            {
                _Array.Add(new JsonDataString(Data_));
                return this;
            }
            protected JsonDataArray Push(TimePoint Data_)
            {
                _Array.Add(new JsonDataNumber(Data_.Ticks));
                return this;
            }
            protected JsonDataArray Push(DateTime Data_)
            {
                _Array.Add(new JsonDataNumber(Data_.ToTimePoint().Ticks));
                return this;
            }
            protected JsonDataArray Push(CStream Data_)
            {
                _Array.Add(new JsonDataString(Data_.ToString()));
                return this;
            }
            public JsonDataArray Pop(int Index_, ref bool Data_)
            {
                Data_ = _Array[Index_].GetBool();
                return this;
            }
            public JsonDataArray Pop(int Index_, ref char Data_)
            {
                Data_ = _Array[Index_].GetChar();
                return this;
            }
            public JsonDataArray Pop(int Index_, ref sbyte Data_)
            {
                Data_ = _Array[Index_].GetSByte();
                return this;
            }
            public JsonDataArray Pop(int Index_, ref byte Data_)
            {
                Data_ = _Array[Index_].GetByte();
                return this;
            }
            public JsonDataArray Pop(int Index_, ref short Data_)
            {
                Data_ = _Array[Index_].GetInt16();
                return this;
            }
            public JsonDataArray Pop(int Index_, ref ushort Data_)
            {
                Data_ = _Array[Index_].GetUInt16();
                return this;
            }
            public JsonDataArray Pop(int Index_, ref int Data_)
            {
                Data_ = _Array[Index_].GetInt32();
                return this;
            }
            public JsonDataArray Pop(int Index_, ref uint Data_)
            {
                Data_ = _Array[Index_].GetUInt32();
                return this;
            }
            public JsonDataArray Pop(int Index_, ref long Data_)
            {
                Data_ = _Array[Index_].GetInt64();
                return this;
            }
            public JsonDataArray Pop(int Index_, ref ulong Data_)
            {
                Data_ = _Array[Index_].GetUInt64();
                return this;
            }
            public JsonDataArray Pop(int Index_, ref float Data_)
            {
                Data_ = _Array[Index_].GetFloat();
                return this;
            }
            public JsonDataArray Pop(int Index_, ref double Data_)
            {
                Data_ = _Array[Index_].GetDouble();
                return this;
            }
            public JsonDataArray Pop(int Index_, ref string Data_)
            {
                Data_ = _Array[Index_].GetString();
                return this;
            }
            public JsonDataArray Pop(int Index_, TimePoint Data_)
            {
                Data_ = _Array[Index_].GetTimePoint();
                return this;
            }
            public JsonDataArray Pop(int Index_, ref DateTime Data_)
            {
                Data_ = _Array[Index_].GetDateTime();
                return this;
            }
            public JsonDataArray Pop(int Index_, CStream Data_)
            {
                Data_ = _Array[Index_].GetStream();
                return this;
            }
            protected JsonDataArray Push<T>(T Data_)
            {
                // switch 말고 다른 방법은 없는가?
                switch (typeof(T).ToString())
                {
                    case "System.Boolean":
                        Push((bool)(object)Data_);
                        break;
                    case "System.SByte":
                        Push((sbyte)(object)Data_);
                        break;
                    case "System.Byte":
                        Push((byte)(object)Data_);
                        break;
                    case "System.Int16":
                        Push((short)(object)Data_);
                        break;
                    case "System.UInt16":
                        Push((ushort)(object)Data_);
                        break;
                    case "System.Int32":
                        Push((int)(object)Data_);
                        break;
                    case "System.UInt32":
                        Push((uint)(object)Data_);
                        break;
                    case "System.Int64":
                        Push((long)(object)Data_);
                        break;
                    case "System.UInt64":
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
                    default: // SProto 함수를 정의해도 SProto를 상속받은 클래스를 파라미터로 받을 경우 이 함수로 들어오기 때문에 SProto 파라미터 함수는 정의하지 않음.
                        {
                            var Collection = new JsonDataObject();
                            _Array.Add(Collection);
                            ((SProto)(object)Data_).Pop(Collection);
                        }
                        break;
                }

                return this;
            }
            public JsonDataArray Pop<T>(int Index_, ref T Data_)
            {
                // switch 말고 다른 방법은 없는가?
                switch (typeof(T).ToString())
                {
                    case "System.Boolean":
                        {
                            bool Data = false;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.SByte":
                        {
                            sbyte Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.Byte":
                        {
                            byte Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.Int16":
                        {
                            short Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.UInt16":
                        {
                            ushort Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.Int32":
                        {
                            int Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.UInt32":
                        {
                            uint Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.Int64":
                        {
                            long Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.UInt64":
                        {
                            ulong Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.Single":
                        {
                            float Data = 0.0f;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "System.Double":
                        {
                            double Data = 0.0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    // string 은 new 불가로 제네릭 함수에서 처리 불가
                    //case "System.String":
                    //{
                    //    string Data = "";
                    //    Pop(Index_, ref Data);
                    //    Data_ = (T)(object)Data;
                    //}
                    //break;
                    case "rso.core.TimePoint":
                        Pop(Index_, (TimePoint)(object)Data_);
                        break;
                    case "System.DateTime":
                        {
                            var Data = new DateTime();
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case "rso.core.CStream":
                        Pop(Index_, (CStream)(object)Data_);
                        break;
                    default:
                        ((SProto)(object)Data_).Push((JsonDataObject)_Array[Index_]);
                        break;
                }

                return this;
            }
            protected JsonDataArray Push<TValue>(TValue[] Data_)
            {
                var Collection = new JsonDataArray();

                foreach (var i in Data_)
                    Collection.Push(i);

                _Array.Add(Collection);

                return this;
            }
            protected JsonDataArray Push<TValue>(List<TValue> Data_)
            {
                var Collection = new JsonDataArray();

                foreach (var i in Data_)
                    Collection.Push(i);

                _Array.Add(Collection);

                return this;
            }
            protected JsonDataArray Push<TKey>(HashSet<TKey> Data_)
            {
                var Collection = new JsonDataArray();

                foreach (var i in Data_)
                    Collection.Push(i);

                _Array.Add(Collection);

                return this;
            }
            protected JsonDataArray Push<TKey, TValue>(Dictionary<TKey, TValue> Data_)
            {
                var Collection = new JsonDataArray();

                foreach (var i in Data_)
                    Collection.Push(i.Key);

                _Array.Add(Collection);

                return this;
            }
            protected JsonDataArray Push<TKey>(CMultiSet<TKey> Data_)
            {
                var Collection = new JsonDataArray();

                foreach (var i in Data_)
                    Collection.Push(i);

                _Array.Add(Collection);

                return this;
            }
            protected JsonDataArray Push<TKey, TValue>(CMultiMap<TKey, TValue> Data_)
            {
                var Collection = new JsonDataArray();

                foreach (var i in Data_)
                    Collection.Push(i.Key);

                _Array.Add(Collection);

                return this;
            }
            public JsonDataArray Pop(int Index_, string[] Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Data_.Length; ++i)
                    Collection.Pop(i, ref Data_[i]);

                return this;
            }
            public JsonDataArray Pop<TValue>(int Index_, TValue[] Data_) where TValue : new()
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Data_.Length; ++i)
                    Collection.Pop(i, ref Data_[i]);

                return this;
            }
            public JsonDataArray Pop(int Index_, List<string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    string Value = "";
                    Collection.Pop(i, ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public JsonDataArray Pop<TValue>(int Index_, List<TValue> Data_) where TValue : new()
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    var Value = new TValue();
                    Collection.Pop(i, ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public JsonDataArray Pop(int Index_, HashSet<string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    string Key = "";
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key);
                }

                return this;
            }
            public JsonDataArray Pop<TKey>(int Index_, HashSet<TKey> Data_) where TKey : new()
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    var Key = new TKey();
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key);
                }

                return this;
            }
            public JsonDataArray Pop(int Index_, Dictionary<string, string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    string Key = "";
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, "");
                }

                return this;
            }
            public JsonDataArray Pop<TValue>(int Index_, Dictionary<string, TValue> Data_) where TValue : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    string Key = "";
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, new TValue());
                }

                return this;
            }
            public JsonDataArray Pop<TKey>(int Index_, Dictionary<TKey, string> Data_) where TKey : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    var Key = new TKey();
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, "");
                }

                return this;
            }
            public JsonDataArray Pop<TKey, TValue>(int Index_, Dictionary<TKey, TValue> Data_) where TKey : new() where TValue : new()
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    var Key = new TKey();
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, new TValue());
                }

                return this;
            }
            public JsonDataArray Pop(int Index_, CMultiSet<string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    string Value = "";
                    Collection.Pop(i, ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public JsonDataArray Pop<TKey>(int Index_, CMultiSet<TKey> Data_) where TKey : new() // Pop<TKey> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    var Key = new TKey();
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key);
                }

                return this;
            }
            public JsonDataArray Pop(int Index_, CMultiMap<string, string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    string Key = "";
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, "");
                }

                return this;
            }
            public JsonDataArray Pop<TValue>(int Index_, CMultiMap<string, TValue> Data_) where TValue : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    string Key = "";
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, new TValue());
                }

                return this;
            }
            public JsonDataArray Pop<TKey>(int Index_, CMultiMap<TKey, string> Data_) where TKey : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    var Key = new TKey();
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, "");
                }

                return this;
            }
            public JsonDataArray Pop<TKey, TValue>(int Index_, CMultiMap<TKey, TValue> Data_) where TKey : new() where TValue : new()
            {
                var Collection = (JsonDataArray)_Array[Index_];

                for (int i = 0; i < Collection.Count; ++i)
                {
                    var Key = new TKey();
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, new TValue());
                }

                return this;
            }
        }
    }
}