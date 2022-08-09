using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace rso
{
    namespace core
    {
        public class MultiSet<T> : IEnumerable<T>, IEnumerable, ISet<T>
        {
            public struct Enumerator : IEnumerator<T>, IEnumerator
            {
                MultiSet<T> _MultiSet;
                Dictionary<T, Int32>.Enumerator _Current;
                Int32 _ValueCurrent;
                bool _First;
                public T Current => _Current.Current.Key;
                object IEnumerator.Current => Current;
                internal Enumerator(MultiSet<T> MultiSet_)
                {
                    _MultiSet = MultiSet_;
                    _Current = _MultiSet._Datas.GetEnumerator();
                    _ValueCurrent = 0;
                    _First = true;
                }
                public bool MoveNext()
                {
                    if (_First)
                    {
                        if (!_Current.MoveNext())
                            return false;

                        _First = false;
                    }

                    if (_ValueCurrent == _Current.Current.Value)
                    {
                        if (!_Current.MoveNext())
                            return false;

                        _ValueCurrent = 1;
                    }
                    else
                    {
                        ++_ValueCurrent;
                    }

                    return true;
                }
                void IEnumerator.Reset()
                {
                    _Current = _MultiSet._Datas.GetEnumerator();
                    _ValueCurrent = 0;
                    _First = true;
                }
                public void Dispose()
                {
                }
            }

            Int32 _Count = 0;
            Dictionary<T, Int32> _Datas;
            public MultiSet() :
                this((IEqualityComparer<T>)null)
            {
            }
            public MultiSet(IEqualityComparer<T> comparer)
            {
                _Datas = new Dictionary<T, Int32>(comparer);
            }
            public void Add(T Key_)
            {
                if (_Datas.ContainsKey(Key_))
                    ++_Datas[Key_];
                else
                    _Datas[Key_] = 1;

                ++_Count;
            }
            public bool ContainsKey(T Key_)
            {
                return _Datas.ContainsKey(Key_);
            }
            public Int32 Count => _Count;
            public bool IsReadOnly => false;
            public Enumerator GetEnumerator()
            {
                return new Enumerator(this);
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(this);
            }
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return new Enumerator(this);
            }
            bool ISet<T>.Add(T item)
            {
                Add(item);
                return true;
            }
            public void UnionWith(IEnumerable<T> other)
            {
                foreach (T item in other)
                    Add(item);
            }
            public void IntersectWith(IEnumerable<T> other)
            {
                //foreach (var k in _Datas.Keys)
                //{
                //    if (other.Contains(k))
                //        _Datas[k] = 적은 값으로 대입
                //    else
                //        _Datas.Remove(k);
                //}

                throw new NotImplementedException();
            }
            public void ExceptWith(IEnumerable<T> other)
            {
                throw new NotImplementedException();
            }
            public void SymmetricExceptWith(IEnumerable<T> other)
            {
                throw new NotImplementedException();
            }
            public bool IsSubsetOf(IEnumerable<T> other)
            {
                throw new NotImplementedException();
            }
            public bool IsSupersetOf(IEnumerable<T> other)
            {
                throw new NotImplementedException();
            }
            public bool IsProperSupersetOf(IEnumerable<T> other)
            {
                throw new NotImplementedException();
            }
            public bool IsProperSubsetOf(IEnumerable<T> other)
            {
                throw new NotImplementedException();
            }
            public bool Overlaps(IEnumerable<T> other)
            {
                throw new NotImplementedException();
            }
            public bool SetEquals(IEnumerable<T> other)
            {
                throw new NotImplementedException();
            }
            public bool Contains(T Key_)
            {
                return _Datas.ContainsKey(Key_);
            }
            public void CopyTo(T[] array, int arrayIndex)
            {
                var it = GetEnumerator();
                while (it.MoveNext())
                    array[arrayIndex++] = it.Current;
            }
            bool ICollection<T>.Remove(T Key_)
            {
                Int32 Value;
                if (!_Datas.TryGetValue(Key_, out Value))
                    return false;

                _Count -= Value;
                _Datas.Remove(Key_);
                return true;
            }
            public void RemoveFirst()
            {
                if (_Datas.Count == 0)
                    return;

                --_Datas[_Datas.First().Key];

                if (_Datas.First().Value == 0)
                    _Datas.Remove(_Datas.First().Key);

                --_Count;
            }
            public void RemoveLast()
            {
                if (_Datas.Count == 0)
                    return;

                --_Datas[_Datas.Last().Key];

                if (_Datas.Last().Value == 0)
                    _Datas.Remove(_Datas.Last().Key);

                --_Count;
            }
            public void Clear()
            {
                _Datas.Clear();
                _Count = 0;
            }
            public T First()
            {
                return _Datas.First().Key;
            }
            public T Last()
            {
                return _Datas.Last().Key;
            }
        }
    }
}