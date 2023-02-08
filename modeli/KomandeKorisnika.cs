using System.Text.RegularExpressions;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.chainOfResponsbility;
using ttomiek_zadaca_1.ConcreteFM;
using ttomiek_zadaca_1.ConcrreteFM;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.komandaV;
using ttomiek_zadaca_1.observer;
using ttomiek_zadaca_1.singelton_class;
using ttomiek_zadaca_1.visitor;
using ttomiek_zadaca_1.zajednickeMetode;
using ttomiek_zadaca_1;
using ttomiek_zadaca_2.view;
using ttomiek_zadaca_2.composite;
using ttomiek_zadaca_2.memento;

namespace ttomiek_zadaca_3.modeli
{
    public static class KomandeKorisnika
    {
        private static bool _potvrdaKomande = false;
        private static bool _provjeraFrekvencije = true;
        private static int _counterStatusaVeza = 0;
        private static int _ukupnoZauzetihVezova = 0;
        private static List<string> _popisRedakaIspisa = new();

        public static void izvrsiKomdanuKorisnika()
        {
            string regexPatternKomandi = "^(?=.*\\s?Q(?<krajPrograma>))?(?=.*\\s?I(?<status>))?(?=.*\\s?VR (?<virtualnoVrijeme>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2}):(\\d{2})))?(?=.*\\s?V (?<zauzetostVeza>(?<vrstaVeza>([A-Z]{2,2})) (?<statusVeza>[Z|S]) (?<datumOd>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2}):(\\d{2})) (?<datumDo>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2}):(\\d{2}))))?(?=.*\\s?UR (?<ucitajDatoteku>[a-zA-Z0-9!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]{3,1000}))?(?=.*\\s?ZP (?<zahtjevPriveza>(?<idBrodaSpajanja>[0-9]{1,3}) (?<trajanje>[0-9]{1,3})))?(?=.*\\s?ZD (?<zatraziDozvolu>[0-9]{1,3}))?(?=.*\\s?F (?<spajanjeKanalu>(?<idBroda>[0-9]{1,3}) (?<idKanala>[0-9]{1,3})(?<odjavljivanje> [Q])?))?(?=.*\\s?ZA (?<vrijeme>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2})))?(?=.*\\s?T(?<tablice>(?=.* (?<Z>Z))?(?=.* (?<P>P))?(?=.* (?<RB>RB))?))?(?=.*\\s?VF (?<vlastitaFunkcionalnost>[a-zA-Z ]{0,100}))?(?=.*\\s?SPS \"(?<spremanjeStanja>[a-zA-Z0-9\\s!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]{1,1000})\")?(?=.*\\s?VPS \"(?<vracanjeStanja>[a-zA-Z0-9\\s!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]{1,1000})\")?";

            List<Mol> popisMolova = BrodskaLuka.Instance.dohvatiKorijen().dohvatiPopisMolova();

            while (true)
            {
                string? uneseniString = Console.ReadLine();
                
                Match match = new Regex(regexPatternKomandi).Match(uneseniString);
                _potvrdaKomande = false;
                _provjeraFrekvencije = true;

                if (match.Groups["status"].Success) pregledStatusa();
                if (match.Groups["virtualnoVrijeme"].Success) postaviVirtualnoVrijeme(match.Groups["virtualnoVrijeme"].Value);
                if (match.Groups["zauzetostVeza"].Success) ispisZauzetostiVeza(match.Groups["zauzetostVeza"].Value);
                if (match.Groups["ucitajDatoteku"].Success) ucitajPodatkeZahtjevaRezervacije(match.Groups["ucitajDatoteku"].Value);
                if (match.Groups["zahtjevPriveza"].Success) zahtjevPriveza(match.Groups["zahtjevPriveza"].Value);
                if (match.Groups["zatraziDozvolu"].Success) zatraziDozvolu(int.Parse(match.Groups["zatraziDozvolu"].Value));
                if (match.Groups["spajanjeKanalu"].Success) spajanjeKanala(match.Groups["spajanjeKanalu"].Value);
                if (match.Groups["vrijeme"].Success) ukupniBrojZauzetihVezova(match.Groups["vrijeme"].Value);
                if (match.Groups["tablice"].Success) uredivanjeTablica(match.Groups["Z"].Value, match.Groups["P"].Value, match.Groups["RB"].Value);
                if (match.Groups["vlastitaFunkcionalnost"].Success) vlastitaFunkcionalnost(match.Groups["vlastitaFunkcionalnost"].Value);
                if (match.Groups["spremanjeStanja"].Success) spremanjeStanja(match.Groups["spremanjeStanja"].Value);
                if (match.Groups["vracanjeStanja"].Success) vracanjeStanja(match.Groups["vracanjeStanja"].Value);
                if (match.Groups["krajPrograma"].Success && _provjeraFrekvencije) krajPrograma();
                if (!_potvrdaKomande) BrojacGreske.Instance.dodajNovuGesku("Pogrešna komanda, molimo unesite ispravne komande!");
            }
        }

