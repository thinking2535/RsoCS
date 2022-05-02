using rso.Base;
using System;
using System.Collections.Generic;

namespace GameClientTest
{
    using TPeerCnt = UInt32;
    using TLongIP = UInt32;
    using TUID = Int64;
    using TRUID = UInt64;
    using TCode = Int32;
    using TBID = Int32;
    using TItemRoomIndex = UInt64;
    using TMineRoomIndex = UInt64;

    public class CClient
    {
        public String ID = "";
        public String PW = "";
        public String Nick = "";
        public TUID UID = 0;
        public TRUID RUID = 0;
        public bool IsPlaying = false;
        public Int32 LastCastTick = 0;

        public CClient(String ID_, String PW_, TUID UID_)
        {
            ID = ID_;
            PW = PW_;
            UID = UID_;
        }
    }
}
