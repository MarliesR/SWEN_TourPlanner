using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Library;

namespace TourPlanner.DAL.SQL
{
    interface IDataAccesss
    {
        public List<Tour> GetToursSQL();
        public List<TourLog> GetAllLogsSQL();
        public TimeSpan GetTimeTotalAverage(int id);
        public double GetDifficultyAverage(int id);
        public int GetTourPoularitySQL(int id);
        public void UpdateTourSQL(string tourname, string tourdescription, int id);
        public void DeleteTourSQL(int TourId);
        public Tour GetTourSQL(int TourId);
        public void AddTourSQL(Tour TourData);
        public void UpdateLogSQL(TourLog log);
        public void DeleteAllLogsOfTourSQL(int tourid);
        public bool DeleteLogSQL(int logId);
        public void AddLogSQL(TourLog log);
        public List<TourLog> GetLogsSQL(int tourId);

    }
}
