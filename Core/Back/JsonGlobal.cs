using System;

namespace rso
{
    namespace core
    {
        public static class JsonGlobal
        {
            public static string GetNameString(string Name_)
            {
                return Name_.Length == 0 ? "" : ("\"" + Name_ + "\" : ");
            }
            public static string PushIndentation(this string Str_)
            {
                return Str_ += "   ";
            }
            public static string PopIndentation(this string Str_)
            {
                return Str_.Remove(Str_.Length - 3, 3);
            }
        }
    }
}