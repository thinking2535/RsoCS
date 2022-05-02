using System;
using rso.physics;

namespace GameClientTest
{
    using TUID = Int64;
    using TCode = Int32;

    public class CChar : SObjectStraight
    {
        TCode _Code;
        bool _IsPlaying = false;

        public CChar(TCode Code_, bool IsPlaying_)
        {
            _Code = Code_;
            _IsPlaying = IsPlaying_;
        }
        public void Proc(double Time_)
        {

        }
    }
}
