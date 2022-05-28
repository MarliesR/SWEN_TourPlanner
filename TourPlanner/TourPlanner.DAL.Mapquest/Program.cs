using System;
using System.Configuration;

namespace TourPlanner.DAL.Mapquest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("test mapAPI");
            Mapquest mapquest = new Mapquest("Vienna", "Berlin", "fastest");
            //fastest, pedestrian, bicycle
            
        }
    }
}
