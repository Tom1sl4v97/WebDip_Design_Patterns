using System.Text.RegularExpressions;
using ttomiek_zadaca_1;
using ttomiek_zadaca_1.adapter;
using ttomiek_zadaca_1.bridge;
using ttomiek_zadaca_1.ConcreteFM;
using ttomiek_zadaca_1.ConcrreteFM;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.singelton_class;
using static ttomiek_zadaca_1.zajednickeMetode.ZajednickeMetode;

public class Aplikacija
{
    private static bool potvrdaKomande = false;
    public static void Main(string[] args)
    {
        string popisNaredbi = napraviStringNaredbi(args);
        provjeriUcitajUneseniString(popisNaredbi);

        konfigurirajPotrebnePodatke();

        string regexPatternKomandi = "^(?=.*\\s?Q(?<krajPrograma>))?(?=.*\\s?I(?<status>))?(?=.*\\s?VR (?<virtualnoVrijeme>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2}):(\\d{2})))?(?=.*\\s?V (?<zauzetostVeza>(?<vrstaVeza>([A-Z]{2,2})) (?<statusVeza>[Z|S]) (?<datumOd>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2}):(\\d{2})) (?<datumDo>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2}):(\\d{2}))))?(?=.*\\s?UR (?<ucitajDatoteku>[a-zA-Z0-9!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]{3,1000}))?(?=.*\\s?ZP (?<zahtjevPriveza>(?<idBroda>[0-9]{1,3}) (?<trajanje>[0-9]{1,3})))?(?=.*\\s?ZD (?<zatraziDozvolu>[0-9]{1,3}))?";

        while (true)
        {
            Console.WriteLine();
            string? uneseniString = Console.ReadLine();
            Match match = new Regex(regexPatternKomandi).Match(uneseniString);
            potvrdaKomande = false;

            if (match.Groups["status"].Success) pregledStatusa();
            if (match.Groups["virtualnoVrijeme"].Success) postaviVirtualnoVrijeme(match.Groups["virtualnoVrijeme"].Value);
            if (match.Groups["zauzetostVeza"].Success) ispisZauzetostiVeza(match.Groups["zauzetostVeza"].Value);
            if (match.Groups["ucitajDatoteku"].Success) ucitajPodatkeZahtjevaRezervacije(match.Groups["ucitajDatoteku"].Value);
            if (match.Groups["zahtjevPriveza"].Success) zahtjevPriveza(match.Groups["zahtjevPriveza"].Value);
            if (match.Groups["zatraziDozvolu"].Success) zatraziDozvolu(int.Parse(match.Groups["zatraziDozvolu"].Value));
            if (match.Groups["krajPrograma"].Success) krajPrograma();
            if (!potvrdaKomande) BrojacGreske.Instance.IspisGreske("Pogrešna komanda aplikacije, molimo unesite ispravne nazive dokumenata!");
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
        string pattern = @"^(?=.*\s?-l (?<luka>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))(?=.*\s?-v (?<vez>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))(?=.*\s?-b (?<brod>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))(?=.*\s?-r (?<raspored>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))?.+";
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

                    string? putanja = Directory.GetCurrentDirectory();
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
        citanjeBrodova.dohvatiPodatkeDatoteke();
        Console.WriteLine("");
        CitanjeDatotekeInterface citanjeLuka = new CitanjeLukeFactory().CreateCitac();
        citanjeLuka.dohvatiPodatkeDatoteke();
        Console.WriteLine("");
        CitanjeDatotekeInterface citanjeVeza = new CitanjeVezFactory().CreateCitac();
        citanjeVeza.dohvatiPodatkeDatoteke();
        Console.WriteLine("");
        if (NaziviDatoteka.Instance.raspored != "")
        {
            CitanjeDatotekeInterface citanjeRasporeda = new CitanjeRasporedaFactory().CreateCitac();
            citanjeRasporeda.dohvatiPodatkeDatoteke();
        }

        VirtualniSat.Instance.virtualnoVrijeme(PodaciDatoteka.Instance.getLuka().virtualnoVrijeme);
    }

    private static void pregledStatusa()
    {
        potvrdaKomande = true;
        ispisVirtualnogSata();

        Console.WriteLine("\nID\tOV\tVRSTA\tCIJENA\tDULJINA\tSIRINA\tDUBINA\tSTATUS");

        DateTime trenutnoVrijeme = VirtualniSat.Instance.virtualnoVrijeme();
        List<Raspored> sviRasporedi = new List<Raspored>(PodaciDatoteka.Instance.getListaRasporeda());
        List<ZahtjevRezervacije> popisZahtjeva = new List<ZahtjevRezervacije>(PodaciDatoteka.Instance.getListaZahtjevaRezervacije());

        foreach (Vez vez in PodaciDatoteka.Instance.getListaVeza())
        {
            int danUTjednu = (int)trenutnoVrijeme.DayOfWeek;
            TimeOnly vrijeme = TimeOnly.FromDateTime(trenutnoVrijeme);
            List<Raspored> preklapanjeRasporedom = sviRasporedi.FindAll(x => x.idVez == vez.id && x.daniUTjednu == danUTjednu);
            bool provjera = true;

            foreach (Raspored r in preklapanjeRasporedom)
            {
                if (r.vrijemeOd <= vrijeme && r.vrijemeDo >= vrijeme)
                {
                    provjera = false;
                }
            }

            foreach (ZahtjevRezervacije zr in popisZahtjeva)
            {
                DateTime pocetnoVrijeme = zr.datumVrijemeOd;
                DateTime krajnjeVrijeme = zr.datumVrijemeOd.AddHours(zr.trajanjePrivezaUH);
                if (pocetnoVrijeme <= trenutnoVrijeme && krajnjeVrijeme >= trenutnoVrijeme)
                {
                    popisZahtjeva.Remove(zr);
                    provjera = false;
                    break;
                }
            }

            if (!provjera) ispisVeza(vez, "ZAUZETI");
            else ispisVeza(vez, "SLOBODAN");
        }
    }

    private static void postaviVirtualnoVrijeme(string value)
    {
        potvrdaKomande = true;
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

    private static void zatraziDozvolu(int value)
    {
        potvrdaKomande = true;
        ispisVirtualnogSata();

        Brod? brod = PodaciDatoteka.Instance.getListaBrodova().Find(x => x.id == value);
        try
        {
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

    private static void provjeraRezervacije(int value, Brod brod)
    {
        DateTime trenutnoVV = VirtualniSat.Instance.virtualnoVrijeme();
        int danUTjednu = (int)trenutnoVV.DayOfWeek;
        TimeOnly vrijeme = TimeOnly.FromDateTime(trenutnoVV);
        VrstaLuke vrstaLuke = new DohvatiVrstuLuke(brod.vrsta);
        string vrstaVeza = vrstaLuke.dohvatiVrstu();

        List<Raspored> sviRasporedi = PodaciDatoteka.Instance.getListaRasporeda();
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

    private static void zahtjevPriveza(string value)
    {
        potvrdaKomande = true;
        ispisVirtualnogSata();

        string[] uneseniPodaci = value.Split(' ');

        try
        {
            CitanjeZahtjevRezervacije czr = new CitanjeZahtjevRezervacije();
            ZahtjevRezervacije zr = new();
            zr.idBrod = int.Parse(uneseniPodaci[0]);
            zr.datumVrijemeOd = VirtualniSat.Instance.virtualnoVrijeme();
            zr.trajanjePrivezaUH = int.Parse(uneseniPodaci[1]);

            czr.provjeraRasporeda(zr);
            Console.WriteLine(PodaciDatoteka.Instance.getListaZahtjevaRezervacije().Count);
        }
        catch (Exception e)
        {
            BrojacGreske.Instance.IspisGreske(e.Message);
        }
    }

    private static void ucitajPodatkeZahtjevaRezervacije(string nazivDatoteke)
    {
        potvrdaKomande = true;
        ispisVirtualnogSata();

        NaziviDatoteka.Instance.zahtjevRezervacije = nazivDatoteke;
        CitanjeDatotekeInterface citanjeZahtjevaRezervacije = new CitanjeZahtjevaRezervacijeFactory().CreateCitac();
        citanjeZahtjevaRezervacije.dohvatiPodatkeDatoteke();
    }

    private static void ispisVeza(Vez vez, string ostatakPoruke)
    {
        Console.WriteLine(vez.id + "\t" + vez.oznakaVeza + "\t" + vez.vrsta + "\t" + vez.cijenaVezaPoSatu + "\t" + vez.maksimalnaDuljina + "\t" + vez.maksimalnaSirina + "\t" + vez.maksimalnaDubina + "\t" + ostatakPoruke);
    }

    private static Raspored? dohvatiMoguciRapored(ZahtjevRezervacije zahtjev)
    {
        List<Raspored> popisRasporeda = PodaciDatoteka.Instance.getListaRasporeda();
        int danUTjednu = (int)VirtualniSat.Instance.virtualnoVrijeme().DayOfWeek;
        TimeOnly virtualnoVrijeme = TimeOnly.FromDateTime(VirtualniSat.Instance.virtualnoVrijeme());

        //Želim pronaći ako postoji koja rezervacija kako bi dobio ispravni vez i njegov ID
        Raspored? moguciRaspored = popisRasporeda.Find(x =>
        x.idBrod == zahtjev.idBrod &&
        x.daniUTjednu == danUTjednu &&
        x.vrijemeOd <= virtualnoVrijeme &&
        x.vrijemeDo >= virtualnoVrijeme
        );

        return moguciRaspored;
    }

    private static void ispisZauzetostiVeza(string value)
    {
        potvrdaKomande = true;
        ispisVirtualnogSata();

        string regexPattern = "^(?<vrstaVeza>([A-Z]{2,2})) (?<statusVeza>[Z|S]) (?<datumOd>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2}):(\\d{2})) (?<datumDo>(\\d{2}).(\\d{2}).(\\d{4}). (\\d{2}):(\\d{2}):(\\d{2}))";
        Match match = new Regex(regexPattern).Match(value);

        string vrstaVeza = match.Groups["vrstaVeza"].Value;
        string statusVeza = match.Groups["statusVeza"].Value;
        DateTime datumOd = DateTime.Parse(match.Groups["datumOd"].Value);
        DateTime datumDo = DateTime.Parse(match.Groups["datumDo"].Value);

        BridgeDostupnostiVeza bridgeDostupnostiVeza;
        KreirajMetodu kreirajMetodu = new KreirajMetodu();
        kreirajMetodu.datumOd = datumOd;
        kreirajMetodu.datumDo = datumDo;

        List<Vez> popisOdgovarajucihVezova = PodaciDatoteka.Instance.getListaVeza().FindAll(x => x.vrsta == vrstaVeza);
        try
        {
            List<ZahtjevRezervacije> pomocnaListaZahtjeva = filtrirajZahtjeve(vrstaVeza);

            kreirajMetodu.pomocnaListaZahtjeva = filtrirajZahtjeve(vrstaVeza);

            Console.WriteLine("\nID\tOV\tVRSTA\tCIJENA\tDULJINA\tSIRINA\tDUBINA\tSTATUS");

            foreach (Vez vez in popisOdgovarajucihVezova)
            {
                kreirajMetodu.vez = vez;
                if (statusVeza == "Z")
                {
                    bridgeDostupnostiVeza = new BridgeDostupnostiVeza(new ProvjeraZauzetihVezova());
                    kreirajMetodu.pomocnaListaZahtjeva = kreirajMetodu.ispisVezova(bridgeDostupnostiVeza);
                }
                else if (statusVeza == "S")
                {
                    bridgeDostupnostiVeza = new BridgeDostupnostiVeza(new ProvjeraSlobodnihVezova());
                    kreirajMetodu.pomocnaListaZahtjeva = kreirajMetodu.ispisVezova(bridgeDostupnostiVeza);
                }
                else
                {
                    throw new Exception("Uneseni status veza je pogrešan: " + statusVeza);
                }
            }
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
            VrstaLuke vrstaLuke = new DohvatiVrstuLuke(podaciBroda.vrsta);
            if (podaciBroda != null && vrstaLuke.dohvatiVrstu() == vrstaVeza)
            {
                pomocnaListaZahtjeva.Add(zahtjev);
            }
        }
        return pomocnaListaZahtjeva;
    }

    private static void krajPrograma()
    {
        potvrdaKomande = true;
        ispisVirtualnogSata();
        Console.WriteLine("Kraj programa, izlazak iz aplikacije!");
        Environment.Exit(0);
    }

    private static void ispisVirtualnogSata()
    {
        Console.WriteLine("Trenutno virtualno vrijeme: " + VirtualniSat.Instance.virtualnoVrijeme().ToString("dd.MM.yyyy. HH:mm:ss "));
    }
}