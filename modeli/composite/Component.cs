using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_2.composite
{
    public abstract class Component
    {
        public abstract void dodajNoviElement(Component noviElement);
        public abstract Mol dohvatiMol();
        public abstract Vez dohvatiVez();
        public abstract List<Component> dohvatiDjecu();

    }
}
