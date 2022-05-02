namespace rso
{
    namespace core
    {
        public class JsonDataBool : JsonData
        {
            bool _Data;
            public JsonDataBool(bool Value_)
            {
                _Data = Value_;
            }
            public override string ToString(string Name_, string Indentation_)
            {
                return Indentation_ + JsonGlobal.GetNameString(Name_) + _Data.ToString();
            }
            public override bool GetBool() { return _Data; }
        }
    }
}