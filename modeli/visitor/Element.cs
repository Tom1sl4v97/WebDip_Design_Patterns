using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttomiek_zadaca_1.visitor
{
    public abstract class Element
    {
        public abstract void Accept(Visitor visitor);
    }
}
