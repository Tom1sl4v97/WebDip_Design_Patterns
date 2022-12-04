using System.Text.RegularExpressions;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public class CitanjeMola : OsnovneMetode
    {
        public override void provjeriDohvacenePodatke(string[] lines)
        {
            List<Mol> popisMolova = PodaciDatoteka.Instance.getListaMolova();
            foreach (string line in lines)
            {
                if (line == lines.First()) continue;

                string[] podaciRetka = line.Split(';');
                try
                {
                    int trenutniId = int.Parse(podaciRetka[0]);
                    Mol? postojeciMol = popisMolova.Find(x => x.idMol == trenutniId);
                    if (postojeciMol == null)
                    {
                        Mol noviMol = new Mol();
                        noviMol.idMol = trenutniId;
                        noviMol.naziv = podaciRetka[1];

                        PodaciDatoteka.Instance.addNoviMol(noviMol);
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
