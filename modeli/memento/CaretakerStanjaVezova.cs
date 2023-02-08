using ttomiek_zadaca_1;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_2.view;

namespace ttomiek_zadaca_2.memento
{
    public class CaretakerStanjaVezova
    {
        List<MementoStanjaVezova> stanjeZahtjevaMemento = new();

        public bool dodajNovoStanje(MementoStanjaVezova novoStanje)
        {
            if (stanjeZahtjevaMemento.Any(x => x.dohvatiStanje.nazivStanja == novoStanje.dohvatiStanje.nazivStanja))
            {
                BrojacGreske.Instance.dodajNovuGesku("Greška prilikom spremanja stanja: Stanje s nazivom \"" + novoStanje.dohvatiStanje.nazivStanja + "\" već postoji.");
                return false;
            }
            stanjeZahtjevaMemento.Add(novoStanje);
            return true;
        }

        public MementoStanjaVezova? vratiStanje(string nazivZeljenogStanja)
        {
            foreach (MementoStanjaVezova stanje in stanjeZahtjevaMemento)
            {
                if (stanje.dohvatiStanje.nazivStanja == nazivZeljenogStanja)
                    return stanje;
            }
            return null;
        }
    }
}
