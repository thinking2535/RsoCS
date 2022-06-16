using System;

namespace rso
{
    namespace gameutil
    {
        public class CFixedRandom
        {
            public UInt64 Seed { get; private set; }
            public UInt64 Counter { get; private set; }

            public CFixedRandom(UInt64 Seed_, UInt64 Counter_)
            {
                Seed = Seed_;
                Counter = Counter_;
            }
            public UInt64 Get()
            {
                ++Counter;
                Int32 ShiftCntSeed = (((Int32)Counter % 128) >> 1);
                Int32 ShiftCntValue = ((Int32)Counter % 64);
                return ((Seed << ShiftCntSeed) | (Seed >> (64 - ShiftCntSeed)) ^
                        (Counter << ShiftCntValue) | (Counter >> (64 - ShiftCntValue)));
            }
        }
    }
}