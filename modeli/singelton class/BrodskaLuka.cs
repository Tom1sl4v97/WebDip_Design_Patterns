using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_2.composite;

namespace ttomiek_zadaca_1
{
    internal class BrodskaLuka
    {
        private BrodskaLuka()
        {

        }

        private static BrodskaLuka? instance = null;
        private static Composite? _korijen;
        public static BrodskaLuka Instance
        {
            get
            {
                if (instance == null)
                {
                    _korijen = new Composite(PodaciDatoteka.Instance.getLuka());
                    instance = new BrodskaLuka();
                }
                return instance;
            }
        }


        public Composite dohvatiKorijen() {
            return _korijen;
        }

        public void postaviKorijen()
        {
            _korijen.postaviPovezanostMolVez(PodaciDatoteka.Instance.getListaMolVezova());

            List<Vez> popisVezova = new List<Vez>(PodaciDatoteka.Instance.getListaVeza());
            foreach(Vez vez in popisVezova)
            {
                _korijen.dodajListMolu(vez);
            }

        }
    }
}
