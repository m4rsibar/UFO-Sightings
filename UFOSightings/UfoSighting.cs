using System;
using System.Collections.Generic;
using System.Text;

namespace UFOSightings
{
    public class UfoSighting
    {
        public DateTime SightingDate { get; set; }
        public string Shape { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Details { get; set; }


        public override string ToString()
        {
            StringBuilder sightingFormatted = new StringBuilder();
            sightingFormatted.Append($"Date: {SightingDate} || City: {City} || State: {State} Country: {Country}");
            sightingFormatted.Append($"\n");
            sightingFormatted.Append($"\nDetails: {Details}");
            sightingFormatted.Append("\n__________________________________________________________________________________");
            return sightingFormatted.ToString();
        }



    }


}
