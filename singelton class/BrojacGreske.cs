using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttomiek_zadaca_1
{
    public sealed class BrojacGreske
    {
        private BrojacGreske() { }
        private static BrojacGreske? instance = null;
        private static int brojGresaka = 0;
        public static BrojacGreske Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BrojacGreske();
                }
                return instance;
            }
        }

        public void IspisGreske(string opis)
        {
            brojGresaka++;
            Console.WriteLine("Broj gresaka: " + brojGresaka + " " + opis);
        }
    }
}
