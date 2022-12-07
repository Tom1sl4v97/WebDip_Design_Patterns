using System.Text.RegularExpressions;
using ttomiek_zadaca_1;
using ttomiek_zadaca_1.chainOfResponsbility;
using ttomiek_zadaca_1.ConcreteFM;
using ttomiek_zadaca_1.ConcrreteFM;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.komandaV;
using ttomiek_zadaca_1.observer;
using ttomiek_zadaca_1.singelton_class;
using ttomiek_zadaca_1.visitor;
using ttomiek_zadaca_1.zajednickeMetode;

public class Aplikacija
{
    private static bool _potvrdaKomande = false;
    private static bool _provjeraFrekvencije = true;
    private static int _counterStatusaVeza = 0;
    private static int _ukupnoZauzetihVezova = 0;
    public static void Main(string[] args)
    {
        string popisNaredbi = napraviStringNaredbi(args);
        provjeriUcitajUneseniString(popisNaredbi);

        konfigurirajPotrebnePodatke();

        string regexPatternKomandi = "^(?=.*\\s?Q(?<krajPrograma>))?(?=.*\\s?I(?<status>))?(?=.*\\s?VR (?<virtualnoVrijeme>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2}):(\\d{2})))?(?=.*\\s?V (?<zauzetostVeza>(?<vrstaVeza>([A-Z]{2,2})) (?<statusVeza>[Z|S]) (?<datumOd>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2}):(\\d{2})) (?<datumDo>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2}):(\\d{2}))))?(?=.*\\s?UR (?<ucitajDatoteku>[a-zA-Z0-9!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]{3,1000}))?(?=.*\\s?ZP (?<zahtjevPriveza>(?<idBrodaSpajanja>[0-9]{1,3}) (?<trajanje>[0-9]{1,3})))?(?=.*\\s?ZD (?<zatraziDozvolu>[0-9]{1,3}))?(?=.*\\s?F (?<spajanjeKanalu>(?<idBroda>[0-9]{1,3}) (?<idKanala>[0-9]{1,3})(?<odjavljivanje> [Q])?))?(?=.*\\s?ZA (?<vrijeme>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2})))?(?=.*\\s?T(?<tablice>(?=.* (?<Z>Z))?(?=.* (?<P>P))?(?=.* (?<RB>RB))?))?(?=.*\\s?VF (?<vlastitaFunkcionalnost>[a-zA-Z ]{0,100}))?";

        while (true)
        {
            Console.WriteLine();
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
            if (match.Groups["krajPrograma"].Success && _provjeraFrekvencije) krajPrograma();
            if (!_potvrdaKomande) BrojacGreske.Instance.IspisGreske("Pogrešna komanda, molimo unesite ispravne komande!");
        }
    }

    private static string napraviStringNaredbi(string[] args)
    {
        string stringNaredbi = "";
        foreach (string arg in args)
        {
            stringNaredbi += arg + " ";
        }

        return stringNaredbi;
    }

    private static void provjeriUcitajUneseniString(string uneseniPodaci)
    {
        string pattern = @"^(?=.*\s?-l (?<luka>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))(?=.*\s?-v (?<vez>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))(?=.*\s?-b (?<brod>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))(?=.*\s?-r (?<raspored>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))?(?=.*\s?-m (?<mol>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))(?=.*\s?-k (?<kanal>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))(?=.*\s?-mv (?<molVezovi>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000})).+";
        var regex = new Regex(pattern);

        while (true)
        {
            if (uneseniPodaci != null && uneseniPodaci != "")
            {
                Match match = regex.Match(uneseniPodaci);

                if (match.Success)
                {
                    NaziviDatoteka.Instance.brod = match.Groups["brod"].Value;
                    NaziviDatoteka.Instance.luka = match.Groups["luka"].Value;
                    NaziviDatoteka.Instance.raspored = match.Groups["raspored"].Value;
                    NaziviDatoteka.Instance.vez = match.Groups["vez"].Value;
                    NaziviDatoteka.Instance.kanal = match.Groups["kanal"].Value;
                    NaziviDatoteka.Instance.mol = match.Groups["mol"].Value;
                    NaziviDatoteka.Instance.molVezovi = match.Groups["molVezovi"].Value;

                    string? putanja = Directory.GetCurrentDirectory();
                    //string? putanja = "C:\\Users\\franj\\OneDrive\\Desktop\\podaci";

                    NaziviDatoteka.Instance.putanjaPrograma = putanja;

                    break;
                }
                else
                {
                    BrojacGreske.Instance.IspisGreske("Uneseni podaci nisu ispravni, molimo unesite ponovno!");
                }
            }
            else
            {
                BrojacGreske.Instance.IspisGreske("Prazan unos, molimo unesite potrebne podatke.");
            }

            uneseniPodaci = Console.ReadLine();
        }
    }

