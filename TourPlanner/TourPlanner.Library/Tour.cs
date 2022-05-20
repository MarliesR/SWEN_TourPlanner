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
        public string Description { get; set; }

        //public int Duration { get; set; }
        //public int Image { get; set; }
    }
}
