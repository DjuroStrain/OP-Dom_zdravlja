using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace E_DOM_ZDRAVLJA_VSMTI
{
    class Program
    {
        public struct Korisnik
        {
            public string username;
            public string password;

            public Korisnik(string u, string p)
            {
                username = u;
                password = p;
            }
        }
        static void prijava(List<Korisnik> lKorisnik)
        {
           foreach (Korisnik k in lKorisnik)
            {
                Console.WriteLine("Unesite korisnicko ime:");
                string ime = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Unesite korisnicku lozinku:");
                string lozinka = Convert.ToString(Console.ReadLine());
                if (ime == k.username && lozinka == k.password)
                {
                    Console.WriteLine("Uspješna prijava!");
                }
                else
                {
                    Console.WriteLine("Uneseno korisničko ime i/ili lozinka nisu ispravni!");
                }
            }
             
        }
        static void Main(string[] args)
        {
            List<Korisnik> lKorisnik = new List<Korisnik>();
            string sJson;
            StreamReader oSr = new StreamReader("config.json");
            string oJson = "";
            using (oSr)
            {
                sJson = oSr.ReadToEnd();
                lKorisnik = JsonConvert.DeserializeObject<List<Korisnik>>(sJson);
            }
            prijava(lKorisnik);
            Console.ReadKey();
        }
    }
}
//dodati korisnika i admina