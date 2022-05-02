using System;

namespace rso
{
    namespace core
    {
        class CVector<TData>
        {
            TData[] _Datas = new TData[1];
            int _Size = 0;

            public TData[] Data
            {
                get
                {
                    return _Datas;
                }
            }
            public int Size
            {
                get
                {
                    return _Size;
                }
            }
            public TData this[int Index_]
            {
                get
                {
                    return _Datas[Index_];
                }
                set
                {
                    _Datas[Index_] = value;
                }
            }
            public void PushBack(TData Data_)
            {
                if (_Size == _Datas.Length)
                {
                    TData[] NewDatas = new TData[_Datas.Length * 2];
                    Array.Copy(_Datas, NewDatas, _Datas.Length);
                    _Datas = NewDatas;
                }

                _Datas[_Size] = Data_;
                ++_Size;
            }
            public void PopBack()
            {
                if (_Size == 0)
                    return;

                --_Size;
            }
            public void Resize(int Size_)
            {
                if (Size_ > _Datas.Length)
                {
                    TData[] NewDatas = new TData[Size_];
                    Array.Copy(_Datas, NewDatas, _Datas.Length);
                    _Datas = NewDatas;
                }
                _Size = Size_;
            }
        }
    }
}