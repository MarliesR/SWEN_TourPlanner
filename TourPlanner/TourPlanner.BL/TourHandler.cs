using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Library;
using TourPlanner.DAL.Mapquest;
using TourPlanner.DAL.SQL;

namespace TourPlanner.BL
{
    public class TourHandler
    {
        public void InitialiseDB()
        {
            Database db = new Database();
        }
        public void AddTour(string name, string start, string destination, string transporttype, string description )
        {
           
            Mapquest mapquest = new Mapquest(start, destination,transporttype);
            int distance = mapquest.GetDistance();
            string imagePath = mapquest.GetImage();
            string duration = mapquest.GetTime();
            TourSql db = new TourSql();
            //string name, string start, string destination, string transporttype, float distance, string description, string duration, string image)
            Tour tour = new Tour(name, start, destination, transporttype, distance, description, duration, imagePath);
            db.AddTourSQL(tour);
            Console.WriteLine("finished");
         

        }

        public Tour GetTour( int id)
        {
            TourSql db = new TourSql();
            Tour tour = db.GetTourSQL(id);
            return tour;
        }

        public void ModifyTour()
        {

        }

        public void DeleteTour()
        {

        }

        public void GenerateTourReport(int id)
        {
            PDFGenerator report = new PDFGenerator();
            TourSql db = new TourSql();
            Tour tour = db.GetTourSQL(id);
            //get tour logs from single tour
            //List <TourLog>
            //report.TourReport(tour,);
        }

        public List<Tour> ListAllTours()
        {
            TourSql db = new TourSql();
            List<Tour> tourlist = db.GetToursSQL();
            return tourlist;
        }

        public List<TourLog> ListAllLogsOfSingleTour(int id)
        {
            LogSql db = new LogSql();
            List<TourLog> loglist = db.GetLogsSQL(id);
            return loglist;
        }

        public void AddLog(TourLog log)
        {
            LogSql db = new LogSql();
            db.AddLogSQL(log);
            Console.WriteLine("log added");

        }

    }
}
