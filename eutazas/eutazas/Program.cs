using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eutazas
{
    internal class Program
    {
        static List<UtasAdat> adatok = new List<UtasAdat>();
        static void Main(string[] args)
        {
            feladat_1();
            feladat_2();
            feladat_3();
            feladat_4();
            feladat_5();
            feladat_7();

            Console.ReadKey();
        }

        private static void feladat_1()
        {
            string[] f = File.ReadAllLines("utasadat.txt", Encoding.UTF8);
            foreach (string sor in f) {
                string[] s = sor.Split(' ');
                adatok.Add(new UtasAdat(Convert.ToInt32(s[0]), s[1], s[2], s[3], Convert.ToInt32(s[4])));
            }
        }
        private static void feladat_2()
        {
            Console.WriteLine("2. feladat");
            Console.WriteLine($"A buszra {adatok.Count()} utas akart felszállni.");
        }
        private static void feladat_3()
        {
            Console.WriteLine("3. feladat");
            var ervenytelen_irat_db = adatok.Where(x => !x.ervenyes()).Count();
            Console.WriteLine($"A buszra {ervenytelen_irat_db} utas nem szállhatott fel");
        }
        private static void feladat_4()
        {
            Console.WriteLine("4. feladat");
            var legtobb_utas = adatok.GroupBy(x => x.megallo).Select(x => new { megallo = x.Key, db = x.Count() })
                               .OrderByDescending(x => x.db).ThenBy(x => x.megallo).First();
            Console.WriteLine($"A legtöbb utas ({legtobb_utas.db} fő) a {legtobb_utas.megallo}. megállóban próbált felszállni");
        }
        private static void feladat_5()
        {
            Console.WriteLine("5. feladat");
            string[] ingyenes = new string[3] { "NYP", "RVS", "GYK" };
            string[] kedvezmenyes = new string[2] { "TAB", "NYB" };
            int ingyenes_utasok_db = adatok.Where(x => x.ervenyes() && ingyenes.Contains(x.irat_tipus)).Count();
            int kedvezmenyes_utasok_db = adatok.Where(x => x.ervenyes() && kedvezmenyes.Contains(x.irat_tipus)).Count();
            Console.WriteLine($"Ingyenesen utazók száma: {ingyenes_utasok_db} fő");
            Console.WriteLine($"A kedvezményesen utazók száma: {kedvezmenyes_utasok_db} fő");
        }
        private static int napokszama(int e1, int h1, int n1, int e2, int h2, int n2)
        {
            h1 = (h1 + 9) % 12;
            e1 = e1 - h1 / 10;
            int d1 = 365 * e1 + e1 / 4 - e1 / 100 + e1 / 400 + (h1 * 306 + 5) / 10 + n1 - 1;
            h2 = (h2 + 9) % 12;
            e2 = e2 - h2 / 10;
            int d2 = 365 * e2 + e2 / 4 - e2 / 100 + e2 / 400 + (h2 * 306 + 5) / 10 + n2 - 1;
            return d2 - d1;
        }
        private static void feladat_7()
        {
            var figyelmeztetett_utasok = adatok.Where(x => x.irat_tipus != "JGY" && x.ervenyes() &&
                                         napokszama(x.datum_ev(), x.datum_ho(), x.datum_nap(), x.ervenyesseg_ev(), x.ervenyesseg_ho(), x.ervenyesseg_nap()) <= 3
                                         ).ToList();
            File.WriteAllText("figyelmeztetes.txt", "");
            foreach (var utas in figyelmeztetett_utasok)
            {
                File.AppendAllText("figyelmeztetes.txt", $"{utas.azonosito} {utas.ervenyesseg_ev()}-{utas.ervenyesseg_ho():00}-{utas.ervenyesseg_nap():00}\n");
            }
        }
    }
}
