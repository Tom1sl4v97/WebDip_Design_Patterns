using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttomiek_zadaca_1.visitor
{
    public class ConcreteVisitor1 : Visitor
    {
        public override void VisitConcreteElementZ(ConcreteElementZ concreteElementZ)
        {
            Console.WriteLine("{0} visited by {1}", concreteElementZ.GetType().Name, this.GetType().Name);
        }

        public override void VisitConcreteElementP(ConcreteElementP concreteElementP)
        {
            Console.WriteLine("{0} visited by {1}", concreteElementP.GetType().Name, this.GetType().Name);
        }

        public override void VisitConcreteElementRB(ConcreteElementRB concreteElementRB)
        {
            Console.WriteLine("{0} visited by {1}", concreteElementRB.GetType().Name, this.GetType().Name);
        }
    }
}
