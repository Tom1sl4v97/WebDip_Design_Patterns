using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttomiek_zadaca_1.klase
{
    internal class Raspored
    {
        public static string PATTERN_INFO_RETKA_CSV = "^(?=.*(?<id_vez>id_vez;?))^(?=.*(?<id_brod>id_brod;?))^(?=.*(?<dani_u_tjednu>dani_u_tjednu;?))^(?=.*(?<vrijeme_od>vrijeme_od;?))^(?=.*(?<vrijeme_do>vrijeme_do;?)).+";
        public int idVez { get; set; }
        public int idBrod { get; set; }
        public int daniUTjednu { get; set; }
        public TimeOnly vrijemeOd { get; set; }
        public TimeOnly vrijemeDo { get; set; }
    }
}
