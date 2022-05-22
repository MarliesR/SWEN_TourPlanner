using System;
using TourPlanner.Library;

namespace TourPlanner.DAL.SQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Database Layer");
            Database database = new Database();
            TourSql db = new TourSql();
            Tour tour = new Tour("hi", "Vienna", "Berlin", "car", 1000 , "description", "100.00", "url");
            db.AddTourSQL(tour);
        }
    }
}
