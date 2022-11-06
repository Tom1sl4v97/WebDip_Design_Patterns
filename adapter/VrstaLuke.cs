using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttomiek_zadaca_1.adapter
{
    public class VrstaLuke
    {
        protected string vrsta;

        public virtual string dohvatiVrstu(){
            throw new Exception("Nije uneseni string vrste broda!");
        }
    }

    public class DohvatiVrstuLuke : VrstaLuke
    {
        private string _vrstaBroda;
        private PodaciVrsteLuke _podaci;

        public DohvatiVrstuLuke(string vrstaBroda)
        {
            this._vrstaBroda = vrstaBroda;
        }

        public override string dohvatiVrstu()
        {
            _podaci = new PodaciVrsteLuke();
            return _podaci.dohvatiVrstuLuke(_vrstaBroda);
        }
    }

    public class PodaciVrsteLuke
    {
        public string dohvatiVrstuLuke(string vrstaBroda)
        {
            switch (vrstaBroda)
            {
                case "TR" or "KA" or "KL" or "KR":
                    return "PU";
                case "RI" or "TE":
                    return "PO";
                case "JA" or "BR" or "RO":
                    return "OS";
                default:
                    throw new Exception("Neispravna oznaka vrste broda: " + vrstaBroda);
            }
        }
    }
}
