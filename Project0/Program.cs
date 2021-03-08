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
namespace Project0
{
    class Program
    {
        static void Main(string[] args)
        {


            // teller omzetten van int naar Datetime object mm:ss
            //Eerst maken van hoofdmenu maken 
            // User kan kiezen voor 
            // Hoe?  de user krijgt een menu
            // read key()

            menu();
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
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
               
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
                                Console.WriteLine("Hello mama");
                                break;
                            case 3:
                                clearConsole();
                                inloggenGebruiker();
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


        public  static void welkomPagina(string username)
        { 
            
            string welkomTekst = $"Welkom {username}, Welkom bij GameX";
            Console.WriteLine($"Welkom {username}, Welkom bij GameX");
            Console.WriteLine(new string('=', welkomTekst.Length));
            string input = string.Empty;
            // Display the time every so often
            toonTijd();
            
            

            
            
        
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


        public static void inloggenGebruiker()
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
                                Console.WriteLine(inlogSuccess);
                                
                            }

                        }
                        
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

           

            
           

        }

        // Verwijderen van User 

        // Update van username & Passwoord

        //


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


     }
}
