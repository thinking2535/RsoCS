using rso.core;
using System;
using System.Collections.Generic;

namespace rso
{
    namespace excel
    {
        public class CEnumValue
        {
            Dictionary<string, Type> _Enums = new Dictionary<string, Type>();
            public CEnumValue(List<Enum> EnumTypes_)
            {
                foreach (var EnumType in EnumTypes_)
                    _Enums.Add(EnumType.GetType().FullName, EnumType.GetType());
            }
            public void PushToStream(string EnumName_, string ValueName_, CStream Stream_)
            {
                try
                {
                    var EnumType = _Enums[EnumName_];

                    switch (Type.GetTypeCode(Enum.GetUnderlyingType(EnumType)))
                    {
                        case TypeCode.SByte:
                            Stream_.Push((SByte)Enum.Parse(EnumType, ValueName_, false));
                            break;
                        case TypeCode.Byte:
                            Stream_.Push((Byte)Enum.Parse(EnumType, ValueName_, false));
                            break;
                        case TypeCode.Int16:
                            Stream_.Push((Int16)Enum.Parse(EnumType, ValueName_, false));
                            break;
                        case TypeCode.UInt16:
                            Stream_.Push((UInt16)Enum.Parse(EnumType, ValueName_, false));
                            break;
                        case TypeCode.Int32:
                            Stream_.Push((Int32)Enum.Parse(EnumType, ValueName_, false));
                            break;
                        case TypeCode.UInt32:
                            Stream_.Push((UInt32)Enum.Parse(EnumType, ValueName_, false));
                            break;
                        case TypeCode.Int64:
                            Stream_.Push((Int64)Enum.Parse(EnumType, ValueName_, false));
                            break;
                        case TypeCode.UInt64:
                            Stream_.Push((UInt64)Enum.Parse(EnumType, ValueName_, false));
                            break;
                        default:
                            throw null;
                    }
                }
                catch
                {
                    throw new Exception("Invalid EnumTypeName[" + EnumName_ + "] or EnumValueName[" + ValueName_ + "]");
                }
            }
        }
    }
}