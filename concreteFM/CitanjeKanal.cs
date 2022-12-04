using System.Text.RegularExpressions;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public class CitanjeKanal : OsnovneMetode
    {
        public override void provjeriDohvacenePodatke(string[] lines)
        {
            List<Kanal> popisKanala =PodaciDatoteka.Instance.getListaKanala();
            foreach (string line in lines)
            {
                if (line == lines.First()) continue;

                string[] podaciRetka = line.Split(';');
                try
                {
                    int trenutniId = int.Parse(podaciRetka[0]);
                    int trenutnaFrekvencija = int.Parse(podaciRetka[1]);
                    Kanal? postojeciKanal = popisKanala.Find(x => x.idKanal == trenutniId || x.frekvencija == trenutnaFrekvencija);
                    if (postojeciKanal == null || postojeciKanal.idKanal != trenutniId)
                    {
                        if (postojeciKanal == null || postojeciKanal.frekvencija != trenutnaFrekvencija)
                        {
                            Kanal noviKanal = new Kanal();

                            noviKanal.idKanal = trenutniId;
                            noviKanal.frekvencija = trenutnaFrekvencija;
                            noviKanal.maksimalanBroj = int.Parse(podaciRetka[2]);

                            PodaciDatoteka.Instance.addNoviKanal(noviKanal);
                        }else
                        {
                            BrojacGreske.Instance.IspisGreske("Navedena frekvencija: " + trenutnaFrekvencija + " već postoji u popisu kanala.");
                        }
                    }
                    else
                    {
                        BrojacGreske.Instance.IspisGreske("Navedeni ID mola: " + trenutniId + " već postoji u popisu molova.");
                    }
                }
                catch (Exception e)
                {
                    BrojacGreske.Instance.IspisGreske("Neispravni redak: " + line + " u datoteci: " + NaziviDatoteka.Instance.mol + " GRESKA: " + e.Message);
                }
            }
        }
    }
}
