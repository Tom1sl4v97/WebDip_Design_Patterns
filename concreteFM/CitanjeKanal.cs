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
            List<Kanal> popisKanala = new List<Kanal>(PodaciDatoteka.Instance.getListaKanala());
            foreach (string line in lines)
            {
                if (line == lines.First()) continue;

                string[] podaciRetka = line.Split(';');
                try
                {
                    int trenutniId = int.Parse(podaciRetka[0]);
                    Kanal? postojeciKanal = popisKanala.Find(x => x.idKanal == trenutniId);
                    if (postojeciKanal == null)
                    {
                        Kanal noviKanal = kreirajObjekt(lines);
                        
                        PodaciDatoteka.Instance.addNoviKanal(noviKanal);
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

        private Kanal kreirajObjekt(string[] lines)
        {
            Kanal noviKanal = new Kanal();

            noviKanal.idKanal = int.Parse(lines[0]);
            noviKanal.frekvencija = int.Parse(lines[1]);
            noviKanal.maksimalanBroj = int.Parse(lines[2]);

            return noviKanal;
        }
    }
}
