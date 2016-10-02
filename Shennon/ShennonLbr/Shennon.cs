using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShennonLbr
{
    public class Shennon
    {
        #region Поля

        private string message;
        private List<Element> list=new List<Element>();
        private Dictionary<char, string> Dictionary = new Dictionary<char, string>();
        #endregion

        #region Свойства

        public string MessageCode { get; private set; }

        public Dictionary<char,string> DictionaryCodes { get; private set; }
        #endregion

        #region Вложенные классы

        private class Element
        {
            public char Symbol { get; set; }

            public double Probability { get; set; }

            public string Code { get; set; }
        }
        #endregion

        #region Private Методы
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

        private void SetCode(List<Element> listElement)
        {
            if (listElement.Count > 1)
            {
                double count2 = 0, count1 = 0;
                for (int i = 0; i < listElement.Count - 1; i++)
                {
                    count1 = count1 + listElement[i].Probability;
                    count2 = 0;
                    for (int j = i + 1; j < listElement.Count; j++)
                    {
                        count2 = count2 + listElement[j].Probability;
                    }
                    if (count1 < count2) continue;
                    else
                    {
                        List<Element> newlist = new List<Element>();
                        for (int g = 0; g <= i; g++)
                        {
                            listElement[0].Code = listElement[0].Code + 0;
                            newlist.Add(listElement[0]);
                            listElement.RemoveAt(0);
                        }
                        for (int h = 0; h < listElement.Count; h++)
                        {
                            listElement[h].Code = listElement[h].Code + 1;
                        }
                        SetCode(newlist);
                        SetCode(listElement);
                    }
                    break;
                }
            }
            else
            {
                if (listElement[0].Code == null) listElement[0].Code = "0";
                list.Add(listElement[0]);
            }
            
        }

        private void SetProperties()
        {
            if(list!=null)
            {
                foreach(var v in list)
                {
                    Dictionary.Add(v.Symbol, v.Code);
                }
                DictionaryCodes = Dictionary;
                for(int i=0;i<message.Length;i++)
                {
                    var x = from v in Dictionary
                               where (v.Key == message[i])
                               select v;
                    foreach(var v in x)
                    {
                        MessageCode = MessageCode + v.Value;
                    }
                }
            }
        }
        #endregion

        #region Конструкторы

        public Shennon(string message)
        {
            this.message = message;
            SetCode(ProbabilityList(message));
            SetProperties();
        }
        #endregion
    }
}
