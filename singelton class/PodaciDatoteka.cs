using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_1
{
    internal class PodaciDatoteka
    {
        private PodaciDatoteka()
        {

        }

        private static PodaciDatoteka? instance = null;
        public static PodaciDatoteka Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PodaciDatoteka();
                }
                return instance;
            }
        }

        private readonly List<Brod> _listaBrodova = new();
        private Luka _luka = new();
        private readonly List<Raspored> _listaRasporeda = new();
        private readonly List<Vez> _listaVeza = new();
        private readonly List<ZahtjevRezervacije> _listaZahtjevaRezervacije = new();

        public void addNoviBrod(Brod newBrod)
        {
            _listaBrodova.Add(newBrod);
        }
        
        public List<Brod> getListaBrodova()
        {
            return _listaBrodova;
        }

        public void setNovaLuka(Luka novaLuka)
        {
            _luka = novaLuka;
        }

        public Luka getLuka()
        {
            return _luka;
        }

        public void addNoviRaspored(Raspored newRaspored)
        {
            _listaRasporeda.Add(newRaspored);
        }

        public List<Raspored> getListaRasporeda()
        {
            return _listaRasporeda;
        }

        public void addNoviVez(Vez newVez)
        {
            _listaVeza.Add(newVez);
        }

        public List<Vez> getListaVeza()
        {
            return _listaVeza;
        }

        public void addNoviZahtjevRezervacije(ZahtjevRezervacije newZahtjevRezervacije)
        {
            _listaZahtjevaRezervacije.Add(newZahtjevRezervacije);
        }

        public List<ZahtjevRezervacije> getListaZahtjevaRezervacije()
        {
            return _listaZahtjevaRezervacije;
        }

    }
}
