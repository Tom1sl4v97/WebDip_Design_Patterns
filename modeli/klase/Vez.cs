using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttomiek_zadaca_1.klase
{
    public class Vez
    {
        public static string PATTERN_INFO_RETKA_CSV = "^(?=.*(?<id>id;?))^(?=.*(?<oznaka_veza>oznaka_veza;?))^(?=.*(?<vrsta>vrsta;?))^(?=.*(?<cijena_veza_po_satu>cijena_veza_po_satu;?))^(?=.*(?<maksimalna_duljina>maksimalna_duljina;?))^(?=.*(?<maksimalna_sirina>maksimalna_sirina;?))^(?=.*(?<maksimalna_dubina>maksimalna_dubina;?)).+";
        public int id { get; set; }
        private string _oznakaVeza = "";
        public string oznakaVeza 
        {
            get { return _oznakaVeza; }
            set
            {
                if (value == null || value == "")
                    throw new Exception("Oznaka veza ne smije biti prazna");
                _oznakaVeza = value;
            }
        }
        private string _vrsta = "";
        public string vrsta 
        {
            get { return _vrsta; }
            set
            {
                if (value == null || value == "")
                    throw new Exception("Vrsta veza ne smije biti prazna");
                _vrsta = value;
            }
        }
        public decimal cijenaVezaPoSatu { get; set; }
        public double maksimalnaDuljina { get; set; }
        public double maksimalnaSirina { get; set; }
        public double maksimalnaDubina { get; set; }
    }
}
