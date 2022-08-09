using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace rso
{
    namespace core
    {
        // 전체를 순회할 수 있는 it을 중간에 find 할 수 없으므로  MultiMap 의 Value는 수정할 수 없는 것으로 한다. class 형식은 불가피하게 수정이 가능하나 상관없음.
        public class MultiMap<TKey, TValue> : IDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary
        {
            public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDictionaryEnumerator
            {
                MultiMap<TKey, TValue> _MultiMap;
                Dictionary<TKey, Queue<TValue>>.Enumerator _KeyCurrent;
                Queue<TValue>.Enumerator _ValueCurrent;
                bool _First;
                public KeyValuePair<TKey, TValue> Current => new KeyValuePair<TKey, TValue>(_KeyCurrent.Current.Key, _ValueCurrent.Current);
                public object Key => _KeyCurrent.Current.Key;
                public object Value => _ValueCurrent.Current;
                public DictionaryEntry Entry => new DictionaryEntry(Key, Value);
                object IEnumerator.Current => Current;
                internal Enumerator(MultiMap<TKey, TValue> MultiMap_)
                {
                    _MultiMap = MultiMap_;
                    _KeyCurrent = _MultiMap._Datas.GetEnumerator();
                    _ValueCurrent = default(Queue<TValue>.Enumerator);
                    _First = true;
                }
                public void Dispose()
                {
                }
                public bool MoveNext()
                {
                    if (_First)
                    {
                        if (!_KeyCurrent.MoveNext())
                            return false;

                        _ValueCurrent = _KeyCurrent.Current.Value.GetEnumerator();
                        _First = false;
                    }

                    if (!_ValueCurrent.MoveNext())
                    {
                        if (!_KeyCurrent.MoveNext())
                            return false;

                        _ValueCurrent = _KeyCurrent.Current.Value.GetEnumerator();
                        _ValueCurrent.MoveNext();
                    }

                    return true;
                }
                public void Reset()
                {
                    _KeyCurrent = _MultiMap._Datas.GetEnumerator();
                    _ValueCurrent = default(Queue<TValue>.Enumerator);
                    _First = true;
                }
            }
            Int32 _Count = 0;
            Dictionary<TKey, Queue<TValue>> _Datas;
            public void Add(TKey Key_, TValue Value_)
            {
                Queue<TValue> EqualRange;
                if (!_Datas.TryGetValue(Key_, out EqualRange))
                {
                    EqualRange = new Queue<TValue>();
                    _Datas[Key_] = EqualRange;
                }

                EqualRange.Enqueue(Value_);

                ++_Count;
            }
            public bool ContainsKey(TKey Key_)
            {
                return _Datas.ContainsKey(Key_);
            }
            public Int32 Count => _Count;
            public ICollection<TKey> Keys => throw new NotImplementedException();
            public ICollection<TValue> Values => throw new NotImplementedException();
            public bool IsReadOnly => false;
            ICollection IDictionary.Keys => throw new NotImplementedException();
            ICollection IDictionary.Values => throw new NotImplementedException();
            public MultiMap() :
                this((IEqualityComparer<TKey>)null)
            {
            }
            public MultiMap(IEqualityComparer<TKey> comparer)
            {
                _Datas = new Dictionary<TKey, Queue<TValue>>(comparer);
            }
            public bool IsFixedSize => throw new NotImplementedException();
            public object SyncRoot => throw new NotImplementedException();
            public bool IsSynchronized => throw new NotImplementedException();
            public object this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public void RemoveFirst()
            {
                if (_Datas.Count == 0)
                    return;

                _Datas.First().Value.Dequeue();

                if (_Datas.First().Value.Count == 0)
                    _Datas.Remove(_Datas.First().Key);

                --_Count;
            }
            public void Clear()
            {
                _Datas.Clear();
                _Count = 0;
            }
            public KeyValuePair<TKey, TValue> First()
            {
                return new KeyValuePair<TKey, TValue>(_Datas.First().Key, _Datas.First().Value.Peek());
            }
            public TValue[] ToArray(TKey Key_)
            {
                return _Datas[Key_].ToArray();
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(this);
            }
            private static bool IsCompatibleKey(object key)
            {
                if (key == null)
                {
                    throw new Exception();
                }
                return key is TKey;
            }
            public Queue<TValue>.Enumerator GetEqualEnumerator(TKey Key_)
            {
                return _Datas.GetValue(Key_).GetEnumerator();
            }
            public Enumerator GetEnumerator()
            {
                return new Enumerator(this);
            }
            bool IDictionary<TKey, TValue>.Remove(TKey Key_)
            {
                Queue<TValue> Value;
                if (!_Datas.TryGetValue(Key_, out Value))
                    return false;

                _Count -= Value.Count;
                _Datas.Remove(Key_);

                return true;
            }
            public bool TryGetValue(TKey key, out TValue value)
            {
                throw new NotImplementedException();
            }
            public void Add(KeyValuePair<TKey, TValue> item)
            {
                throw new NotImplementedException();
            }
            public bool Contains(KeyValuePair<TKey, TValue> item)
            {
                throw new NotImplementedException();
            }
            public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
            {
                var it = GetEnumerator();
                while(it.MoveNext())
                    array[arrayIndex++] = it.Current;
            }
            bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
            {
                throw new NotImplementedException();
            }
            IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
            {
                return new Enumerator(this);
            }
            bool IDictionary.Contains(object key)
            {
                if (IsCompatibleKey(key))
                {
                    return ContainsKey((TKey)key);
                }
                return false;
            }
            public void Add(object key, object value)
            {
                if (key == null)
                {
                    throw new Exception();
                }
                try
                {
                    TKey key2 = (TKey)key;
                    try
                    {
                        Add(key2, (TValue)value);
                    }
                    catch (InvalidCastException)
                    {
                        throw;
                    }
                }
                catch (InvalidCastException)
                {
                    throw;
                }
            }
            IDictionaryEnumerator IDictionary.GetEnumerator()
            {
                return new Enumerator(this);
            }
            public void Remove(object key)
            {
                if (IsCompatibleKey(key))
                {
                    Remove((TKey)key);
                }
            }
            public void CopyTo(Array array, int index)
            {
                var ConvertedArray = (KeyValuePair<TKey, TValue>[])array;
                CopyTo(ConvertedArray, index);
            }
        }
    }
}