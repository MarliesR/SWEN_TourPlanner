using System;
using System.Configuration;

namespace TourPlanner.DAL.Mapquest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("test mapAPI");
            //Mapquest mapquest = new Mapquest("Vienna", "Berlin", "fastest");
            //fastest, pedestrian, bicycle
            Mapquest mapquest = new Mapquest("Vienna", "Paris", "shortest");
            // PROBLEM !!!
            // constuctor hat GetImagePath() drinnen
            // muss file bzw filepath hinzufügen 

            double distance = mapquest.GetDistance();
            Console.WriteLine(distance);

        }
    }
}
