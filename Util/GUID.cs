using System;

namespace rso.util
{
    public class CGUID
    {
        static Int32 StrLength = 32;
        static Int32 CheckSumSize = 8;
        public static string Create()
        {
            var Str = Guid.NewGuid().ToString("N").ToUpper();

            if (Str.Length != StrLength)
                throw new Exception("Invalid Strin Length");

            string NewStr = "";
            byte CheckSum = 0;
            for (Int32 i = 0; i < Str.Length; ++i)
            {
                NewStr += Str[i];
                CheckSum ^= (byte)Str[i];

                if ((i + 1) % CheckSumSize == 0)
                {
                    CheckSum %= 36; // Capital Count + Number Count = 26 + 10 = 36

                    if (CheckSum < 26)
                        NewStr += (char)('A' + CheckSum);
                    else
                        NewStr += (char)('0' + (CheckSum - 26));

                    CheckSum = 0;
                }
            }

            return NewStr;
        }
        public static bool Check(string Str_)
        {
            if (Str_.Length != (StrLength + (StrLength / CheckSumSize)))
                return false;

            byte CheckSum = 0;
            Int32 CheckIndex = CheckSumSize;
            for (Int32 i = 0; i < Str_.Length; ++i)
            {
                if (i == CheckIndex)
                {
                    CheckSum %= 36;

                    if (CheckSum < 26)
                    {
                        if ((char)('A' + CheckSum) != Str_[i])
                            return false;
                    }
                    else
                    {
                        if ((char)('0' + (CheckSum - 26)) != Str_[i])
                            return false;
                    }

                    CheckSum = 0;
                    CheckIndex += (CheckSumSize + 1);
                }
                else
                {
                    CheckSum ^= (byte)Str_[i];
                }
            }

            return true;
        }
    }
}
