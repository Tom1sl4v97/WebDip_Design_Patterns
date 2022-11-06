using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_1.@interface
{
    public interface DostupnostVezaInterface
    {
        List<ZahtjevRezervacije> naredbaDostupnosti(Vez vez, DateTime datumOd, DateTime datumDo, List<ZahtjevRezervacije> pomocnaListaZahtjeva);
    }
}
