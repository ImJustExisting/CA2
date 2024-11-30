using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CA2
{
    internal class Card
    {
        public string CardVal { get; set; }
        public string CardSuit { get; set; }
        public int CardNum { get; set; }



        public override string ToString()
        {
            return $"Card Dealt is the {CardVal} of {CardSuit}, the value is {CardNum}";
        }
    }
}
