/*
 * -------------------------------
 *
 * Autor: Đuro Belačić
 * Projekt: Domovi zdravlja
 * Predmet: Osnove programiranja
 * Ustanova: VŠMTI
 * Godina: 2020.
 *
 * -------------------------------
*/
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Data;

namespace E_DOM_ZDRAVLJA_VSMTI
{
    class Program
    {
        /*
         * -------------------------------------------------------         
         *
         * Struktura koja sadrzi informacije za prijavu korisnika
         *
         * -------------------------------------------------------
        */ 
        public struct Korisnik
        {
            public string unkorisnik;
            public string pwkorisnik;
            public string unadmin;
            public string pwadmin;
            public Korisnik(string uk, string pk, string ua, string pa)
            {
                unkorisnik = uk;
                pwkorisnik = pk;
                unadmin = ua;
                pwadmin = pa;
            }
        }
        /*
         * --------------------------------------------------------------         
         *
         * Struktura koja sadrzi informacije o ordinacijama doma zdravlja
         *
         * --------------------------------------------------------------
        */
        public struct Ordinacije
        {
            public int rbr;
            public string ordinacija;
            public string djelatnost;
            public string adresa;
            public string grad;
            public string doktor;
            public int sifra;
            public string sestra;
            public Ordinacije(int r, string o, string d, string a, string g, string dok, int sif, string s)
            {
                rbr = r;
                ordinacija = o;
                djelatnost = d;
                adresa = a;
                grad = g;
                doktor = dok;
                sifra = sif;
                sestra = s;
            }
        }
        /*
         * -----------------------------------------------------------------------------------         
         *
         * Funkcija koja korisniku omogućava povratak u glavni izbornik ili odjavu iz programa
         *
         * -----------------------------------------------------------------------------------
        */
        public static void Izbor()
        {
            Console.WriteLine('\n' + "Pritisnite ENTER za povratak u glavni izbornik ili ESC za izlaz iz programa");
            ConsoleKeyInfo KeyInfo = Console.ReadKey();
            if (KeyInfo.Key == ConsoleKey.Enter)
            {
                string path = @"logovi.txt";
                StreamWriter datoteka = new StreamWriter(path, true);
                var time = DateTime.UtcNow;
                datoteka.WriteLine(time + " - Povratak u glavni izbornik" + "\n");
                datoteka.Flush();
                datoteka.Close();
                Console.Clear();
            }
            else if (KeyInfo.Key == ConsoleKey.Escape)
            {
                string path1 = @"logovi.txt";
                StreamWriter datoteka1 = new StreamWriter(path1, true);
                var time1 = DateTime.UtcNow;
                datoteka1.WriteLine(time1 + " - Izlaz iz programa" + "\n");
                datoteka1.Flush();
                datoteka1.Close();
                System.Environment.Exit(0);
            }
        }
        /*
         * --------------------------------------  
         *
         * Funckija za prijavu korisnika kao user
         *
         * --------------------------------------
        */
        public static void PrijavaKorisnik(List<Korisnik> lKorisnik)
        {
            string path = @"logovi.txt";
            StreamWriter datoteka = new StreamWriter(path, true);
            var time = DateTime.UtcNow;
            datoteka.WriteLine(time + " - Prijava kao korisnik" + "\n");
            datoteka.Flush();
            datoteka.Close();
            int brojac = 0;
            bool prijava = false;
            while (prijava == false )
            {
                Console.WriteLine("Unesite korisnicko ime:");
                string ime = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Unesite korisnicku lozinku:");
                string lozinka = Convert.ToString(Console.ReadLine());
                foreach (Korisnik k in lKorisnik)
                {
                    {
                        if (ime == k.unkorisnik && lozinka == k.pwkorisnik)
                        {
                            Console.WriteLine("Uspjesna prijava!");
                            prijava = true;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Uneseno korisničko ime i/ili lozinka nisu ispravni! Pokušajte ponovo.");
                            brojac += 1;
                            if ( brojac > 2)
                            {
                                Console.WriteLine("Blokirani ste!");
                                System.Environment.Exit(0);
                            }
                        }
                    }
                }
            }
            Console.Clear();
            
        }
        /*
         * ------------------------------------------------        
         *
         * Varijabla koja govori koji se korisnik prijavio
         *
         * ------------------------------------------------
        */
        static bool is_admin = false;
        /*
         * -----------------------------------------------       
         *
         * Funckija za prijavu korisnika kao administrator
         *
         * -----------------------------------------------
        */
        public static void PrijavaAdmin(List<Korisnik> lKorisnik)
        {
            string path = @"logovi.txt";
            StreamWriter datoteka = new StreamWriter(path, true);
            var time = DateTime.UtcNow;
            datoteka.WriteLine(time + " - Prijava kao administrator" + "\n");
            datoteka.Flush();
            datoteka.Close();
            int brojac = 0;
            bool prijava = false;
            while (prijava == false) 
            {
                Console.WriteLine("Unesite korisnicko ime:");
                string ime = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Unesite korisnicku lozinku:");
                string lozinka = Convert.ToString(Console.ReadLine());
                foreach (Korisnik k in lKorisnik)
                {
                    if (ime == k.unadmin && lozinka == k.pwadmin)
                    {
                        Console.WriteLine("Uspješna prijava!");
                        Console.WriteLine();
                        prijava = true;
                        is_admin = true;
                    }
                    else
                    {
                        Console.WriteLine("Uneseno korisničko ime i/ili lozinka nisu ispravni! Pokušajte ponovo.");
                        brojac += 1;
                        if (brojac > 2)
                        {
                            Console.WriteLine("Blokirani ste!");
                            System.Environment.Exit(1);
                        }
                    }
                }
            }
            Console.Clear();
            
        }
        /*
        * ---------------------------     
        *
        * Funckija za tablicni zapis
        *
        * ---------------------------
       */
        static int tableWidth = 200;
        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }
        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";
            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }
            Console.WriteLine(row);
        }
        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        /*
        * ------------------------------------     
        *
        * Funckija za pregled svih ordinacija
        *
        * ------------------------------------
       */
        public static void PregledOrdinacija(List<Ordinacije> lOrdinacije)
        {
            string path = @"logovi.txt";
            StreamWriter datoteka = new StreamWriter(path, true);
            var time = DateTime.UtcNow;
            datoteka.WriteLine(time + " - Pregled svih ordinacija" + "\n");
            datoteka.Flush();
            datoteka.Close();
            PrintRow("REDNI BROJ","ORDINACIJA","DJELATNOST","ADRESA","GRAD","ZAPOSLENICI");
            PrintRow("");
            int rdBroj = 1;
            foreach (Ordinacije o in lOrdinacije)
            {
                PrintRow(rdBroj.ToString(),o.ordinacija, o.djelatnost,o.adresa,o.grad, o.doktor + ", " + o.sestra);
                rdBroj += 1;
            }
        }
        /*
        * -------------------------------------------------------      
        *
        * Funckija za pregled djelatnosti po gradovima te pregled 
        * ordinacija po rednom broju djelatnosti
        *
        * -------------------------------------------------------
       */
        public static void PregledDjelatnosti(List<Ordinacije> lOrdinacije)
        {
            string path = @"logovi.txt";
            StreamWriter datoteka = new StreamWriter(path, true);
            var time = DateTime.UtcNow;
            datoteka.WriteLine(time + " - Pregled djelatnosti po gradovima" + "\n");
            datoteka.Flush();
            datoteka.Close();
            Console.WriteLine("Gradovi: 1) Zagreb   2) Osijek   3) Virovitica   4) Split   5) Rijeka   6) Daruvar");
            Console.WriteLine("Unesite ime jednog od ponudenih gradova:");
            List<string> lSviGradovi = new List<string>();
            foreach (Ordinacije o in lOrdinacije)
            {
                if (lSviGradovi.Contains(o.grad) == false)
                {
                    lSviGradovi.Add(o.grad);
                }
            }
            string imegrada = "";
            while (lSviGradovi.Contains(imegrada) == false)
            {
                imegrada = Console.ReadLine();
                foreach (string s in lSviGradovi)
                {
                    if (lSviGradovi.Contains(imegrada) == false)
                    {
                        Console.WriteLine("Neispravan unos! Pokušajte ponovo.");
                        break;
                    }
                }
            }
            foreach (string s2 in lSviGradovi)
            {
                if (lSviGradovi.Contains(imegrada) == true)
                {
                    PrintRow("REDNI BROJ", "DJELATNOST");
                    PrintRow("");
                    break;
                }    
            }
            List<string> djelatnosti = new List<string>();
            int rdBroj = 1;
            double postotak = 0;
            foreach (Ordinacije o in lOrdinacije)
            {
                if (o.grad == imegrada)
                {     
                        if (djelatnosti.Contains(o.djelatnost) == false)
                        {
                            djelatnosti.Add(o.djelatnost);
                            foreach (string s in djelatnosti)
                            {
                                postotak = djelatnosti.Count() * 25;
                            }
                        }
                        PrintRow(rdBroj.ToString(), o.djelatnost);
                        rdBroj += 1;
                }
            }            
            Console.WriteLine ("Postotak zastupljenosti djelatnosti je " + postotak + "%");
            Console.WriteLine("\n" + "Djelatnosti: 1) Opca obiteljska medicina   2) Dentalna medicina   3) Pedijatrija   4) Ginekologija");
            Console.WriteLine("Unesite redni broj djelatnosti:");
            int rdBroj2 = 1;
            List<int> lRedniBrojevi = new List<int>();
            foreach (Ordinacije o in lOrdinacije)
            {
                if(lRedniBrojevi.Contains(o.rbr) == false)
                {
                    lRedniBrojevi.Add(o.rbr);
                }
            }
            int rednibroj = 0;
            while (lRedniBrojevi.Contains(rednibroj) == false)
            {
                rednibroj = Convert.ToInt32(Console.ReadLine());
                foreach (int i in lRedniBrojevi)
                {
                    if (lRedniBrojevi.Contains(rednibroj) == false)
                    {
                        Console.WriteLine("Neispravan unos! Pokušajte ponovo.");
                        break;
                    }
                }
            }
            foreach (int i2 in lRedniBrojevi)
            {
                if (lRedniBrojevi.Contains(rednibroj) == true)
                {
                    PrintRow("REDNI BROJ", "ORDINACIJA", "DJELATNOST", "ADRESA", "GRAD", "ZAPOSLENICI");
                    PrintRow("");
                    break;
                } 
            }
            foreach (Ordinacije o in lOrdinacije)
            {
                if (o.rbr == rednibroj)
                {
                    PrintRow(rdBroj2.ToString(), o.ordinacija, o.djelatnost, o.adresa, o.grad, o.doktor + ", " + o.sestra);
                    rdBroj2 += 1;
                }
            }
        }           
        /*
         * --------------------------------------------------------------       
         *
         * Funckija za pregled svih zaposlenika grupiranih po djelatnosti
         *
         * --------------------------------------------------------------
        */
        public static void PregledZaposlenika(List<Ordinacije> lOrdinacije)
        {
            string path = @"logovi.txt";
            StreamWriter datoteka = new StreamWriter(path, true);
            var time = DateTime.UtcNow;
            datoteka.WriteLine(time + " - Pregled svih zaposlenika" + "\n");
            datoteka.Flush();
            datoteka.Close();
            Console.WriteLine("Djelatnosti: 1) Opca obiteljska medicina   2) Dentalna medicina   3) Pedijatrija   4) Ginekologija");
            Console.WriteLine("Unesite redni broj djelatnosti:   ");
            List<int> lRedniBrojevi = new List<int>();
            foreach (Ordinacije o in lOrdinacije)
            {
                if (lRedniBrojevi.Contains(o.rbr) == false)
                {
                    lRedniBrojevi.Add(o.rbr);
                }
            }
            int djelatnost = 0;
            while (lRedniBrojevi.Contains(djelatnost) == false)
            { 
                djelatnost = Convert.ToInt32(Console.ReadLine());            
                foreach (int i in lRedniBrojevi)
                {
                    if(lRedniBrojevi.Contains(djelatnost) == false)
                    {
                        Console.WriteLine("Neispravan unos! Pokusajte ponovo.");
                        break;
                    }
                }
            }
            foreach (int i2 in lRedniBrojevi)
            {
                if(lRedniBrojevi.Contains(djelatnost) == true)
                {
                    PrintRow("REDNI BROJ", "IME I PREZIME", "TIP", "GRAD");
                    PrintRow("");
                    break;
                }
            }
            int rdBroj = 1;
            foreach (Ordinacije o in lOrdinacije)
            {
                if (o.rbr == djelatnost)
                {
                    PrintRow(rdBroj.ToString(), o.doktor, "1", o.grad);
                    rdBroj += 1;
                    PrintRow(rdBroj.ToString(), o.sestra, "2", o.grad);
                    rdBroj += 1;
                }
            }
            Console.WriteLine("1 - doktor, 2 - sestra");
        }
        /*
         * --------------------------------------------    
         *
         * Funckija za pregled statistike djelatnosti
         * (u kojim gradovima su pojedine djelatnosti)
         *
         * -------------------------------------------
        */
        public static void Statistika(List<Ordinacije> lOrdinacije)
        {
            string path = @"logovi.txt";
            StreamWriter datoteka = new StreamWriter(path, true);
            var time = DateTime.UtcNow;
            datoteka.WriteLine(time + " - Statistika djelatnosti" + "\n");
            datoteka.Flush();
            datoteka.Close();
            int counter = 1;
            List<string> gradovi1 = new List<string>();
            List<string> gradovi2 = new List<string>();
            List<string> gradovi3 = new List<string>();
            List<string> gradovi4 = new List<string>();
            foreach (Ordinacije k in lOrdinacije)
            {
                if (k.rbr == 1)
                {
                    if (gradovi1.Contains(k.grad) == false)
                    {
                        gradovi1.Add(k.grad);
                    }
                    
                }
                else if (k.rbr == 2)
                {
                    if (gradovi2.Contains(k.grad) == false)
                    {
                        gradovi2.Add(k.grad);
                    }
                }
                else if (k.rbr == 3)
                {
                    if (gradovi3.Contains(k.grad) == false)
                    {
                        gradovi3.Add(k.grad);
                    }
                }
                else if (k.rbr == 4)
                {
                    if (gradovi4.Contains(k.grad) == false)
                    {
                        gradovi4.Add(k.grad);
                    }
                }
            }
            gradovi1.Sort();
            gradovi2.Sort();
            gradovi3.Sort();
            gradovi4.Sort();
            PrintRow("REDNI BROJ", "DJELATNOST", "GRADOVI");
            PrintRow("");
            foreach (Ordinacije o in lOrdinacije)
            {
                if (o.rbr == counter)
                {
                    if (o.rbr == 1)
                    {
                        var comb = string.Join(", ", gradovi1);
                        PrintRow(o.rbr.ToString(),o.djelatnost,comb);
                        counter += 1;
                    }
                    else if (o.rbr == 2)
                    {
                        var comb2 = string.Join(", ", gradovi2);
                        PrintRow(o.rbr.ToString() ,o.djelatnost, comb2);
                        counter += 1;
                    }
                    else if (o.rbr == 3)
                    {
                        var comb3 = string.Join(", ", gradovi3);
                        PrintRow(o.rbr.ToString(), o.djelatnost, comb3);
                        counter += 1;
                    }
                    else if (o.rbr == 4)
                    {
                        var comb4 = string.Join(", ", gradovi4);
                        PrintRow(o.rbr.ToString(), o.djelatnost, comb4);
                        counter += 1;
                    }
                }
            }
        }
        /*
         * ------------------------------       
         *
         * Funckija za dodavanje doktora
         *
         * ------------------------------
        */
        public static void DodajDoktora(List<Ordinacije>lOrdinacije)
        {
            string path = @"logovi.txt";
            StreamWriter datoteka = new StreamWriter(path, true);
            var time = DateTime.UtcNow;
            datoteka.WriteLine(time + " - Dodaj doktora" + "\n");
            datoteka.Flush();
            datoteka.Close();
            Console.WriteLine("Unesite korisnicke podatke doktora");
            Console.Write("Ime: ");
            string Ime = Console.ReadLine();
            Console.Write("Prezime: ");
            string Prezime = Console.ReadLine();
            Console.Write("Sifra: ");
            int Sifra = Convert.ToInt32(Console.ReadLine());
            List<int> lSifre = new List<int>();
            foreach (Ordinacije o in lOrdinacije)
            {   
                if (lSifre.Contains(o.sifra) == false)
                {
                    lSifre.Add(o.sifra);
                }
            }
            while (lSifre.Contains(Sifra) == true)
            {
                foreach (int i in lSifre)
                {
                    if (lSifre.Contains(Sifra) == true)
                    {
                        Console.WriteLine("Unesena sifra vec postoji! Unesite drugu sifru.");
                        Console.Write("Sifra: ");
                        break;
                    }
                }
                Sifra = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("Grad: ");
            string Grad = Console.ReadLine();
            Console.Write("Adresa: ");
            string Adresa = Console.ReadLine();
            Console.Write("Djelatnost: ");
            string Djelatnost = Console.ReadLine();
            int Rbr = 0;
            if (Djelatnost.ToLower() == "opca obiteljska medicina")
            {
                Rbr = 1;
            }
            else if (Djelatnost.ToLower() == "dentalna medicina")
            {
                Rbr = 2;
            }
            else if (Djelatnost.ToLower() == "pedijatrija")
            {
                Rbr = 3;
            }
            else if(Djelatnost.ToLower() == "ginekologija")
            {
                Rbr = 4;
            }
            string Ordinacija = "Ordinacija dr." + Prezime;
            lOrdinacije.Add(new Ordinacije(Rbr, Ordinacija, Djelatnost, Adresa, Grad, Ime + " " + Prezime, Sifra, ""));   
            var convertedJson = JsonConvert.SerializeObject(lOrdinacije, Formatting.Indented);  
            StreamWriter oSr2 = new StreamWriter("ordinacije.json"); 
            using (oSr2)
            {
                oSr2.Write(convertedJson);  
            }

        }
        /*
         * ------------------------------       
         *
         * Funckija za dodavanje sestre
         *
         * ------------------------------
        */
        public static void DodajSestru(List<Ordinacije> lOrdinacije)
        {
            string path = @"logovi.txt";
            StreamWriter datoteka = new StreamWriter(path, true);
            var time = DateTime.UtcNow;
            datoteka.WriteLine(time + " - Dodaj sestru  " + "\n");
            datoteka.Flush();
            datoteka.Close();
            Console.WriteLine("Unesite korisnicke podatke: ");
            Console.Write("Ime: ");
            string Ime = Console.ReadLine();
            Console.Write("Prezime: ");
            string Prezime = Console.ReadLine();
            string Json = File.ReadAllText("ordinacije.json");   
            dynamic jsonObj = JsonConvert.DeserializeObject(Json); 
            bool praznoMjesto = false;
            int brojac = 0;
            foreach (var elementi in jsonObj)  
            {       
                brojac += 1;
            } 
            foreach (var elementi1 in jsonObj) 
            {
                if(elementi1.sestra == "")  
                { 
                    elementi1.sestra = Ime + " " + Prezime;   
                    praznoMjesto = true;
                }
            }
            int brojac2 = 0;
            if (praznoMjesto == false)  
            {
                Random run = new Random();
                int RdBroj = run.Next(1, brojac + 1);    
                foreach (var elementi2 in jsonObj)     
                {
                        brojac2 += 1;    
                        if (brojac2 == RdBroj)   
                        {
                            elementi2.sestra += ", " + Ime + " " + Prezime;  
                        }
                }
            }
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);  
            File.WriteAllText("ordinacije.json", output);                       
        }
        /*
         * ------------------------------       
         *
         * Funckija za odjavu iz programa
         *
         * ------------------------------
        */
        public static void Odjava(List<Ordinacije> lOrdinacije)
        {
            string path = @"logovi.txt";
            StreamWriter datoteka = new StreamWriter(path, true);
            var time = DateTime.UtcNow;
            datoteka.WriteLine(time + " - Odjava" + "\n");
            datoteka.Flush();
            datoteka.Close();
            System.Environment.Exit(1);
        }
        /*
         * ---------------------------------------------------       
         *
         * Funckija za ispis izbornika kako se zelite prijaviti
         *
         * ---------------------------------------------------
        */
        public static void IzborPrijave()
        {
            string path = @"logovi.txt";
            StreamWriter datoteka = new StreamWriter(path, true);
            var time = DateTime.UtcNow;
            datoteka.WriteLine(time + " - Izbor prijave" + "\n");
            datoteka.Flush();
            datoteka.Close();
            Console.WriteLine("1 - Prijava administrator");
            Console.WriteLine("2 - Prijava korisnik");
        }
        /*
         * ---------------------------------------------------       
         *
         * Funckija za odabir nacina kako se zelite prijaviti
         * (kao user ili administrator)
         *
         * ---------------------------------------------------
        */
        public static void Prijava1(List<Korisnik> lKorisnik)
        {
            int i = Convert.ToInt32(Console.ReadLine());
            switch (i)
            {
                case 1:
                    PrijavaAdmin(lKorisnik);
                    break;
                case 2:
                    PrijavaKorisnik(lKorisnik);
                    break;
                default:
                    Console.WriteLine("Krivi unos.");
                    break;
            }
        }
        /*
         * -------------------------------------------------       
         *
         * Funckija za ispis izbornika sa ponudenim opcijama 
         * koje mozete odabrati rednim brojem
         *
         * -------------------------------------------------
        */
        public static void Izbornik()
        {
                string path = @"logovi.txt";
                StreamWriter datoteka = new StreamWriter(path, true);
                var time = DateTime.UtcNow;
                datoteka.WriteLine(time + " - Pokretanje glavnog izbornika" + "\n");
                datoteka.Flush();
                datoteka.Close();
                Console.WriteLine("1)  Pregled svih ordinacija");
                Console.WriteLine("2)  Pregled djelatnosti po gradovima");
                Console.WriteLine("3)  Pregled svih zaposlenika");
                Console.WriteLine("4)  Statistika djelatnosti");
            if (is_admin == true)
            {
                Console.WriteLine("5)  Dodaj doktora/sestru");
            }
            int broj = 6;
            if(is_admin == false)        
            {
                broj = 5;
            }
                Console.WriteLine("{0})  Odjava", broj);
                Console.WriteLine();
                Console.Write("Odabir:   ");            
        }
        /*
         * ---------------------------------------------------------       
         *
         * Funckija za odabir jedne od ponudenih opcija iz izbornika
         *
         * ---------------------------------------------------------
        */
        public static void Odabir(List<Ordinacije> lOrdinacije)
        {
            int i = Convert.ToInt32(Console.ReadLine());
            switch (i)
            {
                case 1:
                    PregledOrdinacija(lOrdinacije);                    
                    Izbor();
                    break;
                case 2:
                    PregledDjelatnosti(lOrdinacije);                     
                    Izbor();
                    break;
                case 3:
                    PregledZaposlenika(lOrdinacije);                     
                    Izbor();
                    break;
                case 4:
                    Statistika(lOrdinacije);                     
                    Izbor();
                    break;
                case 5:
                    if (is_admin == false)
                    {
                        Odjava(lOrdinacije);
                    }
                    else
                    {
                        Console.WriteLine("1) Dodaj doktora   2) Dodaj sestru");
                        string odabir2 = Console.ReadLine();
                        if (odabir2.ToLower() == "1")
                        {
                            DodajDoktora(lOrdinacije);
                        }
                        else if (odabir2.ToLower() == "2")
                        {
                            DodajSestru(lOrdinacije);
                        }
                        else
                        {
                            Console.WriteLine("Neispravan odabir");
                        }
                        Izbor();
                    }                    
                    break;                 
                case 6:
                    Odjava(lOrdinacije);                     
                    break;
                default:
                    Console.WriteLine("Greska! Neispravan odabir");
                    break;
            }
        }
        /*
         * ------------------------------------------------------------------       
         *
         * Funckija za ispis izbornika za odabir dodavanja doktora ili sestre
         *
         * ------------------------------------------------------------------
        */
        public static void DodajIzbornik()
        {
            Console.WriteLine("1 - Dodaj doktora");
            Console.WriteLine("2 - Dodaj sestru");
        }
        static void Main(string[] args) 
        {
            /*
            * ------------------------------------------       
            *
            * Citanje Json datoteke za prijavu korisnika
            *
            * ------------------------------------------
            */
            List<Korisnik> lKorisnik = new List<Korisnik>();
            string sJson;
            StreamReader oSr = new StreamReader("config.json");
            using (oSr)
            {
                sJson = oSr.ReadToEnd();
                lKorisnik = JsonConvert.DeserializeObject<List<Korisnik>>(sJson);
            }
            /*
            * ------------------------------------------       
            *
            * Citanje Json datoteke za popis ordinacija
            *
            * ------------------------------------------
            */
            List<Ordinacije> lOrdinacije = new List<Ordinacije>();
            string sJson2;
            StreamReader oSr2 = new StreamReader("ordinacije.json");
            using (oSr2)
            {
                sJson2 = oSr2.ReadToEnd();
                lOrdinacije = JsonConvert.DeserializeObject<List<Ordinacije>>(sJson2);
            }
            /*
            * ------------------------------------------       
            *
            * Poziv funkcije za izbor prijave
            *
            * ------------------------------------------
            */
            IzborPrijave(); //poziv funkcije za izbor prijave
            /*
            * ------------------------------------------       
            *
            * Poziv funkcije za prijavu kao administator ili obični korisnik
            *
            * ------------------------------------------
            */
            Prijava1(lKorisnik); //poziv funckije za prijavu kao administrator ili korisnik
            while (true) 
            {
                /*
                * -----------------------------------------------------------       
                *
                * Poziv funkcije za otvaranje izbornika sa ponudenim opcijama
                *
                * -----------------------------------------------------------
                */

                Izbornik(); //poziv funkcije za otvaranje izbornika sa ponudenim opcijama

                /*
                * -----------------------------------------------------------       
                *
                * Poziv funkcije za odabir jedne od ponudenig opcija opcija   
                *
                * -----------------------------------------------------------
                */
                Odabir(lOrdinacije);   
            }
        }
    }
}

       


