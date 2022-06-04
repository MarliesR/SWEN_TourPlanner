using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TourPlanner.Library;

namespace TourPlanner.BL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //handler.AddTour("route2", "Berlin", "Vienna", "Auto", "zweite Route");

            TourPlannerFactoryImpl test = new TourPlannerFactoryImpl();
            Tour tour = new Tour("hallo", "Berlin", "Wien", "fastest", 20.00, "help", "06:20:30", @"C:\SWEN_semesterproject_images\oe1aionb.1ob.jpg");
            test.GetTour(3);
            PDFGenerator generator = new PDFGenerator();
            ObservableCollection<TourLog> loglist = null;
            generator.TourReport(tour, loglist);

        }
    }
}

// wird das noch gebraucht? 
// sollte am ende entfernt werden können
// is zum testen da, löschen wir dann raus :)