using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.ViewModels;
using TourPlanner.Views;
using TourPlanner.Library;
using TourPlanner.BL;
using System.Windows;
using log4net;
using System.Collections;
using System.Linq;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ITourPlannerFactory tourPlannerFactory; 
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ObservableCollection<Tour> TourList { get; set; }
        public ObservableCollection<Tour> FavouriteTours { get; set; }
        public ObservableCollection<TourLog> LogList { get; set; }
        private Tour currentTour;
        private TourLog currentLog;
        private string searchText = "Search..";
        public object selectedViewModel;
        private string filePath;
        private string favouriteIcon;
        private string pathNotFavourite = "/TourPlanner;component/Utilities/favourite.png";
        private string pathFavourite = "/TourPlanner;component/Utilities/alreadyFavourite.png";
        

        private RelayCommand editTourPageCommand1;
        private RelayCommand showTourWindowCommand1;
        private RelayCommand addLogPageCommand1;
        private RelayCommand deleteTourCommand;
        private RelayCommand deleteLogCommand;
        private RelayCommand editLogCommand;
        private RelayCommand genereateReportCommand1;
        private RelayCommand genereateSummarizeReportCommand1;
        private RelayCommand searchCommand1;
        private RelayCommand clearCommand1;
        private RelayCommand exportTourCommand1;
        private RelayCommand importTourCommand1;
        private RelayCommand addFavouriteTour1;

        public ICommand addFavouriteTour => addFavouriteTour1 ??= new RelayCommand(PerformaddFavouriteTour);
        public ICommand editTourPageCommand => editTourPageCommand1 ??= new RelayCommand(EditTourWindow);
        public ICommand showTourWindowCommand => showTourWindowCommand1 ??= new RelayCommand(ShowTourWindow);
        public ICommand addLogPageCommand => addLogPageCommand1 ??= new RelayCommand(ShowNewLogWindow);
        public ICommand DeleteTourCommand => deleteTourCommand ??= new RelayCommand(DeleteTour);
        public ICommand DeleteLogCommand => deleteLogCommand ??= new RelayCommand(DeleteLog);
        public ICommand EditLogCommand => editLogCommand ??= new RelayCommand(EditLog);
        public ICommand genereateReportCommand => genereateReportCommand1 ??= new RelayCommand(genereateReport);
        public ICommand genereateSummarizeReportCommand => genereateSummarizeReportCommand1 ??= new RelayCommand(genereateSummarizeReport);
        public ICommand searchCommand => searchCommand1 ??= new RelayCommand(search);
        public ICommand clearCommand => clearCommand1 ??= new RelayCommand(clear);
        public ICommand exportTourCommand => exportTourCommand1 ??= new RelayCommand(exportTour);
        public ICommand importTourCommand => importTourCommand1 ??= new RelayCommand(importTour);

        public MainViewModel()
        {
            this.tourPlannerFactory = TourPlannerFactory.GetInstance();
            TourList = new ObservableCollection<Tour>();
            LogList = new ObservableCollection<TourLog>();
            FavouriteTours = new ObservableCollection<Tour>();
            SelectedViewModel = new ShowTourViewModel(currentTour);
            favouriteIcon = pathNotFavourite;
            LoadAllTours();
        }

        public object SelectedViewModel  
        {
            get => selectedViewModel;
            set
            {
                if (selectedViewModel != value)
                {
                    selectedViewModel = value;
                    RaisePropertyChangedEvent(nameof(SelectedViewModel));
                }
            }
        }
        

        public Tour CurrentTour
        {
            get => currentTour;
            set
            {
                if (currentTour != value)
                {
                    currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));
                    if (currentTour != null)
                    {
                        if (UpdateFavourite(currentTour.Id))
                        {
                            FavouriteIcon = pathFavourite;
                        }
                        else
                        {
                            FavouriteIcon = pathNotFavourite; 
                        }

                        SelectedViewModel = new ShowTourViewModel(currentTour);
                        LoadLogsCurrentTour();

                    }
                   
                }
            }
        }

        public TourLog CurrentLog
        {
            get => currentLog;
            set
            {
                if (currentLog != value)
                {
                    currentLog = value;
                    RaisePropertyChangedEvent(nameof(CurrentLog));

                }
            }
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    RaisePropertyChangedEvent(nameof(SearchText));
                }
            }
        }

        public string FavouriteIcon
        {
            get => favouriteIcon;
            set
            {
                if (favouriteIcon != value)
                {
                    favouriteIcon = value;
                    RaisePropertyChangedEvent(nameof(FavouriteIcon));
                }
            }
        }

        private void EditTourWindow(object commandParameter)
        {
            if (currentTour != null)
            {
                _logger.Info($"Edit tour with id: {currentTour.Id}.");
                EditTourView newTourWindow = new EditTourView(currentTour);
                bool? dialogResult = newTourWindow.ShowDialog();
                if (dialogResult == true)
                {
                    LoadAllTours();
                }
            }
            else
            {
                MessageBox.Show("Please choose a tour");
            }

           
        }

        
        private void LoadAllTours()
        {
            TourList.Clear();
            List<Tour> tourlist = this.tourPlannerFactory.ListAllTours();
            //helper list for favourites
            List<Tour> favourites = new(); 
            favourites.Clear();
            if (tourlist != null)
            {
                foreach (var tour in tourlist)
                {
                    TourList.Add(tour);
                    //check if this tour from full tourlist is also in favourites list
                    if (UpdateFavourite(tour.Id))
                    {
                        //add to helper list
                        favourites.Add(tour);
                    }
                }
                //clear current favourites observableCollection which are displayed
                FavouriteTours.Clear();
                //add replace favourites with updated helper list 
                foreach(Tour favouriteTour in favourites)
                {
                    FavouriteTours.Add(favouriteTour);
                }
                _logger.Info("All tours loaded.");
            }
        }

        private bool UpdateFavourite(int tourid)
        {
            if(FavouriteTours.Any(x => x.Id == tourid))
            {
                return true;
            }
            return false;
        }

        private void LoadLogsCurrentTour()
        {
            LogList.Clear();
            List <TourLog> loglist = this.tourPlannerFactory.ListAllLogsOfSingleTour(CurrentTour.Id);
            if (loglist != null)
            {
                foreach (var log in loglist)
                {
                    LogList.Add(log);
                }

                _logger.Info("Loaded TourLog of current tour.");
            }
        }


        private void ShowTourWindow(object commandParameter)
        {
            AddTourView newTourWindow = new AddTourView();
            CurrentTour = null;

            bool? dialogResult = newTourWindow.ShowDialog();
            if(dialogResult == true)
            {
                LoadAllTours();
                _logger.Info("Tour has been saved.");
                MessageBox.Show("Tour saved!");
            }

        }

        private void ShowNewLogWindow(object commandParameter)
        {
            if (currentTour != null)
            {
                AddLogView newLogWindow = new AddLogView(currentTour);
                bool? dialogResult = newLogWindow.ShowDialog();
                if(dialogResult == true)
                {
                    LoadLogsCurrentTour();
                    _logger.Info($"Log to tour id {currentTour.Id} has been saved.");
                }
            }
            else
            {
                _logger.Warn("No tour choosen for tour log.");
                MessageBox.Show("Please choose a tour.");
            }
        }


        private void DeleteTour(object commandParameter)
        {
            if(currentTour != null)
            {
                _logger.Info($"Delete tour with id: {currentTour.Id}.");
                this.tourPlannerFactory.DeleteTour(currentTour.Id);
                if (UpdateFavourite(currentTour.Id))
                {
                    FavouriteTours.Remove(currentTour);
                }
                currentTour = null;
                SelectedViewModel = new ShowTourViewModel(currentTour);
                LoadAllTours();
            }
            else
            {
                _logger.Warn("No tour choosen delete.");
                MessageBox.Show("Please choose a tour.");
            }
        }


        private void DeleteLog(object commandParameter)
        {
            if(currentLog != null)
            {
                _logger.Info($"Deleted TourLog {currentLog.Id}.");
                if (this.tourPlannerFactory.DeleteLog(currentLog.Id))
                {
                    LoadLogsCurrentTour();
                }
            }
            else
            {
                _logger.Warn("No tour log choosen to delete.");
                MessageBox.Show("Please choose a log entry.");
            }

        }


        private void EditLog(object commandParameter)
        {
            if (currentTour != null && currentLog != null)
            {
                EditLogView editLogWindow = new EditLogView(currentLog, currentTour.Name);
                bool? dialogResult = editLogWindow.ShowDialog();
                if (dialogResult == true)
                {
                    LoadLogsCurrentTour();
                }

                _logger.Info("Edit current TourLog.");
            }
            else
            {
                _logger.Warn("No tour has been choosen to edit.");
                MessageBox.Show("Please choose a log entry.");
            }
        }

        private void genereateReport(object commandParameter)
        {
            if (currentTour != null)
            {
                this.tourPlannerFactory.GenerateTourReport(currentTour, LogList);
                _logger.Info($"Report has been successfully created from tour id {currentTour.Id}.");
                MessageBox.Show("Report Generated");
            }
            else
            {
                _logger.Warn("No tour has been choosen for PDF generation.");
                MessageBox.Show("Please choose a tour.");
            }
            
        }


        private void search(object commandParameter)
        {
            if (!string.IsNullOrEmpty(searchText) || !string.IsNullOrWhiteSpace(searchText))
            {
                IEnumerable foundTours = this.tourPlannerFactory.SearchTours(searchText);
                TourList.Clear();
                foreach (Tour tour in foundTours)
                {
                    TourList.Add(tour);
                }
                _logger.Info($"Searching for: {searchText}.");
            }
            else
            {
                LoadAllTours();
                SearchText = "";
            }
            
        }

        

        private void clear(object commandParameter)
        {
            LoadAllTours();
            SearchText = "Search..";
        }

        

        private void genereateSummarizeReport(object commandParameter)
        {
            if (currentTour != null)
            {
                this.tourPlannerFactory.GenerateSummarizeReport(currentTour, LogList);
                _logger.Info($"Report has been successfully created from tour id {currentTour.Id}.");
                MessageBox.Show("Sumamrize Report Generated");
            }
            else
            {
                _logger.Warn("No tour has been choosen for Report generation.");
                MessageBox.Show("Please choose a tour");
            }
        }

        private void exportTour(object commandParameter)
        {
            if(currentTour != null)
            {
                this.tourPlannerFactory.ExportTour(currentTour);
                _logger.Warn($"Tour {currentTour.Id} has been succesfully exported.");
                MessageBox.Show("Tour has been succesfully exported!");
            }
            else
            {
                _logger.Warn("No tour has been choosen to export.");
                MessageBox.Show("Please choose a tour");
            }
            
        }


        private void importTour(object commandParameter)
        {
            ImportTourView importTourWindow = new ImportTourView();
            bool? dialogResult = importTourWindow.ShowDialog();
            if (dialogResult == true)
            {
                _logger.Warn("Tour has been succesfully imported.");
                MessageBox.Show("Tour has been succesfully imported!");
                LoadAllTours();
            }
            else
            {
                _logger.Warn("No tour has been choosen to import.");
                MessageBox.Show("Please enter a path to a TourFile");
            }
        }

      

        private void PerformaddFavouriteTour(object commandParameter)
        {
            if (currentTour != null)
            {
                var alreadyFavourite = FavouriteTours.Where(x => x.Id == CurrentTour.Id).ToList();
                if (alreadyFavourite.Count() == 1)
                {
                    FavouriteTours.Remove(currentTour);
                    FavouriteIcon = pathNotFavourite;
                }
                else
                {
                    FavouriteTours.Add(currentTour);
                    FavouriteIcon = pathFavourite;
                }
                
            }
        }
    }
}
