using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Runtime;
using System.Collections.Generic;

namespace Project0
{
    class Program
    {
        public static object MessageBox { get; private set; }

        static void Main(string[] args)
        {
            menu();



            
        }
        public static void GebruikerBewerken()
        {
            // Willen updaten van username of wachtwoord
            
        
        }

        public static void GebruikerVerwijderen()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            string fileName = vindBestand();
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    
                    try
                    {
                        string checkUsername = reader.ReadLine().Split(',')[0];
                        if (checkUsername.Equals(username))
                        {
                            Console.WriteLine(reader.ReadLine());
                            TextWriter verwijderZin = new StreamWriter(fileName);
                            // Pak de gebruiker 
                            verwijderZin.Write(string.Empty);
                            Console.WriteLine(reader.ReadLine());
                            Thread.Sleep(5000);
                            verwijderZin.Flush();
                            verwijderZin.Close();
                        }

                    }catch (IOException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    
                    
                }

                Console.WriteLine("aldfsljdf");

            }
        }






        public static string vindBestand()
        {
            string directory = Directory.GetCurrentDirectory();
            DirectoryInfo currentDirectory = new DirectoryInfo(directory);
            string path = currentDirectory.FullName + @"\gegevens.txt";
            return path;

        }
        public static void menu()
        {
      

            string[] keuzesMenu = { "Gebruiker toevoegen", "Gebruiker bewerken", "Gebruiker verwijderen", "Inloggen" };
            Console.CursorVisible = false;

            int huidigePositie = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                
                for (int i = 0; i < keuzesMenu.Length; i++)
                {
                    if (huidigePositie > 4)
                        huidigePositie = 3;

                    if (huidigePositie < 0)
                        huidigePositie = 0;


                    if (i == huidigePositie)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
               
                    }
                        
                        

                    Console.WriteLine(keuzesMenu[i]);

                    Console.ResetColor();


                }
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        huidigePositie--;
                        break;
                    case ConsoleKey.DownArrow:
                        huidigePositie++;
                        break;
                    case ConsoleKey.Escape:
                        break;
                    case ConsoleKey.Enter:
                        switch (huidigePositie)
                        {
                            case 0:
                                clearConsole();
                                SchrijfGeberuikerNaarBestand(authenticatieGebruiker());
                                break;
                            case 1:
                                clearConsole();
                                Console.WriteLine("Hello world");
                                break;
                            case 2:
                                clearConsole();
                                GebruikerVerwijderen();

                                break;
                            case 3:
                                clearConsole();
                                welkomPagina(inloggenGebruiker());
                                break;
                            default:
                                break;
                        }

                        break;
                    default:
                        Console.WriteLine("Die toets kan je niet gebruiken");
                        break;
                }

                // Als de gebruiker enter indrukt verwijzen naar pagina
                // Afhankelijk van enter drukken dan verwijzen

            } while (key != ConsoleKey.Escape);



        }
        public static void toonTijd()
        {
            DateTime now = new DateTime();
            // reload the function every thread so often 
            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            

            // start tijd

        }
        public static void timer(Object stateInfo)
        {
            Console.WriteLine("Tick: {0}", DateTime.Now.ToString("h:mm:ss"));


        }

        public static int CheckKeuze(bool keuze, int getal)
        {            

            while (!keuze)
            {
                Console.WriteLine("Niet goed ! Je kan alleen getallen ingeven ...");

                Console.Write(": ");

                keuze = int.TryParse(Console.ReadLine(), out getal);
            }

            return getal;

        }
        public  static void welkomPagina(string username)
        {
            clearConsole();
            ConsoleKey key = ConsoleKey.Enter;
            TimerCallback callback = new TimerCallback(timer);
            // create a one second timer tick
            Console.WriteLine(username);
            
            int geld = 100;
            do
            {

                if (!username.Equals("U kon niet succesvol inloggen"))
                {

                    int getal, getal2;

                    if (geld == 0)
                    {
                        Thread.Sleep(1000);
                        clearConsole();
                        Console.WriteLine("U moet terug geld storten om te kunnen spelen!!!");
                        bool stortGeld = int.TryParse(Console.ReadLine(), out getal2);
                        geld = CheckKeuze(stortGeld, getal2);
                        System.Threading.Timer stateTimer = new System.Threading.Timer(callback, null, 0, 1000);

                        // Simulating other work (10 seconds)
                        Console.WriteLine("Speeltijd {0}\n",
                             DateTime.Now.ToString("h:mm:ss"));
                        Thread.Sleep(5000);
                        clearConsole();

                    }

                    string welkomTekst = $"Welkom {username}, bij GameX";
                    Console.WriteLine(welkomTekst);
                    Console.WriteLine(new string('=', welkomTekst.Length));
                    string input = string.Empty;
                    // Display the time every so often
                    toonTijd();
                    string[] spellen = { "Blackjack", "Memory", "Slot Machine" };

                    Console.WriteLine($"U heeft {geld} euro");


                    Console.Write("Als je wil stoppen druk dan Escape");
                    key = Console.ReadKey(true).Key;
                    if (key.Equals(ConsoleKey.Escape))
                    {
                        Console.Write("U wordt uitgelogd");
                        Thread.Sleep(1000);
                        break;

                    }
                        
                    Console.WriteLine();

                    Console.WriteLine("U kan kiezen uit de volgende spellen: ");
                    for (int i = 0; i < spellen.Length; i++)
                        Console.WriteLine($"{i + 1}: {spellen[i]}");
                    Console.Write("Maak uw keuze: ");
                    bool checkKeuze = int.TryParse(Console.ReadLine(), out getal);
                    getal = CheckKeuze(checkKeuze, getal);
                    switch (getal)
                    {
                        case 1:
                            Console.WriteLine("Blackjack");
                            //Blackjack
                            break;
                        case 2:
                            geld = memory(geld);
                            break;
                        case 3:
                            geld = slotMachine(geld);
                            break;
                        default:
                            Console.WriteLine("Dit getal bestaat niet");
                            Thread.Sleep(1000);
                            clearConsole();
                            break;

                    }
                    


                }
                else
                {
                    Console.WriteLine(username);
                    menu();

                }
                key = Console.ReadKey(true).Key;


            } while (!key.Equals(ConsoleKey.Escape));

            
            


        }
        // open a file and write to CSV file 
        // Authenticatie
        public static string[] authenticatieGebruiker()
        {
            // Vragen achter username 
            Console.Write("Vul je username in: ");
            string input = Console.ReadLine().ToLower();
            string username = string.Empty;
            int goedeChar = 0;


            // Alleen letters en cijfers
            for (int i = 0; i < input.Length; i++)
            {
                int character = (int)input[i];

                if ((character >= 48 && character <= 57) || (character >= 97 && character <= 122))
                    goedeChar += 1;

            }
            

            if (goedeChar == input.Length)
                username = input;

            else
                Console.WriteLine("De username was niet goed. U mag alleen letters en getallen ingeven");

            string[] checkOfUsernameBestaat = { username, " " };
            SchrijfGeberuikerNaarBestand(checkOfUsernameBestaat);



            // Checken of username voldoet aan voorwaarden
            // Vragen password

            Console.Write("Geef je password \n!!!!! 1 hoofdletter, 1 letter, 1 getal, 1 vreemd teken \n En uw wachtwoord moet 8-16 characters lang zijn !!!!!!!\n:  ");
            string input2 = Console.ReadLine();
            // Voorwaarden wachtwoord 
            //Regex re = new Regex(input2, pattern
            bool resultaat = !Regex.IsMatch(input2, @"^[a-zA-Z0-9]+$") && (input2.Length >= 8 && input.Length <= 16) && 
                input2.Any(char.IsUpper) && 
                input2.Any(char.IsLower) &&
                input2.Any(char.IsDigit) ? true : false;
            string[] authenticatie = new string[2];
            string[] leeg = new string[2];

            //Check

            if (resultaat)
            {
                authenticatie[0] = username;
                //Wachtwoord moet in hashvorm komen
                authenticatie[1] =  ComputeSha256Hash(input2);
                return authenticatie;
                


            }
            else
                Console.WriteLine("Uw heeft geen goed wachtwoord ingegeven.Check of de voorwaarden kloppen");


            return leeg;
          

        }
        // een nieuwe CSV bestand in huidige folder

        // Open CSV bestand, 

        //Maak een 
        public static void SchrijfGeberuikerNaarBestand(string[] gegevens)
        {
            var fileName = maakBestand();
            var reader = new StreamReader(fileName);
            var allRegels = string.Empty;
            // Bestand bestaat al
            try
            {
                //Check of de gebruikers data is in gegevens
                using (reader)
                { 
                    allRegels = reader.ReadToEnd();

                }

                using (var writer = new StreamWriter(fileName))
                {
                    if (allRegels.Equals(String.Empty))
                    {
                        string gegeven = $"{gegevens[0]},{gegevens[1]}";
                        writer.WriteLine(gegeven);

                    }
                    else
                    {
                        //Check of de een username in de gegvens is 
                        string[] lines = allRegels.Split(',');
                        foreach (var line in lines)
                        {
                            if(lines[0] == gegevens[0])
                            {
                                // Als dat zo is redirect the user
                                Console.WriteLine("Jouw username staat al in onze gegevens");
                                //Redirect to the inlogpage
                                inloggenGebruiker();
                            }
                        }
                        Console.WriteLine(gegevens[0]);
                        Console.WriteLine(gegevens[1]);
                        string gegeven = $"{gegevens[0]},{gegevens[1]}";
                        writer.WriteLine(gegeven);

                    }
                  
                }
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.Message);
              
            }
            
        }
        public static void clearConsole()
        {
            Console.Clear();

        }
                   
   
        public  static string maakBestand()
        {

            string directory = Directory.GetCurrentDirectory();
            DirectoryInfo currentDirectory = new DirectoryInfo(directory);
            string path = currentDirectory.FullName + @"\gegevens.txt";
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (FileStream fs = File.Create(path))
                {
                    return path;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return " ";


        }


        public static string inloggenGebruiker()
        {
            Console.Write("Vul je username in: ");
            string input = Console.ReadLine().ToLower();

            Console.Write("Geef je wachtwoord: ");
            string input2 = ComputeSha256Hash(Console.ReadLine()); 



            //Lees bestand 
            string directory = Directory.GetCurrentDirectory();
            DirectoryInfo currentDirectory = new DirectoryInfo(directory);
            string path = currentDirectory.FullName + @"\gegevens.txt";
            bool inlogSuccess = false;

            try
            {
                using (var reader = new StreamReader(path))
                {
                    while (reader.Peek() > -1)
                    {
                        string user = reader.ReadLine();
                        //Split the username
                        string username = user.Substring(0, user.IndexOf(','));
                        string wachtwoord = user.Substring(username.Length+1);


                 
                        if (username.Equals(input))
                        {
                         

                            if (input2.Equals(wachtwoord))
                            {

                                // Doorverwijzen naar menu van games
                                return username;

                            }
                            else
                                return "U kon niet succesvol inloggen";

                        }
                        else
                            return "U kon niet succesvol inloggen";


                        
                    }
             

                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return " ";

           

            
           

        }


        private static string ComputeSha256Hash(string rawData)
        {

            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        /*Maken van spellen 
         * 
         * Beginnen met Slot Machine
         * 
         * 
         * 
         * 
         * Kleur geven voor te laten tonen aan de gebruiker
         *  
         */


        public static List<char> geefSymbolen()
        {
           
            List<char> tekens = new List<char>();
            for (int i = 2; i <= 6; i++)
            {
                tekens.Add((char)i);
                //Wat kleur geven
                
            }

  
            return tekens;

        }

        public static int[] countNumberOf(int[] symboolNaarGetal)
        {
            int getal = symboolNaarGetal.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;
            int maxNumber = 0;
            foreach (int i in symboolNaarGetal)
            {
                if (i == getal)
                    maxNumber += 1;
             
            }
            int[] getallen = { maxNumber, getal };
            return getallen;


            

        }

        public static List<string> combineerArrayInList(char[] raadKaarten, string[] vraagTekens)
        {
            // Omzetten van char raadkaarten array naar string raadkaarten
            string[] raadKaarten2 = raadKaarten.Select(c => c.ToString()).ToArray();

            List<string> vraagOplossingKaartenSpel = new List<string>();
            vraagOplossingKaartenSpel.AddRange(raadKaarten2);
            vraagOplossingKaartenSpel.AddRange(vraagTekens);

            return vraagOplossingKaartenSpel;

        }

        public static void checkDiagonaal()
        {
            //

        }
        
        public static int memory(int geld)
        {
            
            Thread.Sleep(2000);
            clearConsole();
            Console.WriteLine("Welkom bij Memory: Een spel waar je tijd krijgt om een combinatie te onthouden");

            char[] raadKaarten = new char[10];
            string[] Vraagtekens = new string[10];
            string ruimte = new string(' ', 5);
            string ruimteKaarten = new string(' ', 4);
            Random rnd = new Random();


            foreach (var getal in Enumerable.Range(0, 10))
            {
                int randomGetal = rnd.Next(2, 7);
                Console.Write(" " + $"{getal + 1}" + ruimte);
                Vraagtekens[getal] = "?";
                raadKaarten[getal] = (char)randomGetal;

            }

            Console.WriteLine();
            List<string> lijstKaarten = combineerArrayInList(raadKaarten, Vraagtekens);
            int teller = 0;
            string input = string.Empty;


            for (int i = 0; i < raadKaarten.Length; i++)
            {

                if (i < raadKaarten.Length)
                {
                    //Kleuren er bij zetten
                    switch (raadKaarten[i])
                    {
                        case (char)2:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case (char)3:
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            break;
                        case (char)4:
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            break;
                        case (char)5:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                        case (char)6:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                    }

                    Console.Write("[" + lijstKaarten[i] + "]" + ruimteKaarten);

                }
         

                Console.ResetColor();
            }
            Console.WriteLine();
            Console.WriteLine("ONTHOUT DIT");
            Thread.Sleep(5000);
            clearConsole();


            int teller1 = 1;
            ConsoleKey key;
            char keuze = ' ';
            char teken = ' ';
            do {
                // Maak een combinatie van vraagtekens
                for (int j = 1; j < Vraagtekens.Length + 1; j++)
                {
                    Console.Write(" " + j + ruimte);

                }

                Console.WriteLine();
                int randomTeken = rnd.Next(2, 7);
                

                for (int k = 0; k < Vraagtekens.Length; k++)
                {
                    // Als zijn keuze juist is dan laten tonen van resultaat
                    // Keuze onthouden
                    if (teken == raadKaarten[k])
                    {
                        switch (raadKaarten[k])
                        {
                            case (char)2:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                            case (char)3:
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                break;
                            case (char)4:
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                break;
                            case (char)5:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                break;
                            case (char)6:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                break;
                        }

                        Console.Write("[" + raadKaarten[k] + "]" + ruimteKaarten);
                        Console.ResetColor();

                    }

                    else
                        Console.Write("[" + Vraagtekens[k] + "]" + ruimteKaarten);
                }
                Console.WriteLine();
                Console.WriteLine("Keuzes zijn:  'h'artje, 'k'lavertje, 'l'achje, 'r'uitje, 'b'ladje  ");
                Console.Write($"Kaart {teller+1}: ");
                keuze = Convert.ToChar(Console.ReadLine());

                // Eerst nog omzetten van char
                switch (keuze)
                {
                    case 'h':
                        teken = (char)3;
                        break;
                    case 'k':
                        teken = (char)5;
                        break;
                    case 'l':
                        teken = (char)2;
                        break;
                    case 'r':
                        teken = (char)4;
                        break;
                    case 'b':
                        teken = (char)6;
                        break;
                    default:
                        Console.WriteLine("Deze keuze bestaat niet");
                        break;
                
                }

                key = Console.ReadKey(true).Key;
                
                teller++;
                
                if (teller == 9)
                    key.Equals(ConsoleKey.Escape);
                clearConsole();

            } while (!key.Equals(ConsoleKey.Escape));

            return geld;
          
            // Een deel van de random kaarten laten zien






        }

        public static int slotMachine(int prijs)
        {

            Thread.Sleep(2000);
            clearConsole();
            Console.WriteLine("Welkom bij het slotmachine: ");
            ConsoleKey key;
           

            do
            {
                
                char[,] slotMachine = new char[3, 3];
                Random rnd = new Random();
                List<char> tekens = geefSymbolen();
                int winOfVerlies = 0;



                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int randomGetal = rnd.Next(0, 5);
                        char teken = tekens[randomGetal];
                        // Zet de tekens in de array
                        slotMachine[i, j] = teken;


                    }
                }


                Console.WriteLine("SLOT MACHINE!");

            
                int[] symboolNaarGetal = new int[3];

                Console.WriteLine("Druk Enter om de slot machine te laten draaien...");
                key = Console.ReadKey(true).Key;

                if (key.Equals(ConsoleKey.Enter))
                {
                    Console.WriteLine();

                    for (int k = 0; k < 3; k++)
                    {
                        Console.Write("[" + " ");
                        for (int l = 0; l < 3; l++)
                        {
                            //Omzetten van char[] naar getal 
                            symboolNaarGetal[l] = (int)slotMachine[k, l];

                            //Kleuren er bij zetten
                            switch (slotMachine[k, l])
                            {
                                case (char)2:
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    break;
                                case (char)3:
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    break;
                                case (char)4:
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    break;
                                case (char)5:
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    break;
                                case (char)6:
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    break;


                            }
                            Console.Write($"{slotMachine[k, l]}" + " ");
                            Console.ResetColor();


                        }
                        // Tell het aantal occurences 
                        Console.WriteLine("]");
                        winOfVerlies = countNumberOf(symboolNaarGetal)[0];

                        //
                        if (winOfVerlies == 3)
                        {
                            switch (countNumberOf(symboolNaarGetal)[1])
                            {
                                case 2:
                                    prijs += 3;
                                    break;
                                case 3:
                                    prijs += 5;
                                    break;
                                case 4:
                                    prijs += 7;
                                    break;
                                case 5:
                                    prijs += 10;
                                    break;
                                case 6:
                                    prijs += 20;
                                    break;
                            }

                        }
                        else
                            prijs -= 10;

                    }

                }
                else
                {
                    Console.Write("Je kan alleen enter drukken");
                }

               Console.WriteLine(prijs);
               Thread.Sleep(1500);
               clearConsole();
               if (prijs <= 0)
                    break;


            } while (!key.Equals(ConsoleKey.Escape));
            prijs = 0;

            Console.WriteLine();

            Console.WriteLine("Finished");

            Console.WriteLine($"Geld: {prijs}");

            return prijs;

            





        }
     }
}
