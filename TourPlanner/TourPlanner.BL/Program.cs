using System;
using TourPlanner.Library;

namespace TourPlanner.BL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TourHandler handler = new TourHandler();
            //handler.AddTour("route2", "Berlin", "Vienna", "Auto", "zweite Route");

            Tour tour = new Tour("hallo", "Berlin", "Wien", "fastest", 20.00, "help", "06:20:30", @"C:\SWEN_semesterproject_images\oe1aionb.1ob.jpg");
            PDFGenerator generator = new PDFGenerator();
            //generator.TourReport(tour);

        }
    }
}

// wird das noch gebraucht? 
// sollte am ende entfernt werden können