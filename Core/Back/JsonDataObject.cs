using System;
using System.Collections.Generic;

namespace rso
{
    namespace core
    {
        public class JsonDataObject : JsonDataArray
        {
            List<string> _Names = new List<string>();
            HashSet<string> _NameSet = new HashSet<string>();
            public override string ToString(string Name_, string Indentation_)
            {
                string Str = Indentation_ + JsonGlobal.GetNameString(Name_) + "{\n";
                Indentation_ = Indentation_.PushIndentation();

                if (_Array.Count > 0)
                    Str += _Array[0].ToString(_Names[0], Indentation_);

                for (int i = 1; i < _Array.Count; ++i)
                {
                    Str += ",\n";
                    Str += _Array[i].ToString(_Names[i], Indentation_);
                }
                Str += "\n";

                Indentation_ = Indentation_.PopIndentation();
                Str += Indentation_;
                Str += "}";

                return Str;
            }
            public override string ToString()
            {
                return ToString("", "");
            }
            void Add(string Name_)
            {
                if (_NameSet.Contains(Name_))
                    throw new Exception("Duplicate Name : " + Name_);

                _NameSet.Add(Name_);
                _Names.Add(Name_);
            }
            public JsonDataObject Push(string Name_, bool Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, char Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, sbyte Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, byte Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, short Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, ushort Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, int Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, uint Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, long Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, ulong Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, float Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, double Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, string Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, TimePoint Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, DateTime Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, CStream Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push<T>(string Name_, T Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push<TValue>(string Name_, TValue[] Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push<TValue>(string Name_, List<TValue> Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push<TKey>(string Name_, HashSet<TKey> Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push<TKey, TValue>(string Name_, Dictionary<TKey, TValue> Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push<TKey>(string Name_, CMultiSet<TKey> Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
            public JsonDataObject Push<TKey, TValue>(string Name_, CMultiMap<TKey, TValue> Data_)
            {
                Add(Name_);
                Push(Data_);
                return this;
            }
        }
    }
}