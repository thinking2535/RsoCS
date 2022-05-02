using System;
using System.Collections;
using System.Collections.Generic;

namespace rso
{
    namespace core
    {
        public class CMultiSetEnum<TKey> : IEnumerator
        {
            Dictionary<TKey, List<TKey>> _MultiSet = new Dictionary<TKey, List<TKey>>();
            Dictionary<TKey, List<TKey>>.Enumerator _Current;
            List<TKey>.Enumerator _ValueCurrent;
            bool _First = true;

            public CMultiSetEnum(Dictionary<TKey, List<TKey>> MultiSet_)
            {
                _MultiSet = MultiSet_;
                _Current = _MultiSet.GetEnumerator();
            }
            public bool MoveNext()
            {
                if (_First)
                {
                    if (!_Current.MoveNext())
                        return false;

                    _ValueCurrent = _Current.Current.Value.GetEnumerator();
                    _First = false;
                }

                if (!_ValueCurrent.MoveNext())
                {
                    if (!_Current.MoveNext())
                        return false;

                    _ValueCurrent = _Current.Current.Value.GetEnumerator();
                    _ValueCurrent.MoveNext();
                }

                return true;
            }
            public void Reset()
            {
                _Current = _MultiSet.GetEnumerator();
            }
            object IEnumerator.Current
            {
                get
                {
                    return _Current;
                }
            }
            public TKey Current
            {
                get
                {
                    return _ValueCurrent.Current;
                }
            }
        }
        public class CMultiSet<TKey> : IEnumerable
        {
            int _Count = 0;
            Dictionary<TKey, List<TKey>> _MultiSet = new Dictionary<TKey, List<TKey>>();
            public void Add(TKey Key_)
            {
                List<TKey> Keys;
                if (_MultiSet.TryGetValue(Key_, out Keys))
                {
                    Keys.Add(Key_);
                }
                else
                {
                    Keys = new List<TKey>();
                    Keys.Add(Key_);
                    _MultiSet[Key_] = Keys;
                }
                ++_Count;
            }
            public int Count
            {
                get
                {
                    return _Count;
                }
            }
            public void Clear()
            {
                _MultiSet.Clear();
                _Count = 0;
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public CMultiSetEnum<TKey> GetEnumerator()
            {
                return new CMultiSetEnum<TKey>(_MultiSet);
            }
        }
    }
}