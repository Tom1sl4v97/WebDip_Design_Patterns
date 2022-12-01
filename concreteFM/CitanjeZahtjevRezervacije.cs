using System.Collections.Generic;
using System.Text.RegularExpressions;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.singelton_class;
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
            noviZahtjevRezervacije.trajanjePrivezaUH = int.Parse(podaciRetka[2]);

            return noviZahtjevRezervacije;
        }

        private void provjeraVelicineBroda(int idBroda)
        {
            //Traženje odgovarajućeg broda u listi brodova
            Brod? podaciBroda = dohvatiBrodPremaID(idBroda);
            Luka podaciLuke = PodaciDatoteka.Instance.getLuka();
            //Filtriranje liste vezova po vrsti broda
            string odgovarajucaVrstaVeza = dohvatiOdgovarajucuVrstuVeza(podaciBroda.vrsta);
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

        private string dohvatiOdgovarajucuVrstuVeza(string vrstaBroda)
        {
            switch (vrstaBroda)
            {
                case "TR" or "KA" or "KL" or "KR":
                    return "PU";
                case "RI" or "TE":
                    return "PO";
                case "JA" or "BR" or "RO":
                    return "OS";
                default:
                    throw new Exception("Neispravna oznaka vrste broda: " + vrstaBroda);
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
            int idBroda = noviZahtjevRezervacije.idBrod;
            DateTime trenutnoVV = noviZahtjevRezervacije.datumVrijemeOd;
            TimeOnly vrijeme = TimeOnly.FromDateTime(trenutnoVV);
            int danUTjednu = (int)trenutnoVV.DayOfWeek;
            
            Brod? podaciBroda = dohvatiBrodPremaID(idBroda);

            List<Raspored> sviRasporedi = new List<Raspored>(PodaciDatoteka.Instance.getListaRasporeda());
            List<Raspored> popisRasporeda = new List<Raspored>(sviRasporedi.FindAll(x => x.daniUTjednu == danUTjednu && x.vrijemeOd <= vrijeme && x.vrijemeDo >= vrijeme));

            List<Vez> pomocnaLista = new List<Vez>(dohvatiSlobodneVezove(popisRasporeda, podaciBroda));

            int brojacZauzetihVezova = 0;
            DateTime krajnjeVrijeme = trenutnoVV.AddHours(noviZahtjevRezervacije.trajanjePrivezaUH);
            List<ZahtjevRezervacije> sviZahtejvi = new List<ZahtjevRezervacije>(PodaciDatoteka.Instance.getListaZahtjevaRezervacije());
            List<ZahtjevRezervacije> popisZauzetih = sviZahtejvi.FindAll(x => x.idBrod == idBroda);

            provjeraDuplikata(popisZauzetih, trenutnoVV, krajnjeVrijeme);

            foreach (ZahtjevRezervacije zr in sviZahtejvi)
            {
                DateTime kv = zr.datumVrijemeOd.AddHours(zr.trajanjePrivezaUH);
                if (trenutnoVV <= kv && krajnjeVrijeme >= zr.datumVrijemeOd)
                    brojacZauzetihVezova++;
            }

            if (brojacZauzetihVezova < pomocnaLista.Count)
                PodaciDatoteka.Instance.addNoviZahtjevRezervacije(noviZahtjevRezervacije);
            else
                throw new Exception("Nema slobodnih vezova u luci");
        }

        private List<Vez> dohvatiSlobodneVezove(List<Raspored> popisRasporeda, Brod podaciBroda)
        {
            string odgovarajucaVrstaVeza = dohvatiOdgovarajucuVrstuVeza(podaciBroda.vrsta);

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
