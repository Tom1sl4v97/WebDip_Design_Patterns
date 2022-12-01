using System.Text.RegularExpressions;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public class CitanjeMolVez : OsnovneMetode
    {
        public override void provjeriDohvacenePodatke(string[] lines)
        {
            List<MolVez> popisMolVez = new List<MolVez>(PodaciDatoteka.Instance.getListaMolVezova());
            foreach (string line in lines)
            {
                if (line == lines.First()) continue;

                string[] podaciRetka = line.Split(';');
                try
                {
                    int trenutniIdMola = int.Parse(podaciRetka[0]);
                    int trenutniIdVeza = int.Parse(podaciRetka[1]);
                    MolVez? postojeciKanal = popisMolVez.Find(x => x.idMol == trenutniIdMola && x.idVez == trenutniIdVeza);
                    if (postojeciKanal == null)
                    {
                        MolVez noviMolVez = new MolVez();
                        noviMolVez.idMol = trenutniIdMola;
                        noviMolVez.idVez = trenutniIdVeza;

                        PodaciDatoteka.Instance.addNoviMolVez(noviMolVez);
                    }
                    else
                    {
                        BrojacGreske.Instance.IspisGreske("Navedeni ID mola: " + trenutniIdMola + " i navedeni ID veza: " + trenutniIdVeza + " već postoji u popisu MolVez.");
                    }
                }
                catch (Exception e)
                {
                    BrojacGreske.Instance.IspisGreske("Neispravni redak: " + line + " u datoteci: " + NaziviDatoteka.Instance.molVezovi + " GRESKA: " + e.Message);
                }
            }
        }
    }
}
