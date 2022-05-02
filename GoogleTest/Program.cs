using System;
using rso.google;

namespace GoogleTest
{
    class Program
    {
        static void Main(String[] args)
        {
            var fcm = new CFCMClient();
            fcm.SetToken("asdf");
            var Token = fcm.GetToken();
            if (Token != null)
                Console.WriteLine(Token);

            Token = fcm.GetToken();
            if (Token != null)
                Console.WriteLine(Token);
        }
    }
}
