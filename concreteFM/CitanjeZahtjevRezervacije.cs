using ttomiek_zadaca_1.klase;
using static ttomiek_zadaca_1.zajednickeMetode.ZajednickeMetode;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public class CitanjeZahtjevRezervacije : OsnovneMetode
    {
        public override void provjeriDohvacenePodatke(string[] lines)
        {
            foreach (string line in lines)
            {
                if (line == lines.First()) continue;

                string[] podaciRetka = line.Split(';');
                try
                {
                    ZahtjevRezervacije noviZahtjevRezervacije = kreirajObjekt(podaciRetka);
                    provjeraVelicineBroda(noviZahtjevRezervacije.idBrod);
                    provjeraRasporeda(noviZahtjevRezervacije);
                }
                catch (Exception e)
                {
                    BrojacGreske.Instance.IspisGreske("Neispravni redak: " + line + " u datoteci: " + NaziviDatoteka.Instance.zahtjevRezervacije + " GRESKA: " + e.Message);
                }
            }
        }

        private ZahtjevRezervacije kreirajObjekt(string[] podaciRetka)
        {
            ZahtjevRezervacije noviZahtjevRezervacije = new();

            noviZahtjevRezervacije.idBrod = int.Parse(podaciRetka[0]);
            noviZahtjevRezervacije.datumVrijemeOd = DateTime.Parse(podaciRetka[1]);
            noviZahtjevRezervacije.trajanjePrivezaUH = double.Parse(podaciRetka[2].Replace(',', '.'));

            return noviZahtjevRezervacije;
        }

        private void provjeraVelicineBroda(int idBroda)
        {
            //Traženje odgovarajućeg broda u listi brodova
            Brod? podaciBroda = dohvatiBrodPremaID(idBroda);
            Luka podaciLuke = PodaciDatoteka.Instance.getLuka();
            //Filtriranje liste vezova po vrsti broda
            string odgovarajucaVrstaVeza = dohvatiVrstuLuke(podaciBroda.vrsta);
            List<Vez> listaVezovaPremaVrsti = PodaciDatoteka.Instance.getListaVeza().FindAll(x => x.vrsta == odgovarajucaVrstaVeza);

            //Filtriranje po dimenzijama broda
            List<Vez> popisOdgovarajucihVezova = listaVezovaPremaVrsti.FindAll(
                x => (x.maksimalnaDuljina >= podaciBroda.duljina && x.maksimalnaSirina >= podaciBroda.sirina && x.maksimalnaDubina >= podaciBroda.gaz)
                );
            if (popisOdgovarajucihVezova.Count == 0)
            {
                throw new Exception("Ne postoji prikladni vez za specifikacije brod s ID: " + idBroda);
            }
        }

        private Brod dohvatiBrodPremaID(int idBroda)
        {
            List<Brod> sviBrodovi = new List<Brod>(PodaciDatoteka.Instance.getListaBrodova());
            Brod? podaciBroda = sviBrodovi.Find(x => x.id == idBroda);
            if (podaciBroda == null)
                throw new Exception("Ne postoji brod s ID: " + idBroda);
            return podaciBroda;
        }

        public void provjeraRasporeda(ZahtjevRezervacije noviZahtjevRezervacije)
        {
            List<Vez> popisOdgovarajucihVezova = dohvatiSlobodneVezove(noviZahtjevRezervacije);

            Vez najboljiVez = pronadjiNajboljiVez(popisOdgovarajucihVezova, noviZahtjevRezervacije);
                
            //TODO Treba odabrati najbolji vez ...
            if (popisOdgovarajucihVezova.Count > 0)
            {
                noviZahtjevRezervacije.idVeza = najboljiVez.id;
                PodaciDatoteka.Instance.addNoviZahtjevRezervacije(noviZahtjevRezervacije);
                //zapisiDnevnikRada();
            }
            else
                throw new Exception("Nema slobodnih vezova u luci");
        }

        private Vez pronadjiNajboljiVez(List<Vez> popisOdgovarajucihVezova, ZahtjevRezervacije noviZahtjevRezervacije)
        {
            List<double> koeficijent = new List<double>();
            Brod noviBrod = PodaciDatoteka.Instance.getListaBrodova().Find(x => x.id == noviZahtjevRezervacije.idBrod);

            for(int i = 0; i < popisOdgovarajucihVezova.Count; i++)
            {
                double razlika1 = popisOdgovarajucihVezova[i].maksimalnaSirina - noviBrod.sirina;
                double razlika2 = popisOdgovarajucihVezova[i].maksimalnaDuljina - noviBrod.duljina;
                double razlika3 = popisOdgovarajucihVezova[i].maksimalnaDubina - noviBrod.gaz;
                koeficijent.Add(razlika1 * razlika2 * razlika3);
            }
            
            List<Vez> najboljiVezovi = new List<Vez>();
            double najmanji = koeficijent.Max();
            for (int j = 0; j < koeficijent.Count; j++)
            {
                if(koeficijent[j] <= najmanji)
                {
                    najmanji = koeficijent[j];
                    najboljiVezovi.Add(popisOdgovarajucihVezova[j]);
                }
            }

            if (najboljiVezovi.Count == 1)
            {
                ispisNajboljeg(noviBrod, najboljiVezovi[0]);
                return najboljiVezovi[0];
            }
            else
            {
                int index = 0;
                for (int z = 0; z < najboljiVezovi.Count; z++)
                {
                    if(najboljiVezovi[z].cijenaVezaPoSatu < najboljiVezovi[index].cijenaVezaPoSatu)
                    {
                        index = z;
                    }
                }
                ispisNajboljeg(noviBrod, najboljiVezovi[index]);
                return najboljiVezovi[index];
            }
        }

        private void ispisNajboljeg(Brod? noviBrod, Vez vez)
        {
            Console.WriteLine("Podaci broda: ");
            Console.WriteLine(noviBrod.id + " | " + noviBrod.naziv + " | " + noviBrod.vrsta + " | " + noviBrod.duljina + " | " + noviBrod.sirina + " | " + noviBrod.gaz);
            // id;oznaka_veza;vrsta;cijena_veza_po_satu;maksimalna_duljina;maksimalna_sirina;maksimalna_dubina
            Console.WriteLine(vez.id + " | " + vez.oznakaVeza + " | " + vez.vrsta + " | " + vez.cijenaVezaPoSatu + " | " + vez.maksimalnaDuljina + " | " + vez.maksimalnaSirina + " | " + vez.maksimalnaDubina);
        }

        public List<Vez> dohvatiSlobodneVezove(ZahtjevRezervacije noviZahtjevRezervacije)
        {
            int idBroda = noviZahtjevRezervacije.idBrod;
            DateTime trenutnoVV = noviZahtjevRezervacije.datumVrijemeOd;

            //Provjera duplikata
            DateTime krajnjeVrijeme = trenutnoVV.AddHours(noviZahtjevRezervacije.trajanjePrivezaUH);
            List<ZahtjevRezervacije> sviZahtejvi = new List<ZahtjevRezervacije>(PodaciDatoteka.Instance.getListaZahtjevaRezervacije());
            List<ZahtjevRezervacije> popisZauzetih = sviZahtejvi.FindAll(x => x.idBrod == idBroda);
            provjeraDuplikata(popisZauzetih, trenutnoVV, krajnjeVrijeme);

            //Dohvaćam listu mogućih vezova za privez broda
            Brod? podaciBroda = dohvatiBrodPremaID(idBroda);
            List<Raspored> sviRasporedi = new List<Raspored>(PodaciDatoteka.Instance.getListaRasporeda());
            TimeOnly vrijeme = TimeOnly.FromDateTime(trenutnoVV);
            int danUTjednu = (int)trenutnoVV.DayOfWeek;
            List<Raspored> popisRasporeda = new List<Raspored>(sviRasporedi.FindAll(x => x.daniUTjednu == danUTjednu && x.vrijemeOd <= vrijeme && x.vrijemeDo >= vrijeme));
            //Nije filtrirano po zahtjevima rezervacija
            List<Vez> popisMogucihVezova = new List<Vez>(dohvatiSlobodneVezove(popisRasporeda, podaciBroda));

            //Dohvaćam koliko se je brodova privezalo na traženu vrstu veza u vremenu kada se novi brod želi privezati na vez
            //Piftrirano prema zahtjevima rezervacije
            List<Vez> popisOdgovarajucihVezova = new List<Vez>(popisMogucihVezova);
            foreach (Vez vez in popisMogucihVezova)
            {
                foreach (ZahtjevRezervacije zr in sviZahtejvi)
                {
                    DateTime kv = zr.datumVrijemeOd.AddHours(zr.trajanjePrivezaUH);
                    if (vez.id == zr.idVeza && trenutnoVV <= kv && krajnjeVrijeme >= zr.datumVrijemeOd)
                    {
                        popisOdgovarajucihVezova.Remove(vez);
                        break;
                    }
                }
            }
            return popisOdgovarajucihVezova;
        }
        
        private List<Vez> dohvatiSlobodneVezove(List<Raspored> popisRasporeda, Brod podaciBroda)
        {
            string odgovarajucaVrstaVeza = dohvatiVrstuLuke(podaciBroda.vrsta);

            List<Vez> sviVezovi = new List<Vez>(PodaciDatoteka.Instance.getListaVeza().FindAll(x => x.vrsta == odgovarajucaVrstaVeza));
            List<Vez> popisVezova = sviVezovi.FindAll(x => x.maksimalnaDuljina >= podaciBroda.duljina && x.maksimalnaSirina >= podaciBroda.sirina && x.maksimalnaDubina >= podaciBroda.gaz);
            List<Vez> pomocnaLista = new List<Vez>(popisVezova);
            //Popis slobodnih vezova
            foreach (Vez vez in popisVezova)
            {
                foreach (Raspored r in popisRasporeda)
                {
                    if (vez.id == r.idVez)
                        pomocnaLista.Remove(vez);
                }
            }
            return pomocnaLista;
        }

        private void provjeraDuplikata(List<ZahtjevRezervacije> popisZauzetih, DateTime trenutnoVV, DateTime krajnjeVrijeme)
        {
            foreach (ZahtjevRezervacije zr in popisZauzetih)
            {
                DateTime kv = zr.datumVrijemeOd.AddHours(zr.trajanjePrivezaUH);
                if (trenutnoVV <= kv && krajnjeVrijeme >= zr.datumVrijemeOd)
                    throw new Exception("Brod ne može biti na dva mjesta u traženom periodu");
            }
        }
    }
}
