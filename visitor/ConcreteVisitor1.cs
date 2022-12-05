using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;

namespace ttomiek_zadaca_1.visitor
{
    public class ConcreteVisitor1 : Visitor
    {
        private int broj = 0;

        public override void VisitConcreteElementZauzetost(ConcreteElementZauzetost concreteElementZauzetost)
        {
            List<Vez> popisSvihVezove = new List<Vez>(PodaciDatoteka.Instance.getListaVeza());
            List<Vez> popisOdredenihVezova = popisSvihVezove.FindAll(x => x.vrsta == concreteElementZauzetost.vrstaVeza);
            bool sviVezovi = false;
            List<Vez> popisZauzetihVezova = ZajednickeMetode.ispisTrenutnogStatusa(popisOdredenihVezova, concreteElementZauzetost.vrijeme, sviVezovi);
            
            foreach (Vez vez in popisZauzetihVezova)
            {
                ZajednickeMetode.ispisVeza(vez, "ZAUZETI");
            }

            broj = popisZauzetihVezova.Count;
            Aplikacija.povecajUkupniBrojZauzetihVezova(broj);
        }

        public override void VisitConcreteElementRedniBroj(ConcreteElementRedniBroj concreteElementRedniBroj)
        {
            if (Tablice.Instance.RB)
            {
                Console.WriteLine(String.Format("{0,-21} {1,52}", "UKUPNO VRSTE VEZA " + concreteElementRedniBroj.vrstaVeza + ":", broj));
            }
        }
    }
}
