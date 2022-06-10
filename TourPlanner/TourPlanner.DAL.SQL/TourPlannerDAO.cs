using System;
using System.Collections.Generic;
using TourPlanner.Library;

namespace TourPlanner.DAL.SQL
{
    public class TourPlannerDAO
    {
        private IDataAccesss dataAccesss;

        public TourPlannerDAO()
        {
            //check which datasource to use 
            dataAccesss = new Database(); 
        }
        public List<Tour> GetTours()
        {
            return dataAccesss.GetToursSQL();
        }

        public TimeSpan GetTimeTotalAverage(int id)
        {
            return dataAccesss.GetTimeTotalAverage(id);
        }

        public double GetDifficultyAverage(int id)
        {
            return dataAccesss.GetDifficultyAverage(id);
        }
        public int GetTourPoularitySQL(int id)
        {
            return dataAccesss.GetTourPoularitySQL(id);
        }
        public void UpdateTourSQL(string tourname, string tourdescription, int id)
        {
            //return value?
            dataAccesss.UpdateTourSQL(tourname, tourdescription, id);
        }
        public void DeleteTourSQL(int TourId)
        {
            //return
            dataAccesss.DeleteTourSQL(TourId);
        }
        public Tour GetTourSQL(int TourId)
        {
            return dataAccesss.GetTourSQL(TourId);
        }
        public void AddTourSQL(Tour TourData)
        {
            //return
            dataAccesss.AddTourSQL(TourData);
        }
        public void UpdateLogSQL(TourLog log)
        {
            //return
            dataAccesss.UpdateLogSQL(log);
        }
        public void DeleteAllLogsOfTourSQL(int tourid)
        {
            //return
            dataAccesss.DeleteAllLogsOfTourSQL(tourid);
        }
        public bool DeleteLogSQL(int logId)
        {
            return dataAccesss.DeleteLogSQL(logId);
        }
        public void AddLogSQL(TourLog log)
        {
            //return
            dataAccesss.AddLogSQL(log);
        }
        public List<TourLog> GetLogsSQL(int tourId)
        {
            return dataAccesss.GetLogsSQL(tourId);
        }

        public List<TourLog> GetAllLogsSQL()
        {
            return dataAccesss.GetAllLogsSQL();
        }

    }
}
