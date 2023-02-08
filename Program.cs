using System.Text.RegularExpressions;
using ttomiek_zadaca_1;
using ttomiek_zadaca_1.ConcreteFM;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.singelton_class;
using ttomiek_zadaca_2.composite;
using ttomiek_zadaca_3.modeli;

public class Aplikacija
{
    public static void Main(string[] args)
    {
        string popisNaredbi = napraviStringNaredbi(args);
        provjeriUcitajUneseniString(popisNaredbi);

        konfigurirajPotrebnePodatke();

        KomandeKorisnika.izvrsiKomdanuKorisnika();
    }

    private static void test()
    {
        Composite root = BrodskaLuka.Instance.dohvatiKorijen();

        List<Mol> popisMolova2 = root.dohvatiPopisMolova();
        List<Vez> popisVezova2 = root.dohvatiPopisVezova();
        List<Vez> popisVezova3 = root.dohvatiPopisVezova();
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
        string pattern = @"^(?=.*\s?-l (?<luka>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))" +
            @"(?=.*\s?-v (?<vez>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))" +
            @"(?=.*\s?-b (?<brod>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))" +
            @"(?=.*\s?-r (?<raspored>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))?" +
            @"(?=.*\s?-m (?<mol>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))" +
            @"(?=.*\s?-k (?<kanal>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))" +
            @"(?=.*\s?-mv (?<molVezovi>[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{3,1000}))" +
            @"(?=.*\s?-br (?<brojRedaka>[2-8][0-9]))" +
            @"(?=.*\s?-vt (?<omjerEkrana>(50:50)|(25:75)|(75:25)))" +
            @"(?=.*\s?-pd (?<podjelaEkrana>(R:P)|(P:R))).+";
        var regex = new Regex(pattern);

        while (true)
        {
            if (uneseniPodaci != null && uneseniPodaci != "")
            {
                Match match = regex.Match(uneseniPodaci);

                if (match.Success)
                {
                    provjeriKonfiguracijuEkrana(int.Parse(match.Groups["brojRedaka"].Value), match.Groups["omjerEkrana"].Value, match.Groups["podjelaEkrana"].Value);

                    NaziviDatoteka.Instance.brod = match.Groups["brod"].Value;
                    NaziviDatoteka.Instance.luka = match.Groups["luka"].Value;
                    NaziviDatoteka.Instance.raspored = match.Groups["raspored"].Value;
                    NaziviDatoteka.Instance.vez = match.Groups["vez"].Value;
                    NaziviDatoteka.Instance.kanal = match.Groups["kanal"].Value;
                    NaziviDatoteka.Instance.mol = match.Groups["mol"].Value;
                    NaziviDatoteka.Instance.molVezovi = match.Groups["molVezovi"].Value;

                    //string? putanja = Directory.GetCurrentDirectory();
                    string? putanja = "C:\\Users\\franj\\OneDrive\\Desktop\\podaci";

                    NaziviDatoteka.Instance.putanjaPrograma = putanja;

                    break;
                }
                else
                {
                    BrojacGreske.Instance.dodajGreskeUcitavanjaCSVDatoteki("Uneseni podaci nisu ispravni, molimo unesite ponovno!");
                }
            }
            else
            {
                BrojacGreske.Instance.dodajGreskeUcitavanjaCSVDatoteki("Prazan unos, molimo unesite potrebne podatke.");
            }

            uneseniPodaci = Console.ReadLine();
        }
    }

    private static void provjeriKonfiguracijuEkrana(int brojRedaka, string omjerEkrana, string podjelaEkrana)
    {
        if (brojRedaka < 24 || brojRedaka > 80)
        {
            Console.WriteLine("Broj redaka mora biti u rasponu od 24 do 80!");
            Environment.Exit(0);
        }

        string[] omjeri = omjerEkrana.Split(':');

        KonfiguracijskiPodaci.Instance.brojRedaka = brojRedaka - 1;
        KonfiguracijskiPodaci.Instance.omjerEkrana = int.Parse(omjeri[0]);

        if (podjelaEkrana == "R:P")
            KonfiguracijskiPodaci.Instance.podjelaEkrana = true;
        else
            KonfiguracijskiPodaci.Instance.podjelaEkrana = false;
    }

    private static void konfigurirajPotrebnePodatke()
    {
        KonfiguracijskiPodaci.Instance.PostaviPocetkeReda();

        CitanjeDatotekeInterface citanjeLuka = new CitanjeLukeFactory().CreateCitac();
        citanjeLuka.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.luka, Luka.PATTERN_INFO_RETKA_CSV);
        VirtualniSat.Instance.virtualnoVrijeme(PodaciDatoteka.Instance.getLuka().virtualnoVrijeme);

        CitanjeDatotekeInterface citanjeBrodova = new CitanjeBrodFactory().CreateCitac();
        citanjeBrodova.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.brod, Brod.PATTERN_INFO_RETKA_CSV);

        CitanjeDatotekeInterface citanjeVeza = new CitanjeVezFactory().CreateCitac();
        citanjeVeza.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.vez, Vez.PATTERN_INFO_RETKA_CSV);

        CitanjeDatotekeInterface citanjeMolova = new CitanjeMolFactory().CreateCitac();
        citanjeMolova.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.mol, Mol.PATTERN_INFO_RETKA_CSV);
        
        CitanjeDatotekeInterface citanjeKanala = new CitanjeKanalFactory().CreateCitac();
        citanjeKanala.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.kanal, Kanal.PATTERN_INFO_RETKA_CSV);
        
        CitanjeDatotekeInterface citanjeMolVezovi = new CitanjeMolVezFactory().CreateCitac();
        citanjeMolVezovi.dohvatiPodatkeDatoteke(NaziviDatoteka.Instance.molVezovi, MolVez.PATTERN_INFO_RETKA_CSV);
        
        BrodskaLuka.Instance.postaviKorijen();
        
        string? nazivRasporeda = NaziviDatoteka.Instance.raspored;
        if (nazivRasporeda != "" && nazivRasporeda != null)
        {
            CitanjeDatotekeInterface citanjeRasporeda = new CitanjeRasporedaFactory().CreateCitac();
            citanjeRasporeda.dohvatiPodatkeDatoteke(nazivRasporeda, Raspored.PATTERN_INFO_RETKA_CSV);
        }

    }

}