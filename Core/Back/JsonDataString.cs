using System.Text;

namespace rso
{
    namespace core
    {
        public class JsonDataString : JsonData
        {
            string _Data;
            public JsonDataString(string Value_)
            {
                _Data = Value_;
            }
            public override string ToString(string Name_, string Indentation_)
            {
                return Indentation_ + JsonGlobal.GetNameString(Name_) + "\"" + _Data + "\"";
            }
            public override string GetString() { return _Data; }
            public override CStream GetStream()
            {
                var Stream = new CStream();
                Stream.Push(Encoding.ASCII.GetBytes(_Data));
                return Stream;
            }
        }
    }
}