        public static void dodajRedoveZaIspis(List<string> popisRedaka)
        {
            _popisRedakaIspisa.AddRange(popisRedaka);
        }

        private static void pregledStatusa()
        {
            _potvrdaKomande = true;
            int trenutniBrojRedovaIspisa = _popisRedakaIspisa.Count;

            _popisRedakaIspisa.AddRange(IspisRedovaRada.ispisZaglavlja());

            List<Vez> popisSvihVezova = new List<Vez>(PodaciDatoteka.Instance.getListaVeza());
            DateTime trenutnoVrijeme = VirtualniSat.Instance.virtualnoVrijeme();
            _popisRedakaIspisa.AddRange(ZajednickeMetode.ispisTrenutnogStatusa(popisSvihVezova, trenutnoVrijeme));

            _popisRedakaIspisa.AddRange(IspisRedovaRada.ispisPodnozja(popisSvihVezova.Count));

            IspisRedovaRada.ispisRedaka(_popisRedakaIspisa, trenutniBrojRedovaIspisa);
        }

        private static void postaviVirtualnoVrijeme(string value)
        {
            _potvrdaKomande = true;
            int trenutniBrojRedovaIspisa = _popisRedakaIspisa.Count;

            try
            {
                VirtualniSat.Instance.virtualnoVrijeme(DateTime.Parse(value));
                _popisRedakaIspisa.Add("Virtualno vrijeme postavljeno na: " + value);

                IspisRedovaRada.ispisRedaka(_popisRedakaIspisa, trenutniBrojRedovaIspisa);
            }
            catch (Exception e)
            {
                BrojacGreske.Instance.dodajNovuGesku(e.Message);
            }
        }

        private static List<string>? _pomocnaListaRedakaIspisa;

        public static void dodajRedakZaPomocnuListu(string redak)
        {
            if (_pomocnaListaRedakaIspisa == null)
                _pomocnaListaRedakaIspisa = new List<string>();
            _pomocnaListaRedakaIspisa.Add(redak);
        }

