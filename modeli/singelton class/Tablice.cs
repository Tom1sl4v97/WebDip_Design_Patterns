using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.visitor;

namespace ttomiek_zadaca_1
{
    internal class Tablice
    {
        private Tablice()
        {

        }

        private static Tablice? instance = null;
        public static Tablice Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Tablice();
                }
                return instance;
            }
        }

        public bool Z { get; set; }
        public bool P { get; set; }
        public bool RB { get; set; }
    }
}
