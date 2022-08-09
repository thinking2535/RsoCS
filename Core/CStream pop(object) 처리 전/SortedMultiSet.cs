using System;
using System.Collections.Generic;

namespace rso.core
{
    public class SortedMultiSet<TKey> : SortedSet<TKey> where TKey : IComparable
    {
        public class MultiSetComparer : Comparer<TKey>
        {
            public override Int32 Compare(TKey x, TKey y)
            {
                if (x == null || y == null)
                    return 0;

                return x.Equals(y) ? 1 : Default.Compare(x, y);
            }
        }
        public SortedMultiSet() :
            base(new MultiSetComparer())
        {
        }
        public new void Add(TKey key)
        {
            base.Add(key);
        }
    }
}