        public static void dodajRedakZaPomocnuListu(List<string> popisIspisa)
        {
            if (_pomocnaListaRedakaIspisa == null)
                _pomocnaListaRedakaIspisa = new List<string>();
            _pomocnaListaRedakaIspisa.AddRange(popisIspisa);
        }
        private static void ispisZauzetostiVeza(string value)
        {
            _potvrdaKomande = true;
            _pomocnaListaRedakaIspisa = new();
            int trenutniBrojRedovaIspisa = _popisRedakaIspisa.Count;

            string regexPattern = "^(?<vrstaVeza>([A-Z]{2,2})) (?<statusVeza>[Z|S]) (?<datumOd>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2}):(\\d{2})) (?<datumDo>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2}):(\\d{2}))";
            Match match = new Regex(regexPattern).Match(value);

            string vrstaVeza = match.Groups["vrstaVeza"].Value;
            string statusVeza = match.Groups["statusVeza"].Value;
            DateTime datumOd = DateTime.Parse(match.Groups["datumOd"].Value);
            DateTime datumDo = DateTime.Parse(match.Groups["datumDo"].Value);

            List<ZahtjevRezervacije> popisListeZahtjeva = new List<ZahtjevRezervacije>();
            List<Vez> popisOdgovarajucihVezova = PodaciDatoteka.Instance.getListaVeza().FindAll(x => x.vrsta == vrstaVeza);

            _counterStatusaVeza = 1;

            try
            {
                ZajednickeMetode.provjeriVrstuVeza(vrstaVeza);

                List<ZahtjevRezervacije> pomocnaListaZahtjeva = filtrirajZahtjeve(vrstaVeza);
                popisListeZahtjeva = filtrirajZahtjeve(vrstaVeza);

                _popisRedakaIspisa.AddRange(IspisRedovaRada.ispisZaglavlja());

                foreach (Vez vez in popisOdgovarajucihVezova)
                {
                    if (statusVeza == "Z")
                    {
                        popisListeZahtjeva = ProvjeraZauzetihVezova.provjeraVezova(vez, datumOd, datumDo, pomocnaListaZahtjeva, _counterStatusaVeza++);
                    }
                    else if (statusVeza == "S")
                    {
                        popisListeZahtjeva = ProvjeraSlobodnihVezova.provjeraVezova(vez, datumOd, datumDo, pomocnaListaZahtjeva, _counterStatusaVeza++);
                    }
                    else
                    {
                        throw new Exception("Uneseni status veza je pogrešan: " + statusVeza);
                    }
                }
                _popisRedakaIspisa.AddRange(_pomocnaListaRedakaIspisa);
                _popisRedakaIspisa.AddRange(IspisRedovaRada.ispisPodnozja(_counterStatusaVeza - 1));

                IspisRedovaRada.ispisRedaka(_popisRedakaIspisa, trenutniBrojRedovaIspisa);
            }
            catch (Exception e)
            {
                BrojacGreske.Instance.dodajNovuGesku(e.Message);
            }
        }

        private static List<ZahtjevRezervacije> filtrirajZahtjeve(string vrstaVeza)
        {
            List<Brod> sviBrodovi = new List<Brod>(PodaciDatoteka.Instance.getListaBrodova());
            List<ZahtjevRezervacije> pomocnaListaZahtjeva = new List<ZahtjevRezervacije>();
            foreach (ZahtjevRezervacije zahtjev in PodaciDatoteka.Instance.getListaZahtjevaRezervacije())
            {
                Brod? podaciBroda = sviBrodovi.Find(x => x.id == zahtjev.idBrod);
                string vrstaVezaBroda = ZajednickeMetode.dohvatiVrstuLuke(podaciBroda.vrsta);
                if (podaciBroda != null && vrstaVezaBroda == vrstaVeza)
                {
                    pomocnaListaZahtjeva.Add(zahtjev);
                }
            }
            return pomocnaListaZahtjeva;
        }

        private static void ucitajPodatkeZahtjevaRezervacije(string nazivDatoteke)
        {
            _potvrdaKomande = true;
            int trenutniBrojRedovaIspisa = _popisRedakaIspisa.Count;

            NaziviDatoteka.Instance.zahtjevRezervacije = nazivDatoteke;
            CitanjeDatotekeInterface citanjeZahtjevaRezervacije = new CitanjeZahtjevaRezervacijeFactory().CreateCitac();
            citanjeZahtjevaRezervacije.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.zahtjevRezervacije, ZahtjevRezervacije.PATTERN_INFO_RETKA_CSV);

            _popisRedakaIspisa.Add("Učitani podaci za ZAHTJEVE REZERVACIJE: " + PodaciDatoteka.Instance.getListaZahtjevaRezervacije().Count);

            IspisRedovaRada.ispisRedaka(_popisRedakaIspisa, trenutniBrojRedovaIspisa);
        }

