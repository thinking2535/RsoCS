using System;
using System.Collections;
using System.Collections.Generic;

namespace rso
{
    namespace core
    {
        public class CMultiMapEnum<TKey, TValue> : IEnumerator
        {
            Dictionary<TKey, List<TValue>> _MultiMap = new Dictionary<TKey, List<TValue>>();
            Dictionary<TKey, List<TValue>>.Enumerator _Current;
            List<TValue>.Enumerator _ValueCurrent;
            bool _First = true;

            public CMultiMapEnum(Dictionary<TKey, List<TValue>> MultiMap_)
            {
                _MultiMap = MultiMap_;
                _Current = _MultiMap.GetEnumerator();
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
                _Current = _MultiMap.GetEnumerator();
            }
            object IEnumerator.Current
            {
                get
                {
                    return _Current;
                }
            }
            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    return new KeyValuePair<TKey, TValue>(_Current.Current.Key, _ValueCurrent.Current);
                }
            }
        }
        public class CMultiMap<TKey, TValue> : IEnumerable
        {
            int _Count = 0;
            Dictionary<TKey, List<TValue>> _MultiMap = new Dictionary<TKey, List<TValue>>();
            public void Add(TKey Key_, TValue Value_)
            {
	            List<TValue> Values;
	            if (_MultiMap.TryGetValue(Key_, out Values))
	            {
	                Values.Add(Value_);
	            }
	            else
	            {
	                Values = new List<TValue>();
	                Values.Add(Value_);
	                _MultiMap[Key_] = Values;
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
                _MultiMap.Clear();
                _Count = 0;
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public CMultiMapEnum<TKey, TValue> GetEnumerator()
            {
                return new CMultiMapEnum<TKey, TValue>(_MultiMap);
            }
        }
    }
}