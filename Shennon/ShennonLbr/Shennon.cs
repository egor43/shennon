using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShennonLbr
{
    public class Shennon
    {
        internal class Element
        {
            public char Symbol { get; set; }

            public double Probability { get; set; }

            public char Group { get; set; }

            public char Code { get; set; }
        }

        private List<Element> ProbabilityList(string message)
        {
            Dictionary<char, double> dict = new Dictionary<char, double>();
            char ch = '\0';
            int count;
            for (int i = 0; i < message.Length; i++)
            {
                ch = message[i];
                count = 0;
                if (!dict.ContainsKey(ch))
                {
                    for (int j = i; j < message.Length; j++)
                    {
                        if (ch == message[j]) count++;
                    }
                    dict.Add(ch, (count / Convert.ToDouble(message.Length)));
                }
            }
            var x = from v in dict
                    orderby v.Value descending
                    select v;
            List<Element> result = new List<Element>();
            foreach(var c in x)
            {
                result.Add(new Element() { Symbol = c.Key, Probability = c.Value });
            }
            return result;
        }
        public Shennon(string message)
        {
            //HACK: для теста
            ProbabilityList(message);
        }
    }
}