        private static void zahtjevPriveza(string value)
        {
            _potvrdaKomande = true;
            int trenutniBrojRedovaIspisa = _popisRedakaIspisa.Count;

            string[] uneseniPodaci = value.Split(' ');
            int idBroda = int.Parse(uneseniPodaci[0]);

            DnevnikRada? dr = PodaciDatoteka.Instance.getListaDnevnikaRada().Find(x => x.idBrod == idBroda && x.odobrenZahtjev && !x.slobodan);

            if (dr == null)
                BrojacGreske.Instance.dodajNovuGesku("Traženi brod nije u niti jednom kanalu.");
            else
            {
                try
                {
                    CitanjeZahtjevRezervacije czr = new CitanjeZahtjevRezervacije();
                    ZahtjevRezervacije zr = new();
                    zr.idBrod = idBroda;
                    zr.datumVrijemeOd = VirtualniSat.Instance.virtualnoVrijeme();
                    zr.trajanjePrivezaUH = int.Parse(uneseniPodaci[1]);
                    czr.provjeraRasporeda(zr);

                    _popisRedakaIspisa.Add("Zahtjev za privezivanje broda sa ID-om: " + zr.idBrod + " je uspješno dodan u listu zahtjeva.");
                    IspisRedovaRada.ispisRedaka(_popisRedakaIspisa, trenutniBrojRedovaIspisa);
                }
                catch (Exception e)
                {
                    BrojacGreske.Instance.dodajNovuGesku(e.Message);
                }
            }
        }

        private static void zatraziDozvolu(int value)
        {
            _potvrdaKomande = true;
            int trenutniBrojRedovaIspisa = _popisRedakaIspisa.Count;

            DnevnikRada? dr = PodaciDatoteka.Instance.getListaDnevnikaRada().Find(x => x.idBrod == value && x.odobrenZahtjev && !x.slobodan);

            try
            {
                if (dr == null)
                    throw new Exception("Traženi brod nije u niti jednom kanalu.");

                Brod? brod = PodaciDatoteka.Instance.getListaBrodova().Find(x => x.id == value);
                if (brod == null)
                    throw new Exception("Ne postoji brod sa traženim ID: " + value);
                else
                {
                    _popisRedakaIspisa.Add(provjeraRezervacije(value, brod));
                    IspisRedovaRada.ispisRedaka(_popisRedakaIspisa, trenutniBrojRedovaIspisa);
                }
            }
            catch (Exception e)
            {
                BrojacGreske.Instance.dodajNovuGesku(e.Message);
            }

        }

        private static string provjeraRezervacije(int value, Brod brod)
        {
            DateTime trenutnoVV = VirtualniSat.Instance.virtualnoVrijeme();
            int danUTjednu = (int)trenutnoVV.DayOfWeek;
            TimeOnly vrijeme = TimeOnly.FromDateTime(trenutnoVV);
            string vrstaVeza = ZajednickeMetode.dohvatiVrstuLuke(brod.vrsta);

            List<Raspored> sviRasporedi = new List<Raspored>(PodaciDatoteka.Instance.getListaRasporeda());
            List<Raspored> popisRasporeda = new List<Raspored>(sviRasporedi.FindAll(x => x.idBrod == value && x.daniUTjednu == danUTjednu && x.vrijemeOd <= vrijeme && x.vrijemeDo >= vrijeme));
            if (popisRasporeda.Count == 0)
                throw new Exception("Nema rasporeda za traženi brod (ID: " + brod.id + ") u trenutnom vremenu: " + trenutnoVV.ToString("dd.MM.yyyy. HH:mm:ss"));
            else
            {
                foreach (Raspored r in popisRasporeda)
                {
                    Vez? vez = PodaciDatoteka.Instance.getListaVeza().Find(x => x.id == r.idVez);
                    if (vez == null)
                        throw new Exception("Ne postoji vez sa traženim ID: " + r.idVez);
                    else
                    {
                        if (vez.vrsta == vrstaVeza)
                        {
                            return "Brod posjeduje ispravni raspored, te se je uspiješno vezao na vez: " + vez.id;
                        }
                    }
                }
            }
            return "";
        }

        private static List<int> _popunjeniKanali = new List<int>();

