using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttomiek_zadaca_1.klase
{
    internal class DnevnikRada
    {
        public bool odobrenZahtjev { get; set; }
        public DateTime vrijeme { get; set; }
        public int idBrod { get; set; }
        public int idKanala { get; set; }
        public bool slobodan { get; set; }
    }
}
