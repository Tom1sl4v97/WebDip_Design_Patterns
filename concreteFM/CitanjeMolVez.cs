using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public class CitanjeMolVez : OsnovneMetode
    {
        public override void provjeriDohvacenePodatke(string[] lines)
        {
            List<MolVez> popisMolVez = PodaciDatoteka.Instance.getListaMolVezova();
            foreach (string line in lines)
            {
                if (line == lines.First()) continue;

                string[] podaciRetka = line.Split(';');
                try
                {
                    int trenutniIdMola = int.Parse(podaciRetka[0]);
                    MolVez? postojeciKanal = popisMolVez.Find(x => x.idMol == trenutniIdMola);
                    if (postojeciKanal == null)
                    {
                        MolVez noviMolVez = new MolVez();
                        noviMolVez.idMol = trenutniIdMola;

                        string[] idVezova = podaciRetka[1].Split(',');
                        for (int i = 0; i < idVezova.Length; i++)
                        {
                            try
                            {
                                int idVeza = int.Parse(idVezova[i]);
                                Vez? postojeciVez = PodaciDatoteka.Instance.getListaVeza().Find(x => x.id == idVeza);

                                if(postojeciVez != null)
                                {
                                    noviMolVez.idVez = idVeza;
                                    PodaciDatoteka.Instance.addNoviMolVez(noviMolVez);
                                }
                                else
                                {
                                    BrojacGreske.Instance.IspisGreske("Ne postoji traženi id veza: " + idVeza + " u zapisu: " + podaciRetka[1]);
                                }

                            } catch (Exception)
                            {
                                BrojacGreske.Instance.IspisGreske("Nemoguće je parsirati id veza iz podatka: " + idVezova[i]);
                            }
                        }
                    }
                    else
                    {
                        BrojacGreske.Instance.IspisGreske("Navedeni ID mola: " + trenutniIdMola + " već postoji u popisu MolVez.");
                    }
                }
                catch (Exception e)
                {
                    BrojacGreske.Instance.IspisGreske("Neispravni redak: " + line + " u datoteci: " + NaziviDatoteka.Instance.molVezovi + " GRESKA: " + e.Message);
                }
            }

            filtrirajVisakVezova();
        }

        private void filtrirajVisakVezova()
        {
            List<Vez> popisSvihVezova = new List<Vez>(PodaciDatoteka.Instance.getListaVeza());
            List<MolVez> popisSvihMolVez = new List<MolVez>(PodaciDatoteka.Instance.getListaMolVezova());
            int counter = 0;
            foreach (Vez vez in popisSvihVezova){
                foreach (MolVez molVez in popisSvihMolVez)
                {
                    if (vez.id == molVez.idVez)
                    {
                        PodaciDatoteka.Instance.removeVez(vez);
                        counter++;
                        break;
                    }
                }
            }

            if (counter > 0)
            {
                BrojacGreske.Instance.IspisGreske("Ukupno je izbrisano " + counter + " veza iz popisa veza, jer ne posijeduju dodijeljeni kanal.");
            }

        }
    }
}
