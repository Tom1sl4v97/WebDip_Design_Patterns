using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttomiek_zadaca_1.visitor
{
    public class ConcreteElementRedniBroj : Element
    {
        private string _vrstaVeza;

        public ConcreteElementRedniBroj(string vrstaVeza)
        {
            _vrstaVeza = vrstaVeza;
        }

        public string vrstaVeza
        {
            get { return _vrstaVeza; }
            set { _vrstaVeza = value; }
        }

        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementRedniBroj(this);
        }
    }
}
