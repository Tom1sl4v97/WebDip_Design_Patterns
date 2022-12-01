using System.Text.RegularExpressions;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public class CitanjeVez : OsnovneMetode
    {
        private int brojacPU = 0;
        private int brojacPO = 0;
        private int brojacOS = 0;
        
        public override void provjeriDohvacenePodatke(string[] lines)
        {
            List<Vez> popisVezova = PodaciDatoteka.Instance.getListaVeza();
            Vez noviVez = new();
            foreach (string line in lines)
            {
                if (line == lines.First()) continue;

                string[] podaciRetka = line.Split(';');
                try
                {
                    int trenutniId = int.Parse(podaciRetka[0]);
                    Vez? vez = popisVezova.Find(x => x.id == trenutniId);
                    if (vez == null)
                    {
                        noviVez = kreirajObjekt(podaciRetka);
                        provjeriKompatibilnostVeza(noviVez);
                        PodaciDatoteka.Instance.addNoviVez(noviVez);
                    }
                    else
                    {
                        BrojacGreske.Instance.IspisGreske("Navedeni ID veza: " + trenutniId + " već postoji u popisu vezova.");
                    }
                }
                catch (Exception e)
                {
                    BrojacGreske.Instance.IspisGreske("Neispravni redak: " + line + " u datoteci: " + NaziviDatoteka.Instance.vez + " GRESKA: " + e.Message);
                }
            }
        }

        private Vez kreirajObjekt(string[] podaciRetka)
        {
            Vez noviVez = new();
            noviVez.id = int.Parse(podaciRetka[0]);
            noviVez.oznakaVeza = podaciRetka[1];
            noviVez.vrsta = podaciRetka[2];
            noviVez.cijenaVezaPoSatu = decimal.Parse(podaciRetka[3]);
            noviVez.maksimalnaDuljina = int.Parse(podaciRetka[4]);
            noviVez.maksimalnaSirina = int.Parse(podaciRetka[5]);
            noviVez.maksimalnaDubina = int.Parse(podaciRetka[6]);

            return noviVez;
        }

        private void provjeriKompatibilnostVeza(Vez noviVez)
        {
            Luka luka = PodaciDatoteka.Instance.getLuka();
            
            if (luka.dubinaLuke <= noviVez.maksimalnaDubina)
                throw new Exception("Dubina veza (" + noviVez.maksimalnaDubina + ") je veća od dubine luke (" + luka.dubinaLuke + ")!");

            switch (noviVez.vrsta)
            {
                case "PU": provjeraPU(luka); break;
                case "PO": provjeraPO(luka); break;
                case "OS": provjeraOS(luka); break;
                default: throw new Exception("Vrsta veza je neispravna: " + noviVez.vrsta);
            }
        }

        private void provjeraPU(Luka luka)
        {
            if (brojacPU < luka.ukupniBrojPutnickihVezova)
                brojacPU++;
            else throw new Exception("Maksimalni kapacitet putničkih (PU) vezova u luci je dosegnut!");
        }

        private void provjeraPO(Luka luka)
        {
            if (brojacPO < luka.ukupniBrojPoslovnihVezova)
                brojacPO++;
            else throw new Exception("Maksimalni kapacitet poslovnih (PO) vezova u luci je dosegnut!");
        }

        private void provjeraOS(Luka luka)
        {
            if (brojacOS < luka.ukupniBrojOstalihVezova)
                brojacOS++;
            else throw new Exception("Maksimalni kapacitet ostalih (OS) vezova u luci je dosegnut!");
        }

    }
}
