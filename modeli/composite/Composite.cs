using ttomiek_zadaca_1;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_2.iterator;

namespace ttomiek_zadaca_2.composite
{
    public class Composite : Component
    {
        private List<Component> _popisElemenata = new List<Component>();
        private List<MolVez> _popisMolVezova = new List<MolVez>();

        public Luka _podaciLuke;
        private Mol _podaciMola;
        private Vez _podaciVeza;

        public Composite(Mol podaciMola)
        {
            if (podaciMola is not null)
                _podaciMola = podaciMola;
            else
                BrojacGreske.Instance.dodajNovuGesku("Mol ne može biti prazan!");
        }

        public Composite(Luka podaciLuke)
        {
            if (podaciLuke is not null)
                _podaciLuke = podaciLuke;
            else
                BrojacGreske.Instance.dodajNovuGesku("Luka ne može biti prazna!");
        }

        public override void dodajNoviElement(Component noviElement)
        {
            _popisElemenata.Add(noviElement);
        }

        public override Mol dohvatiMol()
        {
            return _podaciMola;
        }

        public override Vez dohvatiVez()
        {
            throw new NotImplementedException();
        }

        public override List<Component> dohvatiDjecu()
        {
            return _popisElemenata;
        }
        

        public void postaviPovezanostMolVez(List<MolVez> popisMolVeza)
        {
            _popisMolVezova.AddRange(popisMolVeza);
        }

        public void dodajListMolu(Vez noviVez)
        {
            try
            {
                int idMola = dohvatiIdMola(noviVez.id);

                Component? trazeniMol = _popisElemenata.Find(x => x.dohvatiMol().idMol == idMola);
                if (trazeniMol is not null)
                {
                    trazeniMol.dodajNoviElement(new Leaf(noviVez));
                }
                else
                    BrojacGreske.Instance.dodajNovuGesku("Mol nije pronađen!");
            }
            catch (Exception e)
            {
                BrojacGreske.Instance.dodajNovuGesku(e.Message);
            }
        }

        private int dohvatiIdMola(int idVeza)
        {
            foreach (MolVez podaciPovezanosti in _popisMolVezova)
            {
                if (podaciPovezanosti.idVez == idVeza)
                    return podaciPovezanosti.idMol;
            }
            throw new Exception("Traženi vez sa ID-em:  " + idVeza + " nema svoj određeni mol!");
        }

        public List<Vez> dohvatiPopisVezova()
        {
            bool provjera = true;
            foreach (Component element in _popisElemenata)
            {
                foreach (Component element2 in element.dohvatiDjecu())
                {
                    provjera = false;
                    break;
                }
                break;
            }

            if (provjera)
                return new List<Vez>();

            ConcreteAggregate ca = new ConcreteAggregate();
            int brojac = 0;
            foreach (Component element in _popisElemenata)
                foreach (Component element2 in element.dohvatiDjecu())
                    ca[brojac++] = element2.dohvatiVez();

            List<Vez> popisMolova = new List<Vez>();

            Iterator i = ca.KreirajIterator();

            Vez? item = i.DohvatiPrvoga() as Vez;

            while (item is not null)
            {
                popisMolova.Add(item);
                item = i.Sljedeci() as Vez;
            }

            return popisMolova;
        }

        public List<Mol> dohvatiPopisMolova()
        {
            if (_popisElemenata.Count == 0)
                return new List<Mol>();

            ConcreteAggregate ca = new ConcreteAggregate();
            int brojac = 0;
            foreach (Component element in _popisElemenata)
            {
                ca[brojac++] = element.dohvatiMol();
            }

            List<Mol> popisMolova = new List<Mol>();

            Iterator i = ca.KreirajIterator();

            Mol? item = i.DohvatiPrvoga() as Mol;

            while (item is not null)
            {
                popisMolova.Add(item);
                item = i.Sljedeci() as Mol;
            }

            return popisMolova;
        }
    }
}
