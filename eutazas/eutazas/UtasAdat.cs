using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eutazas
{
    internal class UtasAdat
    {
        public UtasAdat(int megallo, string datum, string azonosito, string irat_tipus, int irat_ervenyesseg)
        {
            this.megallo = megallo;
            this.datum = datum;
            this.azonosito = azonosito;
            this.irat_tipus = irat_tipus;
            this.irat_ervenyesseg = irat_ervenyesseg;
        }

        public int megallo { get; set; }
        public string datum { get; set; }
        public string azonosito { get; set; }
        public string irat_tipus { get; set; }
        public int irat_ervenyesseg { get; set; }

        public bool ervenyes()
        {
            if ((irat_tipus == "JGY" && irat_ervenyesseg == 0) || (irat_tipus != "JGY" && irat_ervenyesseg < Convert.ToInt32(datum.Split('-')[0]))) { return false; }
            return true;
        }
        public int datum_ev() { return Convert.ToInt32(datum.Split('-')[0].Substring(0, 4)); }
        public int datum_ho() { return Convert.ToInt32(datum.Split('-')[0].Substring(4, 2)); }
        public int datum_nap() { return Convert.ToInt32(datum.Split('-')[0].Substring(6, 2)); }
        public int ervenyesseg_ev() { return irat_ervenyesseg / 10000; }
        public int ervenyesseg_ho() { return (irat_ervenyesseg % 10000) / 100; }
        public int ervenyesseg_nap() { return irat_ervenyesseg % 100; }
    }
}
