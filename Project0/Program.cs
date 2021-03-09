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
            memory();



            
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
                        if (reader.ReadLine().Equals(username))
                        {

                            TextWriter verwijderZin = new StreamWriter(fileName);
                            // Pak de gebruiker 
                            verwijderZin.Write(string.Empty);
                            Console.WriteLine(reader.ReadLine());
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
            Console.SetCursorPosition(50, 50);
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
                                authenticatieGebruiker();
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
        public static void timer()
        {
            // Pak een Datetime.now minutes and seconds start 
            // Doe een thread om de 2000 seconden
            // Zet dat om naar een variable speeltijd die het verschil neemt tussen elke tijd dat er een thread is ti
            
            string now = DateTime.Now.ToString("mm:ss");
            

        }


        public  static void welkomPagina(bool inlog)
        {
            string username = "";
            if (!username.Equals(string.Empty))
            {

                string welkomTekst = $"Welkom {username}, Welkom bij GameX";
                Console.WriteLine($"Welkom {username}, Welkom bij GameX");
                Console.WriteLine(new string('=', welkomTekst.Length));
                string input = string.Empty;
                // Display the time every so often
                toonTijd();

                int geld;

            }
            else
            {
                Console.WriteLine("U kon niet succesvol inloggen");
                menu();

            }




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
            string wachtwoord = string.Empty;
            string[] authenticatie = new string[2];
            string[] leeg = new string[2];

            //Check

            if (resultaat)
            {
                authenticatie[0] = username;
                //Wachtwoord moet in hashvorm komen
                authenticatie[1] = ComputeSha256Hash(wachtwoord);
                Console.WriteLine(authenticatie[1]);
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

                        //Check of de een username in de gegvens is 
                        allRegels = reader.ReadToEnd();

                        string[] lines = allRegels.Split(',');
                        foreach (var line in lines)
                        {
                            if (lines[0] == gegevens[0])
                            {
                                // Als dat zo is redirect the user
                                Console.WriteLine("Jouw username staat al in onze gegevens");
                                //Redirect to the inlogpage
                                inloggenGebruiker();
                            }
                        }

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


        public static bool inloggenGebruiker()
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
                        string wachtwoord = user.Substring(username.Length + 1);

                        if (username.Equals(input))
                        {
                            if (input2.Equals(wachtwoord))
                            {
                                inlogSuccess = true;
                                // Doorverwijzen naar menu van games
                                
                            }

                        }
                        
                    }
             

                    
                }
                if (inlogSuccess)
                    return inlogSuccess;
                else
                    Console.WriteLine("U kon niet succesvol inloggen");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;

           

            
           

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
        public static void memory()
        {
            // Maken van 2 Arrays ==> 1 Oplossing, en 1 voor vraagen
            // tekens vragen + Maak een random getal tussen begin en aantal
            // Maak een switch met de kleuren voor de verschillende tekens
            // Teller maken van 1 -10
            // Laten verdwijnen van resultaten
            // Hoe checken resultaten. 
            // Kaart 

            char[] raadKaarten = new char[10];
            string[] Vraagtekens = new string[10];
            string ruimte = new string(' ', 5);
            string ruimteKaarten = new string(' ', 4);
            Random rnd = new Random();
            

            foreach (var getal in Enumerable.Range(0, 10))
            {
                int randomGetal = rnd.Next(2, 7);
                Console.Write(" " + $"{getal+1}" + ruimte);
                Vraagtekens[getal] = "[?]";
                raadKaarten[getal] = (char)randomGetal;
                
            }

            Console.WriteLine();
            List<string> lijstKaarten = combineerArrayInList(raadKaarten, Vraagtekens);
            int teller = 0;
            string input = string.Empty;

            do
            {

                for (int i = 0; i < lijstKaarten.Count(); i++)
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
                        teller += 1;

                    }
                    else
                    {
                        if (teller == raadKaarten.Length)
                        {
                            Thread.Sleep(5000);
                            clearConsole();
                        }


                    }

                    Console.Write("[" + lijstKaarten[i] + "]" + ruimteKaarten);
                    Console.ResetColor();
                }

                input = Console.ReadLine();


            } while (input != "s");








        }

        public static void slotMachine(int prijs)
        {
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

                // Check als 1 van  de 3 charaters op een rij gelijk zijn aan elkaar
                // Dan wint hij 
                // Maak een teller 
                // Sla het vorige teken op 
                // Vergelijk dan het huidige teken met vorige teken
                // Als teller is 0 dan niet opslaan
                // Als teller 2 is dan stoppen
                // Vergelijk elke symbool met elkaar
                // 1 Optellen als symbool groter is dan 
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

                            key = ConsoleKey.Escape;
                        }
                        else
                            prijs -= 10;

                    }

                }
                else
                {
                    Console.Write("Je kan alleen enter drukken");
                }


               Thread.Sleep(1500);
               clearConsole();


            } while (!key.Equals(ConsoleKey.Escape));

            
            Console.WriteLine();

            Console.WriteLine("Finished");

            Console.WriteLine($"Geld: {prijs}");

            





        }
     }
}
