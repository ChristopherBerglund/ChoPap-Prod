using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Features.Counter
{
    public class TimeCounter
    {
        private static readonly int Seconds = 180;
        public static void Counter()
        {
            for (int a = Seconds; a >= 0; a--)
            {
                Console.Write("\rNext update in {0:00}", a);
                Thread.Sleep(1000);
            }
        }
    }
}
