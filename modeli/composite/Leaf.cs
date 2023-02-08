using ttomiek_zadaca_1;
using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_2.composite
{
    public class Leaf : Component
    {
        private Vez _podaciVeza;

        public Leaf(Vez podaciVeza)
        {
            if (podaciVeza is not null)
                _podaciVeza = podaciVeza;
            else
                BrojacGreske.Instance.dodajNovuGesku("Vez ne može biti prazan!");
        }

        public override void dodajNoviElement(Component noviElement)
        {
            throw new NotImplementedException();
        }

        public override Mol dohvatiMol()
        {
            throw new NotImplementedException();
        }

        public override Vez dohvatiVez()
        {
            return _podaciVeza;
        }

        public override List<Component> dohvatiDjecu()
        {
            throw new NotImplementedException();
        }
    }
}
