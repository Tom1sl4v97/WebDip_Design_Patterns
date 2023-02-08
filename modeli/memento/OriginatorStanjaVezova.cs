using ttomiek_zadaca_1;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.singelton_class;

namespace ttomiek_zadaca_2.memento
{
    public class OriginatorStanjaVezova
    {
        #nullable disable
        StanjeZahtjevaRezervacije _stanjeVezova;
        public StanjeZahtjevaRezervacije stanjeVezova
        {
            get { return _stanjeVezova; }
            set
            {
                if (value is not null)
                    _stanjeVezova = value;
                else
                    BrojacGreske.Instance.dodajNovuGesku("Niste unjeli naziv stanja, koji je potreban kako bi se mogao pronaći kod vraćanja stanja!");
            }
        }
        public MementoStanjaVezova kreirajMementoStanjaVezova()
        {
            return (new MementoStanjaVezova(_stanjeVezova));
        }
        public void setajMementoStanjaVezova(MementoStanjaVezova mementoStanjaVezova)
        {
            stanjeVezova = mementoStanjaVezova.dohvatiStanje;

            VirtualniSat.Instance.virtualnoVrijeme(_stanjeVezova.vrijeme);
            PodaciDatoteka.Instance.postaviNovuListuZahtjevaRezervacije(_stanjeVezova.listaZahtjevaRezervacije);
        }
    }
}
