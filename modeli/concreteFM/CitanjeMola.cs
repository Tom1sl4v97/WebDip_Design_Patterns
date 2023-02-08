using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_2.composite;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public class CitanjeMola : OsnovneMetode
    {
        public override void provjeriDohvacenePodatke(string[] lines)
        {
            //List<Mol> popisMolova = PodaciDatoteka.Instance.getListaMolova();
            List<Mol> popisMolova = new List<Mol>();
            popisMolova = BrodskaLuka.Instance.dohvatiKorijen().dohvatiPopisMolova();
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

                        popisMolova.Add(noviMol);
                        BrodskaLuka.Instance.dohvatiKorijen().dodajNoviElement(new Composite(noviMol));
                        //PodaciDatoteka.Instance.addNoviMol(noviMol);
                    }
                    else
                    {
                        BrojacGreske.Instance.dodajGreskeUcitavanjaCSVDatoteki("Navedeni ID mola: " + trenutniId + " već postoji u popisu molova.");
                    }
                }
                catch (Exception e)
                {
                    BrojacGreske.Instance.dodajGreskeUcitavanjaCSVDatoteki("Neispravni redak: " + line + " u datoteci: " + NaziviDatoteka.Instance.mol + " GRESKA: " + e.Message);
                }
            }
        }
    }
}
