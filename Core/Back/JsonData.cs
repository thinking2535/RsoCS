using System;

namespace rso
{
    namespace core
    {
        public abstract class JsonData
        {
            public abstract string ToString(string Name_, string Indentation_);
            public virtual bool GetBool() { throw new Exception("Can Not Convert To Bool"); }
            public virtual char GetChar() { throw new Exception("Can Not Convert To Char"); }
            public virtual sbyte GetSByte() { throw new Exception("Can Not Convert To Int8"); }
            public virtual byte GetByte() { throw new Exception("Can Not Convert To UInt8"); }
            public virtual short GetInt16() { throw new Exception("Can Not Convert To Int16"); }
            public virtual ushort GetUInt16() { throw new Exception("Can Not Convert To UInt16"); }
            public virtual int GetInt32() { throw new Exception("Can Not Convert To Int32"); }
            public virtual uint GetUInt32() { throw new Exception("Can Not Convert To UInt32"); }
            public virtual long GetInt64() { throw new Exception("Can Not Convert To Int64"); }
            public virtual ulong GetUInt64() { throw new Exception("Can Not Convert To UInt64"); }
            public virtual float GetFloat() { throw new Exception("Can Not Convert To Float"); }
            public virtual double GetDouble() { throw new Exception("Can Not Convert To Double"); }
            public virtual string GetString() { throw new Exception("Can Not Convert To String"); }
            public virtual TimePoint GetTimePoint() { throw new Exception("Can Not Convert To TimePoint"); }
            public virtual DateTime GetDateTime() { throw new Exception("Can Not Convert To DateTime"); }
            public virtual CStream GetStream() { throw new Exception("Can Not Convert To Stream"); }
        }
    }
}