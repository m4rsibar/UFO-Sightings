using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text;
using System.Linq;

namespace UFOSightings
{
    class Program
    {
        static void Main(string[] args)
        {
            //Getting the path location to ufo.csv
            string currentDirectory = Directory.GetCurrentDirectory();
            var directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "ufo.csv");
            //call method to read ufo data and store in variable.
            var ufoData = ReadUfoResults(fileName);
            var fileContents = ufoData;

            //Menu
            StringBuilder menu = new StringBuilder();
            menu.Append("Welcome to UFO Sightings");
            menu.Append("\n=======================");
            menu.Append("\nTo return a list of all sightings enter 1.");
            menu.Append("\n============================================");
            menu.Append("\nTo sort sightings by date, press 2");
            menu.Append("\n============================================");
            menu.Append("\nTo sort sightings by location, press 3");
            menu.Append("\n============================================");
            menu.Append("\nEnter Q to quit");
            Console.ForegroundColor = ConsoleColor.Cyan;
            //Display the menu options to user.
            Console.WriteLine(menu.ToString());


            var input = Console.ReadLine();
            while (input.ToLower() != "q")
            {

                switch (input)
                {
                    case "1":
                        PrintList(ufoData);
                        //Console.WriteLine(fileContents.Count + " total number of passengers");
                        Console.WriteLine(menu.ToString());

                        break;

                    case "2":

                        var chooseDateRange = "";
                        StringBuilder dateMenu = new StringBuilder();
                        dateMenu.Append("Choose a decade");
                        dateMenu.Append("\n============================================");
                        dateMenu.Append("\n1940s || press 1");
                        dateMenu.Append("\n============================================");
                        dateMenu.Append("\n1950s || press 2");
                        dateMenu.Append("\n============================================");
                        dateMenu.Append("\n1960s || press 3");
                        dateMenu.Append("\n============================================");
                        dateMenu.Append("\n1970s || press 4");
                        dateMenu.Append("\n============================================");
                        dateMenu.Append("\n1980s || press 5");
                        dateMenu.Append("\n============================================");
                        dateMenu.Append("\n1990s || press 6");
                        dateMenu.Append("\n============================================");
                        dateMenu.Append("\n2000s || press 7");
                        dateMenu.Append("\n============================================");
                        dateMenu.Append("\n2010s || press 8");
                        Console.WriteLine(dateMenu.ToString());
                        var decade=Console.ReadLine();
                        int decadeParsed = 0;
                        Int32.TryParse(decade, out decadeParsed);

                        int chosenDecade = 0;

                        switch (decadeParsed)
                        {
                            case 1:
                                chosenDecade = 1940;
                                PrintDecade(chosenDecade);
                                //run method to write fitting sightings to screen. //
                                fileContents = DecadeSightings(ufoData,chosenDecade);
                                PrintList(fileContents);
                                break;
                            case 2:
                                chosenDecade = 1950;
                                break;
                            case 3:
                                chosenDecade = 1960;
                                break;
                            case 4:
                                chosenDecade = 1970;
                                break;
                            case 5:
                                chosenDecade = 1980;
                                break;
                            case 6:
                                chosenDecade = 1990;
                                break;
                            case 7:
                                chosenDecade = 2000;
                                break;
                            case 8:
                                chosenDecade = 2010;
                                break;
                            default:
                                Console.WriteLine("1 through 8 only.");
                                Console.WriteLine(dateMenu.ToString());
                                break;
                        }
                        PrintDecade(chosenDecade);
                        fileContents = DecadeSightings(ufoData, chosenDecade);
                        PrintList(fileContents);


                        break;

                    case "3":
                       //display loation info
                        break;
                }
                input = Console.ReadLine();
            }
            //end menu

       }

        public static List<UfoSighting> DecadeSightings(List<UfoSighting> sightings, int decade)
        {
            List<UfoSighting> decadeSighting = new List<UfoSighting>();

            decadeSighting = decadeSighting.OrderBy(x => x.SightingDate).ToList();
    
            foreach (UfoSighting sighting in sightings)
            {
                if ((sighting.SightingDate.Year >= decade) && (sighting.SightingDate.Year <= decade+9))
                {
                    decadeSighting.Add(sighting);
                }
            }
            return decadeSighting;
        }



        private static void PrintDecade(int chosenDecade)
        {
            Console.WriteLine($"You selected {chosenDecade}'s");
        }

        private static void PrintList(List<UfoSighting> sightings)
        {
            foreach (var sighting in sightings)
            {
                Console.WriteLine(sighting.ToString());
            }
        }

        private static void WriteUfoResults(List<UfoSighting> fileContents)
        {
            using (var writer = File.AppendText("UpdatedUfoSightings.csv"))
            {
                writer.WriteLine("DateTime,City,State,Country,Comments");
                foreach (var sighting in fileContents)
                {
                    writer.WriteLine(sighting.SightingDate + "," + sighting.City + "," + sighting.State +","+ sighting.Country +","+ sighting.Details);
                }
            }
        }

        public static List<UfoSighting> ReadUfoResults(string fileName)
        {
            var ufoResults = new List<UfoSighting>();
            using (var reader = new StreamReader(fileName))
            {
                string line = "";
                //read header line to skip it and get to the first line of data
                reader.ReadLine();
                while ((line=reader.ReadLine()) != null)
                {
                    var ufoSighting = new UfoSighting();
                    string[] values= line.Split(',');

                    DateTime sightingDate;
                    if (DateTime.TryParse(values[0], out sightingDate))
                    {
                        ufoSighting.SightingDate = sightingDate;
                    }
                    ufoSighting.Shape = values[4];
                    var city= CultureInfo.CurrentCulture.TextInfo.ToTitleCase(values[1]);
                    var state = values[2].ToUpper();
                    ufoSighting.City = city;
                    ufoSighting.State = state;
                    var country= CultureInfo.CurrentCulture.TextInfo.ToUpper(values[3]);
                    ufoSighting.Country = country;
                    ufoSighting.Details = values[7];
                    ufoResults.Add(ufoSighting);
                }
            }
            return ufoResults;
        }
    }
}

