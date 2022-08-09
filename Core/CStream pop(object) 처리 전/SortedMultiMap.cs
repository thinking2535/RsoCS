﻿using System;
using System.Collections.Generic;

namespace rso.core
{
    public class SortedMultiMap<TKey, TValue> : SortedDictionary<TKey, TValue> where TKey : IComparable
    {
        public class MultiMapComparer : Comparer<TKey>
        {
            public override Int32 Compare(TKey x, TKey y)
            {
                if (x == null || y == null)
                    return 0;

                return x.Equals(y) ? 1 : Default.Compare(x, y);
            }
        }
        public SortedMultiMap() :
            base(new MultiMapComparer())
        {
        }
        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
        }
    }
}
