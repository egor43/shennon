using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShennonLbr;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Shennon sh = new Shennon(Console.ReadLine());
            Console.WriteLine(sh.MessageCode);
            Console.WriteLine();
            foreach(var v in sh.DictionaryCodes)
            {
                Console.WriteLine("symbol: {0}    code: {1}",v.Key,v.Value);
            }
            Console.ReadLine();
        }
    }
}
