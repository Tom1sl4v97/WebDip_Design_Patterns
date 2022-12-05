using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public class CitanjeMolVez : OsnovneMetode
    {
        public override void provjeriDohvacenePodatke(string[] lines)
        {
            foreach (string line in lines)
            {
                if (line == lines.First()) continue;

                string[] podaciRetka = line.Split(';');
                try
                {
                    List<MolVez> popisMolVez = new List<MolVez>(PodaciDatoteka.Instance.getListaMolVezova());
                    int trenutniIdMola = int.Parse(podaciRetka[0]);
                    MolVez? postojeciKanal = popisMolVez.Find(x => x.idMol == trenutniIdMola);
                    if (postojeciKanal == null)
                    {
                        string[] idVezova = podaciRetka[1].Split(',');
                        for (int i = 0; i < idVezova.Length; i++)
                        {
                            try
                            {
                                List<Vez> popisSvihVezova = new List<Vez>(PodaciDatoteka.Instance.getListaVeza());
                                int idVeza = int.Parse(idVezova[i]);
                                Vez? postojeciVez = popisSvihVezova.Find(x => x.id == idVeza);

                                if(postojeciVez != null)
                                {
                                    if (!provjeriPostojili(idVeza))
                                    {
                                        MolVez noviMolVez = new MolVez();
                                        noviMolVez.idMol = trenutniIdMola;
                                        noviMolVez.idVez = idVeza;
                                        PodaciDatoteka.Instance.addNoviMolVez(noviMolVez);
                                    }else
                                    {
                                        BrojacGreske.Instance.IspisGreske("Navedeni ID veze: " + idVeza + " već postoji u popisu veza molova.");
                                    }
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

        private bool provjeriPostojili(int idVeza)
        {
            List<MolVez> popisSvihMolVezova = new List<MolVez>(PodaciDatoteka.Instance.getListaMolVezova());
            MolVez odgovarajuciMolVeza = popisSvihMolVezova.Find(x => x.idVez == idVeza);

            if (odgovarajuciMolVeza != null)
                return true;
            return false;
        }

        private void filtrirajVisakVezova()
        {
            List<Vez> popisSvihVezova = new List<Vez>(PodaciDatoteka.Instance.getListaVeza());
            int counter = 0;
            foreach (Vez vez in popisSvihVezova){
                if (!provjeriPostojili(vez.id))
                {
                    provjeriPostojili(vez.id);
                    counter++;
                    PodaciDatoteka.Instance.removeVez(vez);
                }
            }

            if (counter > 0)
            {
                BrojacGreske.Instance.IspisGreske("Ukupno je izbrisano " + counter + " veza iz popisa veza, jer ne posijeduju dodijeljeni kanal.");
            }

        }

        private void ispisiSveMolVeze()
        {
            List<MolVez> popis = new List<MolVez>(PodaciDatoteka.Instance.getListaMolVezova());
            List<MolVez> popissvih = new List<MolVez>(popis);

            foreach (MolVez mv in popissvih)
            {
                Console.WriteLine(mv.idVez + " " + mv.idMol);
            }
        }
    }
}
