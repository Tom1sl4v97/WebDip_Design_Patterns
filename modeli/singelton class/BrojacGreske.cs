using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;
using ttomiek_zadaca_2.view;

namespace ttomiek_zadaca_1
{
    public sealed class BrojacGreske
    {
        private BrojacGreske() 
        {

        }

        private static BrojacGreske? _instance = null;
        private static int _brojGresaka = 0;
        private static Queue<Greske> _queueGreske;
        private static int _brojRedaka;
        public static BrojacGreske Instance
        {
            get
            {
                if (_instance == null)
                {
                    _queueGreske = new Queue<Greske>();
                    postaviBrojRedaka();
                    _instance = new BrojacGreske();
                }
                return _instance;
            }
        }

        private static void postaviBrojRedaka()
        {
            _brojRedaka = ZajednickeMetode.DohvatiBrojRedakaGreski();
        }

        public void dodajGreskeUcitavanjaCSVDatoteki(string opis)
        {
            dodajNovuGesku(opis, false);

            IspisPocetnihGreski.ispisPocetnihUcitavanja();
        }

        public void dodajNovuGesku(string opis, bool potvrda = true)
        {
            if (_queueGreske.Count >= _brojRedaka)
            {
                _queueGreske.Dequeue();
            }

            _brojGresaka++;
            Greske novaGreska = new Greske(_brojGresaka, opis);
            _queueGreske.Enqueue(novaGreska);

            if (potvrda)
            {
                IspisPocetnihGreski.ispisGresaka();
                IspisPocetnihGreski.ispisPocetneKomande();
                Console.Write("\x1b[K");
            }
        }

        public Queue<Greske> dohvatiPopisGreski()
        {
            return _queueGreske;
        }
    }
}
