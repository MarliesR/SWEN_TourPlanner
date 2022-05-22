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
        public int Distance { get; set; } //von MApquest
        public string Description { get; set; }
        public string Duration { get; set; } //von Mapquest
        public string Image { get; set; } //von Mapquest

        public Tour(string name, string start, string destination, string transporttype, int distance, string description, string duration, string image)
        {
            Name = name;
            Start = start;
            Destination = destination;
            TransportType = transporttype;
            Distance = distance;
            Description = description;
            Duration = duration;
            Image = image;
        }
    }
}
