using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttomiek_zadaca_1.klase
{
    public class Luka
    {
        public static string PATTERN_INFO_RETKA_CSV = "^(?=.*(?<naziv>naziv;?))(?=.*(?<gps_sirina>(GPS|gps)_sirina;?))(?=.*(?<gps_visina>(GPS|gps)_visina;?))(?=.*(?<dubina_luke>dubina_luke;?))(?=.*(?<ubpuv>ukupni_broj_putnickih_vezova;?))(?=.*(?<ubpov>ukupni_broj_poslovnih_vezova;?))(?=.*(?<ubosv>ukupni_broj_ostalih_vezova;?))(?=.*(?<virtualno_vrijeme>virtualno_vrijeme;?)).+";
        private string _naziv = "";
        public string naziv
        {
            get
            {
                return _naziv;
            }
            set
            {
                if (value == null || value == "")
                    throw new Exception("Naziv ne smije biti prazan!");
                _naziv = value;
            }
        }
        public double gpsSirina { get; set; }
        public double gpsVisina { get; set; }
        public double dubinaLuke { get; set; }
        public int ukupniBrojPutnickihVezova { get; set; }
        public int ukupniBrojPoslovnihVezova { get; set; }
        public int ukupniBrojOstalihVezova { get; set; }
        public DateTime virtualnoVrijeme { get; set; }
    }
}
