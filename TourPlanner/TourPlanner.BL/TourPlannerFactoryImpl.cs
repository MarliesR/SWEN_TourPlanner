using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TourPlanner.DAL.Mapquest;
using TourPlanner.DAL.SQL;
using TourPlanner.Library;

namespace TourPlanner.BL
{
    internal class TourPlannerFactoryImpl : ITourPlannerFactory
    {
        private TourPlannerDAO tourPlannerDAO = new TourPlannerDAO(); //this objects creates connection with database class and returns results from sql statements

      
        public void AddLog(TourLog log)
        {
            //return value
            tourPlannerDAO.AddLogSQL(log);
        }

        public bool AddTour(string name, string start, string destination, string transporttype, string description)
        {
            Mapquest mapquest = new Mapquest(start, destination, transporttype);
            mapquest.SaveImage();
            double distance = mapquest.GetDistance();
            string imagePath = mapquest.GetImage();
            string duration = mapquest.GetTime();
            if (distance.Equals(0) || String.IsNullOrEmpty(imagePath))
            {
                return false;
            }
            Tour tour = new Tour(name, start, destination, transporttype, distance, description, duration, imagePath);
            tourPlannerDAO.AddTourSQL(tour);
            return true;
        }

        public bool DeleteLog(int id)
        {
            return tourPlannerDAO.DeleteLogSQL(id);
        }

        public void DeleteTour(int id)
        {
            //return value
            tourPlannerDAO.DeleteTourSQL(id);
            tourPlannerDAO.DeleteAllLogsOfTourSQL(id);
        }

        public void GenerateTourReport(Tour tour, ObservableCollection<TourLog> loglist)
        {
            //return value
            PDFGenerator report = new PDFGenerator();
            report.TourReport(tour, loglist);
        }

        public Tour GetTour(int id)
        {
            return tourPlannerDAO.GetTourSQL(id);
        }

        public string GetTourChildFriendlyness(Tour tour)
        {
            string childFriendly = "Child friendly Route!";
            string notChildFriendly = "Not a Child friendly Route!";
            double difficultyAVG = tourPlannerDAO.GetDifficultyAverage(tour.Id);
            TimeSpan totalTimeAVG = tourPlannerDAO.GetTimeTotalAverage(tour.Id); //default 00:00

            int limitDifficulty = 2;
            TimeSpan limitTime = TimeSpan.Parse("03:00"); //3 stunden ist grenze von der zeit
            int limitDistance = 300; //300km grenze

            if (difficultyAVG > limitDifficulty) { return notChildFriendly; };
            if (totalTimeAVG > limitTime) { return notChildFriendly; };
            if (tour.Distance > limitDistance) { return notChildFriendly; }

            return childFriendly;
        }

        public int GetTourPopularity(int id)
        {
           return tourPlannerDAO.GetTourPoularitySQL(id);
        }

        public List<TourLog> ListAllLogsOfSingleTour(int id)
        {
            return tourPlannerDAO.GetLogsSQL(id);
        }

        public List<Tour> ListAllTours()
        {
            return tourPlannerDAO.GetTours();
        }

        public void ModifyLogEntry(TourLog log)
        {
            //no return value
            tourPlannerDAO.UpdateLogSQL(log);
        }

        public void ModifyTour(string tourname, string tourdescription, int id)
        {
            //no return value
           tourPlannerDAO.UpdateTourSQL(tourname, tourdescription, id);
        }

        public IEnumerable<TourLog> SearchLogs(string tourname, bool caseSensitive = false)
        {
            IEnumerable<Tour> tours = ListAllTours();

            if (caseSensitive)
            {
                return (IEnumerable<TourLog>)tours.Where(x => x.Name.Contains(tourname));
            }
            return (IEnumerable<TourLog>)tours.Where(x => x.Name.ToLower().Contains(tourname.ToLower()));
            throw new NotImplementedException();
        }

        public IEnumerable<Tour> SearchTours(string tourname, bool caseSensitive = false)
        {
            throw new NotImplementedException();
        }
    }
}
