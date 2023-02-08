using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.singelton_class;
using ttomiek_zadaca_3.modeli;

namespace ttomiek_zadaca_1.observer
{
    public class ConcreteObserver : Observer
    {
        private int _idKanala;
        private SubjectKanala _subjectKanala;
        private List<string> _popisRedaka;

        public ConcreteObserver(int idKanala)
        {
            this._idKanala = idKanala;
        }

        public void Update(SubjectKanala subjectKanala)
        {
            _popisRedaka = new();
            List<DnevnikRada> dnevnikRada = PodaciDatoteka.Instance.getListaDnevnikaRada().FindAll(x => x.idKanala == _idKanala && x.odobrenZahtjev && !x.slobodan);

            _popisRedaka.Add("Ukupno je " + subjectKanala.TrenutniKapacitet + " brodova vezano na kanalu " + dnevnikRada[0].idKanala);
            _popisRedaka.Add("Obaviještavamo sljedeće brodove: ");
            if (dnevnikRada == null)
            {
                _popisRedaka.Add("Nema niti jednog broda za obavijestiti.");
            }
            else
            {
                ispisZaglavlja();
                int index = 1;
                List<Brod> popisSvihBrodova = new List<Brod>(PodaciDatoteka.Instance.getListaBrodova());
                foreach (DnevnikRada dr in dnevnikRada)
                {
                    Brod podaciBroda = popisSvihBrodova.Find(x => x.id == dr.idBrod);
                    ispisiTablicu(podaciBroda, index++);
                }
                ispisPodnozja(subjectKanala.TrenutniKapacitet);
                KomandeKorisnika.dodajRedoveZaIspis(_popisRedaka);
            }
        }

        private void ispisZaglavlja()
        {
            if (Tablice.Instance.Z)
            {
                string podaciZaglavlja;

                if (Tablice.Instance.RB)
                    podaciZaglavlja = String.Format("{0, 4} | {1,-8} | {2,-12} | {3,-15} | {4,-11}", "RB", "ID BRODA", "OZNAKA BRODA", "NAZIV BRODA", "VRSTA BRODA");
                else
                    podaciZaglavlja = String.Format("{0,-8} | {1,-12} | {2,-15} | {3,-11}", "ID BRODA", "OZNAKA BRODA", "NAZIV BRODA", "VRSTA BRODA");

                _popisRedaka.Add("----------------------------------------------------------");
                _popisRedaka.Add("Brodovi koji se obaviještavaju");
                _popisRedaka.Add(podaciZaglavlja);
                _popisRedaka.Add("----------------------------------------------------------");
            }
        }

        private void ispisiTablicu(Brod podaciBroda, int index)
        {
            string ispisRetka;

            if (Tablice.Instance.RB)
                ispisRetka = String.Format("{0, 4} | {1,8} | {2,-12} | {3,-15} | {4,-11}", index, podaciBroda.id, podaciBroda.oznakaBroda, podaciBroda.naziv, podaciBroda.vrsta);
            else
                ispisRetka = String.Format("{0,8} | {1,-12} | {2,-15} | {3,-11}", podaciBroda.id, podaciBroda.oznakaBroda, podaciBroda.naziv, podaciBroda.vrsta);

            _popisRedaka.Add(ispisRetka);
        }

        private void ispisPodnozja(int broj)
        {
            if (Tablice.Instance.P)
            {
                _popisRedaka.Add("----------------------------------------------------------");
                _popisRedaka.Add(String.Format("{0,-14} {1,42}", "UKUPNO REDAKA:", broj));
                _popisRedaka.Add("----------------------------------------------------------");
            }
        }

        public SubjectKanala SubjectKanala
        {
            get { return _subjectKanala; }
            set { _subjectKanala = value; }
        }
    }
}