        private static void spajanjeKanala(string komanda)
        {
            _potvrdaKomande = true;
            _provjeraFrekvencije = false;
            _pomocnaListaRedakaIspisa = new();
            int trenutniBrojRedovaIspisa = _popisRedakaIspisa.Count;

            string pattern = "^(?<idBroda>[0-9]{1,3}) (?<idKanala>[0-9]{1,3})(?<odjavljivanje> [Q])?";
            Match match = new Regex(pattern).Match(komanda);

            int idBroda = int.Parse(match.Groups["idBroda"].Value);
            int frekvencija = int.Parse(match.Groups["idKanala"].Value);

            try
            {
                provjeriBrod(idBroda);
                provjeriPridruziBrodKanalu(idBroda, frekvencija, match.Groups["odjavljivanje"].Success);

                _popisRedakaIspisa.AddRange(_pomocnaListaRedakaIspisa);
                IspisRedovaRada.ispisRedaka(_popisRedakaIspisa, trenutniBrojRedovaIspisa);
            }
            catch (Exception e)
            {
                BrojacGreske.Instance.dodajNovuGesku(e.Message);
            }
        }

        private static void provjeriBrod(int idBroda)
        {
            Brod? podaciBroda = PodaciDatoteka.Instance.getListaBrodova().Find(x => x.id == idBroda);
            if (podaciBroda == null)
            {
                throw new Exception("Ne postoji traženi ID broda: " + idBroda);
            }
        }

        private static void provjeriPridruziBrodKanalu(int idBroda, int frekvencija, bool provjeraKomande)
        {
            Kanal podaciKanala = dohvatiKanal(frekvencija);

            //Konstruktor za observer
            ConcreteSubjectKanala csk = new ConcreteSubjectKanala(podaciKanala.idKanal, 0);
            csk.Attach(new ConcreteObserver(podaciKanala.idKanal));

            List<DnevnikRada> dnevnikRada = PodaciDatoteka.Instance.getListaDnevnikaRada().FindAll(x => x.idKanala == podaciKanala.idKanal && x.odobrenZahtjev && !x.slobodan);

            if (provjeraKomande)
            {
                odjaviBrod(idBroda, podaciKanala.idKanal, dnevnikRada.Count - 1, csk);
            }
            else
            {
                Kanal? kanal = PodaciDatoteka.Instance.getListaKanala().Find(x => x.idKanal == podaciKanala.idKanal);
                //Provjeravamo je li kanal ima mjesta ili ne
                if (dnevnikRada == null || kanal != null && dnevnikRada.Count < kanal.maksimalanBroj)
                {
                    if (provjeraDuplikata(podaciKanala.idKanal, idBroda))
                    {
                        return;
                    }
                    dodajRedakZaPomocnuListu("Prijavljujemo brod sa ID-om: " + idBroda + " na kanal sa ID-om: " + podaciKanala.idKanal);
                    ZajednickeMetode.zapisiDnevnikRada(true, VirtualniSat.Instance.virtualnoVrijeme(), idBroda, podaciKanala.idKanal, false);
                    csk.TrenutniKapacitet = dnevnikRada.Count + 1;
                }
                else
                {
                    ZajednickeMetode.zapisiDnevnikRada(false, VirtualniSat.Instance.virtualnoVrijeme(), idBroda, podaciKanala.idKanal, true);
                    BrojacGreske.Instance.dodajNovuGesku("Kanal sa ID-om: " + podaciKanala.idKanal + " je puni, traži se sljedeći slobodni kanal.");
                    _popunjeniKanali.Add(podaciKanala.idKanal);
                    odaberiSljedećiKanal(idBroda);
                }
            }
        }

        private static Kanal dohvatiKanal(int frekvencija)
        {
            Kanal? podaciKanala = PodaciDatoteka.Instance.getListaKanala().Find(x => x.frekvencija == frekvencija);

            if (podaciKanala == null)
                throw new Exception("Ne postoji niti jedan kanal sa frekvencijom: " + frekvencija);

            return podaciKanala;
        }

