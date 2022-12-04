using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttomiek_zadaca_1.visitor
{
    public class ConcreteElementP : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementP(this);
        }

        public void OperationB()
        {
        }
    }
}
