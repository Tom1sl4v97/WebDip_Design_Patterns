using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_1.bridge
{
    //Abstraction
    class BridgeDostupnostiVeza
    {
        protected DostupnostVezaInterface _dostupnostVeza;
        
        public BridgeDostupnostiVeza(DostupnostVezaInterface dostupnostVeza)
        {
            _dostupnostVeza = dostupnostVeza;
        }

        public virtual List<ZahtjevRezervacije> naredbaDostupnosti(Vez vez, DateTime datumOd, DateTime datumDo, List<ZahtjevRezervacije> pomocnaListaZahtjeva)
        {
            return _dostupnostVeza.naredbaDostupnosti(vez, datumOd, datumDo, pomocnaListaZahtjeva);
        }
    }
    
    //Extended Abstraction
    class DohvatiOdredeneVezove : BridgeDostupnostiVeza
    {
        public DohvatiOdredeneVezove(DostupnostVezaInterface dostupnostVeza) : base(dostupnostVeza)
        {
            
        }
        public override List<ZahtjevRezervacije> naredbaDostupnosti(Vez vez, DateTime datumOd, DateTime datumDo, List<ZahtjevRezervacije> pomocnaListaZahtjeva)
        {
            return base.naredbaDostupnosti(vez, datumOd, datumDo, pomocnaListaZahtjeva);
        }
    }

    class KreirajMetodu
    {
        public Vez vez { get; set; }
        public DateTime datumOd { get; set; }
        public DateTime datumDo { get; set; }
        public List<ZahtjevRezervacije> pomocnaListaZahtjeva { get; set; }
        public List<ZahtjevRezervacije> ispisVezova(BridgeDostupnostiVeza bridgeDostupnostiVeza)
        {
            return bridgeDostupnostiVeza.naredbaDostupnosti(vez, datumOd, datumDo, pomocnaListaZahtjeva);
        }
    }
}
