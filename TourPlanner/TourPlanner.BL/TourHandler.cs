using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Library;
using TourPlanner.DAL.Mapquest;
using TourPlanner.DAL.SQL;
using System.Collections.ObjectModel;

namespace TourPlanner.BL
{
    public class TourHandler
    {
        public void InitialiseDB()
        {
            Database db = new Database();
        }
        public bool AddTour(string name, string start, string destination, string transporttype, string description )
        {
           
            Mapquest mapquest = new Mapquest(start, destination,transporttype);
            mapquest.SaveImage();
            double distance = mapquest.GetDistance();
            string imagePath = mapquest.GetImage();
            string duration = mapquest.GetTime();
            if (distance.Equals(0)|| String.IsNullOrEmpty(imagePath))
            {
                return false;
            }
            TourSql db = new TourSql();
            Tour tour = new Tour(name, start, destination, transporttype, distance, description, duration, imagePath);
            db.AddTourSQL(tour);
            return true;
        }

        public Tour GetTour( int id)
        {
            TourSql db = new TourSql();
            Tour tour = db.GetTourSQL(id);
            return tour;
        }

        public void ModifyTour(string tourname, string tourdescription, int id)
        {
            TourSql db = new TourSql();
            db.UpdateTourSQL(tourname,tourdescription,id);
        }

        public void ModifyLogEntry(TourLog log)
        {
            LogSql db = new LogSql();
            db.UpdateLogSQL(log);
        }
        public bool DeleteLog(int id)
        {
            LogSql db = new LogSql();
            return db.DeleteLogSQL(id);
        }

        public void DeleteTour(int id)
        {
            TourSql db = new TourSql();
            LogSql dbs = new LogSql();
            db.DeleteTourSQL(id);
            dbs.DeleteAllLogsOfTourSQL(id);
        }

        public int GetTourPopularity(int id)
        {
            TourSql db = new TourSql();
            int popularity = db.GetTourPoularitySQL(id);
            return popularity;
        }

        public void GenerateTourReport(Tour tour, ObservableCollection<TourLog> loglist)
        {
            PDFGenerator report = new PDFGenerator();
            report.TourReport(tour, loglist);

            
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

        public string GetTourChildFriendlyness(Tour tour)
        {
            string childFriendly = "Child friendly Route!";
            string notChildFriendly = "Not a Child friendly Route!";

            TourSql db = new TourSql();
            double difficultyAVG = db.GetDifficultyAverage(tour.Id);
            TimeSpan totalTimeAVG = db.GetTimeTotalAverage(tour.Id); //default 00:00


            int limitDifficulty = 2;
            TimeSpan limitTime = TimeSpan.Parse("03:00"); //3 stunden ist grenze von der zeit
            int limitDistance = 300; //300km grenze

            if(difficultyAVG > limitDifficulty) { return notChildFriendly; };
            if(totalTimeAVG > limitTime) { return notChildFriendly; };
            if(tour.Distance > limitDistance) { return notChildFriendly; }

            return childFriendly;
        }

    }
}
