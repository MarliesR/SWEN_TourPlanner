using System;

namespace TourPlanner.Library
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Start { get; set; }
        public string Destination { get; set; }
        public string TransportType { get; set; }
        public double Distance { get; set; } //von MApquest
        public string Description { get; set; }
        public string Duration { get; set; } //von Mapquest
        public string Image { get; set; } //von Mapquest

    }
}
