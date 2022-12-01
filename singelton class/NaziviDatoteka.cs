using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttomiek_zadaca_1
{
    public sealed class NaziviDatoteka
    {
        private NaziviDatoteka() { }

        private static NaziviDatoteka? instance = null;
        public static NaziviDatoteka Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NaziviDatoteka();
                }
                return instance;
            }
        }
    
        public string? brod { get; set; }
        public string? luka { get; set; }
        public string? raspored { get; set; }
        public string? vez { get; set; }
        public string? zahtjevRezervacije { get; set; }
        public string? kanal { get; set; }
        public string? mol { get; set; }
        public string? molVezovi { get; set; }
        public string? putanjaPrograma { get; set; }
    }
}
