﻿using System.Text.RegularExpressions;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public class CitanjeBrod : OsnovneMetode
    {
        public override void provjeriDohvacenePodatke(string[] lines)
        {
            List<Brod> popisBrodova = PodaciDatoteka.Instance.getListaBrodova();
            foreach (string line in lines)
            {
                if (line == lines.First()) continue;

                string[] podaciRetka = line.Split(';');
                try
                {
                    int trenutniId = int.Parse(podaciRetka[0]);
                    Brod? brod = popisBrodova.Find(x => x.id == trenutniId);
                    if (brod == null)
                    {
                        Brod noviBrod = kreirajObjekt(podaciRetka);

                        if (noviBrod.kapacitetPutnika == 0 && noviBrod.kapacitetTereta == 0 && noviBrod.kapacitetOsobnihVozila == 0)
                        {
                            BrojacGreske.Instance.dodajGreskeUcitavanjaCSVDatoteki("Navedeni ID broda: " + noviBrod.id + " nema postavljene kapacitete!");
                            continue;
                        }

                        PodaciDatoteka.Instance.addNoviBrod(noviBrod);
                    }
                    else
                    {
                        BrojacGreske.Instance.dodajGreskeUcitavanjaCSVDatoteki("Navedeni ID broda: " + trenutniId + " već postoji u popisu brodova.");
                    }
                }
                catch (Exception e)
                {
                    BrojacGreske.Instance.dodajGreskeUcitavanjaCSVDatoteki("Neispravni redak: " + line + " u datoteci: " + NaziviDatoteka.Instance.brod + " GRESKA: " + e.Message);
                }
            }
        }

        private Brod kreirajObjekt(string[] podaciRetka)
        {
            Brod noviBrod = new();

            noviBrod.id = int.Parse(podaciRetka[0]);
            noviBrod.oznakaBroda = podaciRetka[1];
            noviBrod.naziv = podaciRetka[2];
            noviBrod.vrsta = podaciRetka[3];
            noviBrod.duljina = double.Parse(podaciRetka[4].Replace(',', '.'));
            noviBrod.sirina = double.Parse(podaciRetka[5].Replace(',', '.'));
            noviBrod.gaz = double.Parse(podaciRetka[6].Replace(',', '.'));
            noviBrod.maksimalnaBrzina = double.Parse(podaciRetka[7].Replace(',', '.'));
            noviBrod.kapacitetPutnika = int.Parse(podaciRetka[8]);
            noviBrod.kapacitetOsobnihVozila = int.Parse(podaciRetka[9]);
            noviBrod.kapacitetTereta = double.Parse(podaciRetka[10].Replace(',', '.'));

            ZajednickeMetode.dohvatiVrstuLuke(noviBrod.vrsta);

            return noviBrod;
        }

    }
}
