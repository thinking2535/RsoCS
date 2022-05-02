using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rso.Base;

namespace rso
{
    namespace net
    {
        using TPeerCnt = UInt32;

        public class CPeerMgr
        {
            Dictionary<TPeerCnt, SKey> _PeerList = new Dictionary<TPeerCnt, SKey>();
            TPeerCnt _Counter = 0;

            public SKey Link(TPeerCnt PeerNum_)
            {
                SKey Key = new SKey(PeerNum_, _Counter);

                try
                {
                    _PeerList.Add(PeerNum_, Key);
                    Key.PeerNum = PeerNum_;
                    ++_Counter;

                    return Key;
                }
                catch
                {
                    return null;
                }
            }
            public void UnLink(TPeerCnt PeerNum_)
            {
                _PeerList.Remove(PeerNum_);
            }
        }
    }
}