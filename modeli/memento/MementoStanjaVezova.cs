using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_2.memento
{
    public class MementoStanjaVezova
    {
        StanjeZahtjevaRezervacije stanjeVezova;
        public MementoStanjaVezova(StanjeZahtjevaRezervacije state)
        {
            this.stanjeVezova = state;
        }
        public StanjeZahtjevaRezervacije dohvatiStanje
        {
            get { return stanjeVezova; }
        }
    }
}