    private static void konfigurirajPotrebnePodatke()
    {
        CitanjeDatotekeInterface citanjeBrodova = new CitanjeBrodFactory().CreateCitac();
        citanjeBrodova.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.brod, Brod.PATTERN_INFO_RETKA_CSV);
        Console.WriteLine("Učitani podaci za BRODOVE: " + PodaciDatoteka.Instance.getListaBrodova().Count + "\n");

        CitanjeDatotekeInterface citanjeLuka = new CitanjeLukeFactory().CreateCitac();
        citanjeLuka.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.luka, Luka.PATTERN_INFO_RETKA_CSV);
        Console.WriteLine("Učitani podaci za LUKE: " + PodaciDatoteka.Instance.getLuka().naziv + "\n");

        CitanjeDatotekeInterface citanjeVeza = new CitanjeVezFactory().CreateCitac();
        citanjeVeza.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.vez, Vez.PATTERN_INFO_RETKA_CSV);
        Console.WriteLine("Učitani podaci za VEZOVE: " + PodaciDatoteka.Instance.getListaVeza().Count + "\n");

        string? nazivRasporeda = NaziviDatoteka.Instance.raspored;
        if (nazivRasporeda != "" && nazivRasporeda != null)
        {
            CitanjeDatotekeInterface citanjeRasporeda = new CitanjeRasporedaFactory().CreateCitac();
            citanjeRasporeda.dohvatiPodatkeDatoteke(nazivRasporeda, Raspored.PATTERN_INFO_RETKA_CSV);
            Console.WriteLine("Učitani podaci za RASPORED: " + PodaciDatoteka.Instance.getListaRasporeda().Count + "\n");
        }

        CitanjeDatotekeInterface citanjeKanala = new CitanjeKanalFactory().CreateCitac();
        citanjeKanala.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.kanal, Kanal.PATTERN_INFO_RETKA_CSV);
        Console.WriteLine("Učitani podaci za KANAL: " + PodaciDatoteka.Instance.getListaKanala().Count + "\n");

        CitanjeDatotekeInterface citanjeMolova = new CitanjeMolFactory().CreateCitac();
        citanjeMolova.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.mol, Mol.PATTERN_INFO_RETKA_CSV);
        Console.WriteLine("Učitani podaci za MOL: " + PodaciDatoteka.Instance.getListaMolova().Count + "\n");

        CitanjeDatotekeInterface citanjeMolVezovi = new CitanjeMolVezFactory().CreateCitac();
        citanjeMolVezovi.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.molVezovi, MolVez.PATTERN_INFO_RETKA_CSV);
        Console.WriteLine("Učitani podaci za MOL VEZOVI: " + PodaciDatoteka.Instance.getListaMolVezova().Count + "\n");

        VirtualniSat.Instance.virtualnoVrijeme(PodaciDatoteka.Instance.getLuka().virtualnoVrijeme);
    }

    private static void pregledStatusa()
    {
        _potvrdaKomande = true;
        ispisVirtualnogSata();

        ZajednickeMetode.ispisZaglavljaStatusa();
        
        List<Vez> popisSvihVezova = new List<Vez>(PodaciDatoteka.Instance.getListaVeza());
        DateTime trenutnoVrijeme = VirtualniSat.Instance.virtualnoVrijeme();
        bool sviVezovi = true;

        ZajednickeMetode.ispisTrenutnogStatusa(popisSvihVezova, trenutnoVrijeme, sviVezovi);

        ZajednickeMetode.ispisPodnozjaStatusa(popisSvihVezova.Count);
    }

    private static void postaviVirtualnoVrijeme(string value)
    {
        _potvrdaKomande = true;
        ispisVirtualnogSata();

        try
        {
            VirtualniSat.Instance.virtualnoVrijeme(DateTime.Parse(value));
            Console.WriteLine("Virtualno vrijeme postavljeno na: " + value);
        }
        catch (Exception e)
        {
            BrojacGreske.Instance.IspisGreske(e.Message);
        }
    }

    private static void ispisZauzetostiVeza(string value)
    {
        _potvrdaKomande = true;
        ispisVirtualnogSata();

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
            List<ZahtjevRezervacije> pomocnaListaZahtjeva = filtrirajZahtjeve(vrstaVeza);
            popisListeZahtjeva = filtrirajZahtjeve(vrstaVeza);

            ZajednickeMetode.ispisZaglavljaStatusa();

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

            ZajednickeMetode.ispisPodnozjaStatusa(_counterStatusaVeza - 1);
        }
        catch (Exception e)
        {
            BrojacGreske.Instance.IspisGreske(e.Message);
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
        ispisVirtualnogSata();

        NaziviDatoteka.Instance.zahtjevRezervacije = nazivDatoteke;
        CitanjeDatotekeInterface citanjeZahtjevaRezervacije = new CitanjeZahtjevaRezervacijeFactory().CreateCitac();
        citanjeZahtjevaRezervacije.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.zahtjevRezervacije, ZahtjevRezervacije.PATTERN_INFO_RETKA_CSV);
        Console.WriteLine("Učitani podaci za ZAHTJEVE REZERVACIJE: " + PodaciDatoteka.Instance.getListaZahtjevaRezervacije().Count);
    }

    private static void zahtjevPriveza(string value)
    {
        _potvrdaKomande = true;
        ispisVirtualnogSata();

        string[] uneseniPodaci = value.Split(' ');
        int idBroda = int.Parse(uneseniPodaci[0]);

        DnevnikRada dr = PodaciDatoteka.Instance.getListaDnevnikaRada().Find(x => x.idBrod == idBroda && x.odobrenZahtjev && !x.slobodan);

        if (dr == null)
            BrojacGreske.Instance.IspisGreske("Traženi brod nije u niti jednom kanalu.");
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
                Console.WriteLine("Zahtjev za privezivanje broda sa ID-om: " + zr.idBrod + " je uspješno dodan u listu zahtjeva.");
            }
            catch (Exception e)
            {
                BrojacGreske.Instance.IspisGreske(e.Message);
            }
        }
    }

    private static void zatraziDozvolu(int value)
    {
        _potvrdaKomande = true;
        ispisVirtualnogSata();

        DnevnikRada dr = PodaciDatoteka.Instance.getListaDnevnikaRada().Find(x => x.idBrod == value && x.odobrenZahtjev && !x.slobodan);

        if (dr == null)
            BrojacGreske.Instance.IspisGreske("Traženi brod nije u niti jednom kanalu.");
        else
        {
            try
            {
                Brod? brod = PodaciDatoteka.Instance.getListaBrodova().Find(x => x.id == value);
                if (brod == null)
                    throw new Exception("Ne postoji brod sa traženim ID: " + value);
                else
                {
                    provjeraRezervacije(value, brod);
                }
            }
            catch (Exception e)
            {
                BrojacGreske.Instance.IspisGreske(e.Message);
            }
        }
    }

    private static void provjeraRezervacije(int value, Brod brod)
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
                        Console.WriteLine("Brod posjeduje ispravni raspored, te se je uspiješno vezao na vez: " + vez.id);
                    }
                }
            }
        }
    }

    private static List<int> _popunjeniKanali = new List<int>();

    private static void spajanjeKanala(string komanda)
    {
        _potvrdaKomande = true;
        _provjeraFrekvencije = false;
        ispisVirtualnogSata();

        string pattern = "^(?<idBroda>[0-9]{1,3}) (?<idKanala>[0-9]{1,3})(?<odjavljivanje> [Q])?";
        Match match = new Regex(pattern).Match(komanda);

        int idBroda = int.Parse(match.Groups["idBroda"].Value);
        int frekvencija = int.Parse(match.Groups["idKanala"].Value);

        try
        {
            provjeriBrod(idBroda);
            provjeriPridruziBrodKanalu(idBroda, frekvencija, match.Groups["odjavljivanje"].Success);
        }catch(Exception e)
        {
            BrojacGreske.Instance.IspisGreske(e.Message);
        }
    }

    private static void provjeriBrod(int idBroda)
    {
        Brod podaciBroda = PodaciDatoteka.Instance.getListaBrodova().Find(x => x.id == idBroda);
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
            if (dnevnikRada == null || (kanal != null && dnevnikRada.Count < kanal.maksimalanBroj))
            {
                if (provjeraDuplikata(podaciKanala.idKanal, idBroda))
                {
                    return;
                }
                Console.WriteLine("Prijavljujemo brod sa ID-om: " + idBroda + " na kanal sa ID-om: " + podaciKanala.idKanal);
                ZajednickeMetode.zapisiDnevnikRada(true, VirtualniSat.Instance.virtualnoVrijeme(), idBroda, podaciKanala.idKanal, false);
                csk.TrenutniKapacitet = dnevnikRada.Count + 1;
            }
            else
            {
                ZajednickeMetode.zapisiDnevnikRada(false, VirtualniSat.Instance.virtualnoVrijeme(), idBroda, podaciKanala.idKanal, true);
                BrojacGreske.Instance.IspisGreske("Kanal sa ID-om: " + podaciKanala.idKanal + " je puni, traži se sljedeći slobodni kanal.");
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
        DnevnikRada dr = PodaciDatoteka.Instance.getListaDnevnikaRada().Find(x => x.idKanala == idKanala && x.idBrod == idBroda && x.odobrenZahtjev && !x.slobodan);

        if (dr != null)
        {
            Console.WriteLine("Odjavljujemo bod sa ID-om: " + idBroda + " od kanala sa ID-om: " + idKanala);
            PodaciDatoteka.Instance.getListaDnevnikaRada().Find(x => x.idBrod == idBroda && x.idKanala == idKanala && x.odobrenZahtjev && !x.slobodan).slobodan = true;
            csk.TrenutniKapacitet = razlika;
        }
        else
            BrojacGreske.Instance.IspisGreske("Ne posotji traženi ID boda: " + idBroda + " u kanali sa ID-om: " + idKanala);
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
        DnevnikRada dr = PodaciDatoteka.Instance.getListaDnevnikaRada().Find(x => x.idKanala == idKanala && x.idBrod == idBroda && x.odobrenZahtjev && !x.slobodan);
        if (dr == null) return false;

        BrojacGreske.Instance.IspisGreske("Navedeni ID broda: " + idBroda + " je već spojeni na traženi ID kanala: " + idKanala);
        return true;
    }

    private static void ukupniBrojZauzetihVezova(string value)
    {
        _potvrdaKomande = true;
        ispisVirtualnogSata();

        DateTime vrijeme = DateTime.Parse(value);
        _ukupnoZauzetihVezova = 0;

        ZajednickeMetode.ispisZaglavljaStatusa();

        ObjectStructure visitor = new ObjectStructure();

        visitor.Attach(new ConcreteElementZauzetost("PU", vrijeme));
        visitor.Attach(new ConcreteElementRedniBroj("PU"));

        visitor.Attach(new ConcreteElementZauzetost("PO", vrijeme));
        visitor.Attach(new ConcreteElementRedniBroj("PO"));

        visitor.Attach(new ConcreteElementZauzetost("OS", vrijeme));
        visitor.Attach(new ConcreteElementRedniBroj("OS"));

        ConcreteVisitor1 v1 = new ConcreteVisitor1();
        visitor.Accept(v1);

        ZajednickeMetode.ispisPodnozjaStatusa(_ukupnoZauzetihVezova);
    }

    private static void uredivanjeTablica(string z, string p, string rb)
    {
        _potvrdaKomande = true;
        ispisVirtualnogSata();

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
        Console.WriteLine("Oblikovanje tablica uspiješno postavljeno.");
    }
    
    private static void vlastitaFunkcionalnost(string value)
    {
        _potvrdaKomande = true;
        ispisVirtualnogSata();

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
    }

    private static void krajPrograma()
    {
        _potvrdaKomande = true;
        ispisVirtualnogSata();
        Console.WriteLine("Kraj programa, izlazak iz aplikacije!");
        Environment.Exit(0);
    }

    private static void ispisVirtualnogSata()
    {
        Console.WriteLine("Trenutno virtualno vrijeme: " + VirtualniSat.Instance.virtualnoVrijeme().ToString("dd.MM.yyyy. HH:mm:ss "));
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