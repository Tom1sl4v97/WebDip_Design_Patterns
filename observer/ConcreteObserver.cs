using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.singelton_class;

namespace ttomiek_zadaca_1.observer
{
    public class ConcreteObserver : Observer
    {
        private int _idKanala;
        private SubjectKanala _subjectKanala;

        public ConcreteObserver(int idKanala)
        {
            this._idKanala = idKanala;
        }

        public void Update(SubjectKanala subjectKanala)
        {
            List<DnevnikRada> dnevnikRada = PodaciDatoteka.Instance.getListaDnevnikaRada().FindAll(x => x.idKanala == _idKanala && x.odobrenZahtjev && !x.slobodan);

            Console.WriteLine("Ukupno je " + subjectKanala.TrenutniKapacitet + " brodova vezano na kanalu " + dnevnikRada[0].idKanala);
            Console.WriteLine("Obaviještavamo sljedeće brodove: ");
            if (dnevnikRada == null)
            {
                Console.WriteLine("Nema niti jednog broda za obavijestiti.");
            }
            else
            {
                ispisZaglavlja();
                foreach (DnevnikRada dr in dnevnikRada)
                {
                    Brod podaciBroda = PodaciDatoteka.Instance.getListaBrodova().Find(x => x.id == dr.idBrod);
                    ispisiTablicu(podaciBroda);
                }
                ispisPodnozja(subjectKanala.TrenutniKapacitet);
            }
        }

        private void ispisZaglavlja()
        {
            if (Tablice.Instance.Z)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("Brodovi koji se obaviještavaju");
                Console.WriteLine(String.Format("{0,-8} | {1,-12} | {2,-15} | {3,-11}", "ID BRODA", "OZNAKA BRODA", "NAZIV BRODA", "VRSTA BRODA"));
                Console.WriteLine("----------------------------------------------------------");
            }
        }

        private void ispisiTablicu(Brod? podaciBroda)
        {
            Console.WriteLine(String.Format("{0,8} | {1,-12} | {2,-15} | {3,-11}", podaciBroda.id, podaciBroda.oznakaBroda, podaciBroda.naziv, podaciBroda.vrsta));
        }

        private void ispisPodnozja(int broj)
        {
            if (Tablice.Instance.P)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine(String.Format("{0,-14} {1,42}", "UKUPNO REDAKA:", broj));
                Console.WriteLine("----------------------------------------------------------");
            }
        }

        public SubjectKanala SubjectKanala
        {
            get { return _subjectKanala; }
            set { _subjectKanala = value; }
        }
    }
}
