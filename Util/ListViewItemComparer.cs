using System;
using System.Collections;
using System.Windows.Forms;

namespace rso
{
    namespace util
    {
        public class ListViewItemComparer : IComparer
        {
            public SortOrder Order;
            public Int32 Column;
            public ListViewItemComparer()
            {
                Column = 0;
            }
            public ListViewItemComparer(SortOrder Order_, Int32 Column_)
            {
                Order = Order_;
                Column = Column_;
            }
            public Int32 Compare(object x, object y)
            {
                var Ret = String.Compare(((ListViewItem)x).SubItems[Column].Text, ((ListViewItem)y).SubItems[Column].Text);
                if (Order == SortOrder.Descending)
                    Ret *= -1;

                return Ret;
            }
        }
    }
}