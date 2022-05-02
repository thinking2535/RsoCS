namespace rso
{
    namespace core
    {
        public abstract partial class SProto
        {
            public abstract void Push(CStream Stream_);
            public abstract void Pop(CStream Stream_);
            public abstract void Push(JsonDataObject Value_);
            public abstract void Pop(JsonDataObject Value_);
        }
    }
}