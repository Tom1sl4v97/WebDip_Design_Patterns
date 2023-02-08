using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ttomiek_zadaca_1.ConcrreteFM;
using ttomiek_zadaca_1.@interface;

namespace ttomiek_zadaca_1.ConcreteFM
{
    public class CitanjeBrodFactory : CitacDatotekeFactory
    {
        protected override CitanjeDatotekeInterface MakeCitac()
        {
            CitanjeDatotekeInterface citac = new CitanjeBrod();
            return citac;
        }
    }

    public class CitanjeLukeFactory : CitacDatotekeFactory
    {
        protected override CitanjeDatotekeInterface MakeCitac()
        {
            CitanjeDatotekeInterface citac = new CitanjeLuka();
            return citac;
        }
    }

    public class CitanjeRasporedaFactory : CitacDatotekeFactory
    {
        protected override CitanjeDatotekeInterface MakeCitac()
        {
            CitanjeDatotekeInterface citac = new CitanjeRaspored();
            return citac;
        }
    }

    public class CitanjeVezFactory : CitacDatotekeFactory
    {
        protected override CitanjeDatotekeInterface MakeCitac()
        {
            CitanjeDatotekeInterface citac = new CitanjeVez();
            return citac;
        }
    }

    public class CitanjeZahtjevaRezervacijeFactory : CitacDatotekeFactory
    {
        protected override CitanjeDatotekeInterface MakeCitac()
        {
            CitanjeDatotekeInterface citac = new CitanjeZahtjevRezervacije();
            return citac;
        }
    }

    public class CitanjeKanalFactory : CitacDatotekeFactory
    {
        protected override CitanjeDatotekeInterface MakeCitac()
        {
            CitanjeDatotekeInterface citac = new CitanjeKanal();
            return citac;
        }
    }

    public class CitanjeMolFactory : CitacDatotekeFactory
    {
        protected override CitanjeDatotekeInterface MakeCitac()
        {
            CitanjeDatotekeInterface citac = new CitanjeMola();
            return citac;
        }
    }

    public class CitanjeMolVezFactory : CitacDatotekeFactory
    {
        protected override CitanjeDatotekeInterface MakeCitac()
        {
            CitanjeDatotekeInterface citac = new CitanjeMolVez();
            return citac;
        }
    }
}
