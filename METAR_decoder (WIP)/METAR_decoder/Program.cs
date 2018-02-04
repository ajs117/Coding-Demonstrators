using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METAR_decoder
{
    using CustomExtensions;
    using System.Text.RegularExpressions;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** METAR Decoder ***");
            Console.WriteLine("Please enter the METAR you would like to decode.");

            string metar = Console.ReadLine();

            if (metar.Length > 0)
            {
                string[] mets = metar.Split(' ');

                for (int i = 0; i < mets.Count(); i++)
                {
                    bool success = MetDecoder(i, mets[i]);

                    if (!success)
                    {
                        Console.WriteLine("ERROR OCCURED!!!");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("No input found.");
            }
        }

        private static bool MetDecoder(int pos, string met)
        {
            if (pos == 0 && met != "METAR")
            {
                Console.WriteLine("METAR should start with METAR.");
                return false;
            }

            if (Regex.IsMatch(met, @"^[A-Z]{4}$"))
            {
                ICAOfinder(met);
            }

            if (Regex.IsMatch(met, @"^\d{6}[z]$"))
            {
                char[] times = met.ToCharArray();
                Console.WriteLine("Day: " + times[0] + times[1]);
                Console.WriteLine("Time: " + times[2] + times[3] + ":" + times[4] + times[5] + " Zulu (GMT/UTC)");



                return true;
            }

            if (Regex.IsMatch(met, @"\d{6}[z]"))
            {
                string units;

                if (met.Contains("KT"))
                {
                    units = " Knots";
                }
                else
                {
                    units = " Meters per second";
                }

                char[] components = met.ToCharArray();

                Console.WriteLine("Wind direction: " + components[0] + components[1] + components[2] + " degrees");
                Console.WriteLine("Wind speed: " + components[3] + components[4] + units);

                return true;
            }

            if (Regex.IsMatch(met, @"^\d{3}[V]\d{3}$"))
            {
                string[] directions = met.Split('V');

                Console.WriteLine("Wind variance: from " + directions[0] + " to " + directions[1] + " degrees");

                return true;
            }

            if (Regex.IsMatch(met, @"^\d{4}$"))
            {
                string feet = met.ToFeet();

                Console.WriteLine("Visibility: " + met + " meters (" + feet + " feet)");

                return true;
            }

            if (Regex.IsMatch(met, @"^[Q]\d{4}$"))
            {
                string pressure = met.Substring(1, 4);

                string inhg = pressure.ToInHg();

                Console.WriteLine("Pressure: " + pressure + " hPa (" + inhg + " inHg)");
            }

            if (Regex.IsMatch(met, @"^[A-Z]{3}\d{3}$"))
            {
                string pressure = met.Substring(1, 4);

                string inhg = pressure.ToInHg();

                Console.WriteLine("Pressure: " + pressure + " hPa (" + inhg + " inHg)");
            }

            return true;
        }

        private static void ICAOfinder(string met)
        {
            string line = File.ReadLines("airport-codes.csv").SingleOrDefault(o => o.Substring(0, 4) == met.ToUpper());

            string[] details = line.Split(',');

            Console.WriteLine("ICAO: " + details[0]);

            if (details[5] != null)
            {
                Console.WriteLine("IATA: " + details[5]);
            }

            string coordinates = details[3] + "N " + details[2] + "E";

            Console.WriteLine("Airport name: " + details[1]);
            Console.WriteLine("Coordinates: " + coordinates.Replace("\"", string.Empty));
            Console.WriteLine("Country: " + details[4]);
        }
    }
}
