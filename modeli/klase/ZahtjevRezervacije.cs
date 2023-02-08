using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttomiek_zadaca_1.klase
{
    public class ZahtjevRezervacije
    {
        public static string PATTERN_INFO_RETKA_CSV = "^(?=.*(?<id_brod>id_brod;?))^(?=.*(?<datum_vrijeme_od>datum_vrijeme_od;?))^(?=.*(?<trajanje_priveza_u_h>trajanje_priveza_u_h;?)).+";
        public int idBrod { get; set; }
        public DateTime datumVrijemeOd { get; set; }
        public double trajanjePrivezaUH { get; set; }
        public int idVeza { get; set; }
    }
}
