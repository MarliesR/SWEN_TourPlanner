
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TourPlanner.Library;

namespace TourPlanner.BL
{
    public interface ITourPlannerFactory
    {
        IEnumerable<Tour> SearchTours(string tourname);
        public IEnumerable<TourLog> ListAllLogs();
        public List<Tour> ListAllTours();
        public bool ValidAddTourCall(string name, string start, string destination, string description);
        public bool AddTour(string name, string start, string destination, string transporttype, string description);
        public Tour GetTour(int id);
        public void ModifyTour(string tourname, string tourdescription, int id);
        public void ModifyLogEntry(TourLog log);
        public bool DeleteLog(int id);
        public void DeleteTour(int id);
        public int GetTourPopularity(int id);
        public void GenerateTourReport(Tour tour, ObservableCollection<TourLog> loglist);
        public bool GenerateSummarizeReport(Tour tour, ObservableCollection<TourLog> loglist);
        public List<TourLog> ListAllLogsOfSingleTour(int id);
        public bool ValidLogCall(TourLog log);
        public void AddLog(TourLog log);
        public string GetTourChildFriendlyness(Tour tour);
        public void ExportTour(Tour tour);
    }
}
