using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rso;
using rso.crypto;

namespace CryptoTest
{
    class Program
    {
        static void Main(String[] args)
        {
            var a = new CCrypto();
            var b = new CCrypto();

            byte[] data = { 9,8,7,6,5,4,3,2,1,0 };

            for (int i = 0; i < 10; ++i)
            {
                a.Encode(data, 0, data.Length, 0x1f3a49b72c8d5ef6);
                foreach (byte d in data)
                    Console.Write(String.Format("{0,4}", d));
                Console.WriteLine();

                b.Decode(data, 0, data.Length, 0x1f3a49b72c8d5ef6);
                foreach (byte d in data)
                    Console.Write(String.Format("{0,4}", d));
                Console.WriteLine();
            }
        }
    }
}