        private static void odjaviBrod(int idBroda, int idKanala, int razlika, ConcreteSubjectKanala csk)
        {
            DnevnikRada? dr = PodaciDatoteka.Instance.getListaDnevnikaRada().Find(x => x.idKanala == idKanala && x.idBrod == idBroda && x.odobrenZahtjev && !x.slobodan);

            if (dr != null)
            {
                _popisRedakaIspisa.Add("Odjavljujemo bod sa ID-om: " + idBroda + " od kanala sa ID-om: " + idKanala);
                PodaciDatoteka.Instance.getListaDnevnikaRada().Find(x => x.idBrod == idBroda && x.idKanala == idKanala && x.odobrenZahtjev && !x.slobodan).slobodan = true;
                csk.TrenutniKapacitet = razlika;
            }
            else
                BrojacGreske.Instance.dodajNovuGesku("Ne posotji traženi ID boda: " + idBroda + " u kanali sa ID-om: " + idKanala);
        }

        private static void odaberiSljedećiKanal(int idBroda)
        {
            List<Kanal> popisKanala = new List<Kanal>(PodaciDatoteka.Instance.getListaKanala());
            foreach (int zauzetiIdKanala in _popunjeniKanali)
            {
                popisKanala.RemoveAll(x => x.idKanal == zauzetiIdKanala);
            }

            if (popisKanala.Count == 0)
                throw new Exception("Popunjeni su svi dostupni kanali");

            spajanjeKanala(idBroda + " " + popisKanala[0].frekvencija);
        }

        private static bool provjeraDuplikata(int idKanala, int idBroda)
        {
            DnevnikRada? dr = PodaciDatoteka.Instance.getListaDnevnikaRada().Find(x => x.idKanala == idKanala && x.idBrod == idBroda && x.odobrenZahtjev && !x.slobodan);
            if (dr == null) return false;

            BrojacGreske.Instance.dodajNovuGesku("Navedeni ID broda: " + idBroda + " je već spojeni na traženi ID kanala: " + idKanala);
            return true;
        }

        private static void ukupniBrojZauzetihVezova(string value)
        {
            _potvrdaKomande = true;
            _pomocnaListaRedakaIspisa = new();
            int trenutniBrojRedovaIspisa = _popisRedakaIspisa.Count;

            DateTime vrijeme = DateTime.Parse(value);
            _ukupnoZauzetihVezova = 0;

            _popisRedakaIspisa.AddRange(IspisRedovaRada.ispisZaglavlja());

            ObjectStructure visitor = new ObjectStructure();

            visitor.Attach(new ConcreteElementZauzetost("PU", vrijeme));
            visitor.Attach(new ConcreteElementRedniBroj("PU"));

            visitor.Attach(new ConcreteElementZauzetost("PO", vrijeme));
            visitor.Attach(new ConcreteElementRedniBroj("PO"));

            visitor.Attach(new ConcreteElementZauzetost("OS", vrijeme));
            visitor.Attach(new ConcreteElementRedniBroj("OS"));

            ConcreteVisitor1 v1 = new ConcreteVisitor1();
            visitor.Accept(v1);

            _popisRedakaIspisa.AddRange(_pomocnaListaRedakaIspisa);

            _popisRedakaIspisa.AddRange(IspisRedovaRada.ispisPodnozja(_ukupnoZauzetihVezova));

            IspisRedovaRada.ispisRedaka(_popisRedakaIspisa, trenutniBrojRedovaIspisa);
        }

        private static void uredivanjeTablica(string z, string p, string rb)
        {
            _potvrdaKomande = true;
            int trenutniBrojRedovaIspisa = _popisRedakaIspisa.Count;

            if (z != "")
                Tablice.Instance.Z = true;
            else
                Tablice.Instance.Z = false;
            if (p != "")
                Tablice.Instance.P = true;
            else
                Tablice.Instance.P = false;
            if (rb != "")
                Tablice.Instance.RB = true;
            else
                Tablice.Instance.RB = false;

            _popisRedakaIspisa.Add("Oblikovanje tablica uspiješno postavljeno.");
            _popisRedakaIspisa.Add("Z: " + Tablice.Instance.Z);
            _popisRedakaIspisa.Add("P: " + Tablice.Instance.P);
            _popisRedakaIspisa.Add("RB: " + Tablice.Instance.RB);

            IspisRedovaRada.ispisRedaka(_popisRedakaIspisa, trenutniBrojRedovaIspisa);
        }

