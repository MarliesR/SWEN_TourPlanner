using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TourPlanner.DAL.Mapquest;
using TourPlanner.DAL.SQL;
using TourPlanner.Library;

namespace TourPlanner.BL
{
    public class TourPlannerFactoryImpl : ITourPlannerFactory
    {
        private TourPlannerDAO tourPlannerDAO = new TourPlannerDAO(); //this objects creates connection with database class and returns results from sql statements

        public bool ValidateStringInput(string input)
        {
            if (!String.IsNullOrEmpty(input))
            {
                bool valid;

                string[] invalidStringsAndCharas = {"INSERT", "DELETE", "SELECT", "UPDATE", "CREATE", "@", "/", "§", "%", "&", "#" };

                valid = ContainsAny(input, invalidStringsAndCharas);

                if (!valid)
                {
                    return true;
                }
            }

            return false;   
        }

        public bool ContainsAny(string toBeTested, params string[] invalidStringsAndCharacters)
        {
            foreach (string parts in invalidStringsAndCharacters)
            {
                if (toBeTested.Contains(parts))
                    return true;
            }

            return false;
        }

        // TOUR LOG VALIDATION TO-DO
        //    cmd.Parameters.AddWithValue("datetime", NpgsqlDbType.Varchar, log.DateTime);
        //    cmd.Parameters.AddWithValue("comment", NpgsqlDbType.Varchar, log.Comment);
        //    cmd.Parameters.AddWithValue("difficulty", NpgsqlDbType.Integer, log.Difficulty);
        //    cmd.Parameters.AddWithValue("totaltime", NpgsqlDbType.Interval, log.TotalTime);
        //    cmd.Parameters.AddWithValue("rating", NpgsqlDbType.Integer, log.Rating);

        public bool ValidAddTourCall(string name, string start, string destination, string description)
        {
            bool validName = ValidateStringInput(name);
            bool validStart = ValidateStringInput(start);
            bool validDestination = ValidateStringInput(destination);
            bool validDescription = ValidateStringInput(description);

            if (validName && validStart && validDestination && validDescription)
            {
                return true;
            }

            return false;
        }

        public bool ValidLogCall(TourLog log)
        {
            bool validComment = ValidateStringInput(log.Comment);
            bool validDateTime = ValidateStringInput(log.DateTime);

            if (validComment && validDateTime)
            {
                return true;
            }

            return false;
        }

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

        public bool GenerateSummarizeReport(Tour tour, ObservableCollection<TourLog> loglist)
        {
            TimeSpan totalTimeAVG = tourPlannerDAO.GetTimeTotalAverage(tour.Id);
            PDFGenerator report = new PDFGenerator();
            return report.SummarizeReport(tour.Name, loglist, totalTimeAVG.ToString());
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

        public IEnumerable<TourLog> ListAllLogs()
        {
            return tourPlannerDAO.GetAllLogsSQL();
        }

        public IEnumerable<Tour> SearchTours(string searchstring)
        {
            IEnumerable<Tour> tours = ListAllTours();
            IEnumerable<TourLog> logs = ListAllLogs();
            List<int> tourIds = new List<int>();
            if (logs != null)
            {
                IEnumerable<TourLog> resultLogs = (IEnumerable<TourLog>)logs.Where(x => x.DateTime.ToLower().Contains(searchstring.ToLower()) || x.Comment.ToLower().Contains(searchstring.ToLower()) || x.Difficulty.ToString().Contains(searchstring.ToLower()) || x.TotalTime.ToString().Contains(searchstring) || x.Rating.ToString().Contains(searchstring));
                foreach (TourLog log in resultLogs)
                {
                    tourIds.Add(log.TourId);
                }
            }
           return (IEnumerable<Tour>)tours.Where(x => tourIds.Any(y => y == x.Id) || x.Name.ToLower().Contains(searchstring.ToLower()) || x.Start.ToLower().Contains(searchstring.ToLower()) || x.Destination.ToLower().Contains(searchstring.ToLower()) || x.TransportType.ToLower().Contains(searchstring.ToLower()) || x.Distance.ToString().ToLower().Contains(searchstring.ToLower()) || x.Duration.ToLower().Contains(searchstring.ToLower()) || x.Description.ToLower().Contains(searchstring.ToLower()));
        }
    }
}
