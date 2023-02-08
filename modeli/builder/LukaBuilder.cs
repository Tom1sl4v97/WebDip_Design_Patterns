using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_1.builder
{
    public class LukaBuilder
    {
        private Luka _luka = new Luka();

        public Luka Build() => _luka;

        public LukaBuilder addNaziv(string naziv)
        {
            _luka.naziv = naziv;
            return this;
        }
        public string getNaziv()
        {
            return _luka.naziv;
        }
        public LukaBuilder addGpsSirina(double gpsSirina)
        {
            _luka.gpsSirina = gpsSirina;
            return this;
        }
        public double getGpsSirina()
        {
            return _luka.gpsSirina;
        }
        public LukaBuilder addGpsVisina(double gpsVisina)
        {
            _luka.gpsVisina = gpsVisina;
            return this;
        }

        public double getGpsVisina()
        {
            return _luka.gpsVisina;
        }

        public LukaBuilder addDubinaLuke(double dubinaLuke)
        {
            _luka.dubinaLuke = dubinaLuke;
            return this;
        }
        public double getDubinuLuke()
        {
            return _luka.dubinaLuke;
        }

        public LukaBuilder addUkupniBrojPutnickihVezova(int ukupniBrojPutnickihVezova)
        {
            _luka.ukupniBrojPutnickihVezova = ukupniBrojPutnickihVezova;
            return this;
        }

        public int getUkupniBrojPutnickihVezova()
        {
            return _luka.ukupniBrojPutnickihVezova;
        }

        public LukaBuilder addUkupniBrojPoslovnihVezova(int ukupniBrojPoslovnihVezova)
        {
            _luka.ukupniBrojPoslovnihVezova = ukupniBrojPoslovnihVezova;
            return this;
        }

        public int getUkupniBrojPoslovnihVezova()
        {
            return _luka.ukupniBrojPoslovnihVezova;
        }
        public LukaBuilder addUkupniBrojOstalihVezova(int ukupniBrojOstalihVezova)
        {
            _luka.ukupniBrojOstalihVezova = ukupniBrojOstalihVezova;
            return this;
        }

        public int getUkupniBrojOstalihVezova()
        {
            return _luka.ukupniBrojOstalihVezova;
        }

        public LukaBuilder addVirtualnoVrijeme(DateTime virtualnoVrijeme)
        {
            _luka.virtualnoVrijeme = virtualnoVrijeme;
            return this;
        }

        public DateTime getVirtualnoVrijeme()
        {
            return _luka.virtualnoVrijeme;
        }
    }
}