        private static void vlastitaFunkcionalnost(string value)
        {
            _potvrdaKomande = true;
            _pomocnaListaRedakaIspisa = new();
            int trenutniBrojRedovaIspisa = _popisRedakaIspisa.Count;

            string pattern = "^(?=.*\\s?(?<prikazRasporeda>[rR]))?(?=.*\\s?(?<prikazDnevnika>[dD]))?(?=.*\\s?(?<prikazBrodova>[bB]))?(?=.*\\s?(?<prikazZahtjevaRezervacija>[zZ]))?";
            Match matcherKomande = new Regex(pattern).Match(value);

            HandlerVlastiteFunkcije prikazRasporeda = new PrikazRasporeda();
            HandlerVlastiteFunkcije prikazDnevnika = new PrikazDnevnika();
            HandlerVlastiteFunkcije prikazBrodova = new PrikazBrodova();
            HandlerVlastiteFunkcije prikazZahtjevaRezervacije = new PrikazZahtjevaRezervacija();
            prikazRasporeda.SetSuccessor(prikazDnevnika);
            prikazDnevnika.SetSuccessor(prikazBrodova);
            prikazBrodova.SetSuccessor(prikazZahtjevaRezervacije);

            prikazRasporeda.ProcesRequest(matcherKomande);

            _popisRedakaIspisa.AddRange(_pomocnaListaRedakaIspisa);
            IspisRedovaRada.ispisRedaka(_popisRedakaIspisa, trenutniBrojRedovaIspisa);
        }

        private static OriginatorStanjaVezova osv = new OriginatorStanjaVezova();
        static private CaretakerStanjaVezova csv = new CaretakerStanjaVezova();
        private static List<StanjeZahtjevaRezervacije> _popisStanja = new();

        private static void spremanjeStanja(string nazivStanja)
        {
            _potvrdaKomande = true;
            int trenutniBrojRedovaIspisa = _popisRedakaIspisa.Count;

            StanjeZahtjevaRezervacije novoStanje = new StanjeZahtjevaRezervacije(nazivStanja);

            osv.stanjeVezova = novoStanje;
            if (csv.dodajNovoStanje(osv.kreirajMementoStanjaVezova()))
            {
                _popisRedakaIspisa.Add("Uspiješno spremanje postojećeg stanja pod nazivom: " + nazivStanja);
            }
            IspisRedovaRada.ispisRedaka(_popisRedakaIspisa, trenutniBrojRedovaIspisa);
        }

        private static void vracanjeStanja(string nazivDatoteke)
        {
            _potvrdaKomande = true;
            int trenutniBrojRedovaIspisa = _popisRedakaIspisa.Count;

            MementoStanjaVezova? trazenoStanje = csv.vratiStanje(nazivDatoteke);

            if (trazenoStanje is not null)
            {
                osv.setajMementoStanjaVezova(trazenoStanje);

                _popisRedakaIspisa.Add("Traženo stanje je uspiješno vraćeno");
                IspisRedovaRada.ispisRedaka(_popisRedakaIspisa, trenutniBrojRedovaIspisa);
            }
            else
                BrojacGreske.Instance.dodajNovuGesku("Greška prilikom vraćanja stanja: Ne postoji traženo stanje: " + nazivDatoteke);
        }

        private static void krajPrograma()
        {
            _potvrdaKomande = true;
            int trenutniBrojRedovaIspisa = _popisRedakaIspisa.Count;
            IspisRedovaRada.ispisRedaka(new List<string> { "Kraj programa, izlazak iz aplikacije!" }, trenutniBrojRedovaIspisa);
            Environment.Exit(0);
        }

        public static void povecajCounterVezova()
        {
            _counterStatusaVeza++;
        }

        public static void povecajUkupniBrojZauzetihVezova(int broj)
        {
            _ukupnoZauzetihVezova = broj;
        }
    }
}
