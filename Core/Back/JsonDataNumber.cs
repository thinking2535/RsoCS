using System;

namespace rso
{
    namespace core
    {
        public class JsonDataNumber : JsonData
        {
            double _Data;
            public JsonDataNumber(double Value_)
            {
                _Data = Value_;
            }
            public override string ToString(string Name_, string Indentation_)
            {
                return Indentation_ + JsonGlobal.GetNameString(Name_) + _Data.ToString();
            }
            public override char GetChar() { return (char)_Data; }
            public override sbyte GetSByte() { return (sbyte)_Data; }
            public override byte GetByte() { return (byte)_Data; }
            public override short GetInt16() { return (short)_Data; }
            public override ushort GetUInt16() { return (ushort)_Data; }
            public override int GetInt32() { return (int)_Data; }
            public override uint GetUInt32() { return (uint)_Data; }
            public override long GetInt64() { return (long)_Data; }
            public override ulong GetUInt64() { return (ulong)_Data; }
            public override float GetFloat() { return (float)_Data; }
            public override double GetDouble() { return _Data; }
            public override TimePoint GetTimePoint() { return new TimePoint((long)_Data); }
            public override DateTime GetDateTime() { return GetTimePoint().ToDateTime(); }
        }
    }
}