using System;

namespace TourPlanner.BL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TourHandler handler = new TourHandler();
            handler.AddTour("route1", "Berlin", "Vienna", "Auto", "erste Route");
        }
    }
}
