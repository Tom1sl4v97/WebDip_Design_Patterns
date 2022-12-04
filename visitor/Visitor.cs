using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttomiek_zadaca_1.visitor
{
    public abstract class Visitor
    {
        public abstract void VisitConcreteElementZ(ConcreteElementZ concreteElementZ);
        public abstract void VisitConcreteElementP(ConcreteElementP concreteElementP);
        public abstract void VisitConcreteElementRB(ConcreteElementRB concreteElementRB);
    }
}
