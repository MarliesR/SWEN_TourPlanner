using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.ViewModels;
using TourPlanner.Views;
using TourPlanner.Library;
using TourPlanner.BL;
using System.Windows;
using TourPlanner.Logger;
using log4net;


namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ITourPlannerFactory tourPlannerFactory; 
        //private static readonly log4net.ILog _logger = LoggingHandler.GetLogger();
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ObservableCollection<Tour> TourList { get; set; }
        public ObservableCollection<TourLog> LogList { get; set; }
        private Tour currentTour;
        private TourLog currentLog;
        public object selectedViewModel; 


        private RelayCommand editTourPageCommand1;
        private RelayCommand showTourWindowCommand1;
        private RelayCommand addLogPageCommand1;
        private RelayCommand deleteTourCommand;
        private RelayCommand deleteLogCommand;
        private RelayCommand editLogCommand;
        private RelayCommand genereateReportCommand1;
        private RelayCommand genereateSummarizeReportCommand1;
        public ICommand editTourPageCommand => editTourPageCommand1 ??= new RelayCommand(EditTourWindow);
        public ICommand showTourWindowCommand => showTourWindowCommand1 ??= new RelayCommand(ShowTourWindow);
        public ICommand addLogPageCommand => addLogPageCommand1 ??= new RelayCommand(ShowNewLogWindow);
        public ICommand DeleteTourCommand => deleteTourCommand ??= new RelayCommand(DeleteTour);
        public ICommand DeleteLogCommand => deleteLogCommand ??= new RelayCommand(DeleteLog);
        public ICommand EditLogCommand => editLogCommand ??= new RelayCommand(EditLog);
        public ICommand genereateReportCommand => genereateReportCommand1 ??= new RelayCommand(genereateReport);
        public ICommand genereateSummarizeReportCommand => genereateSummarizeReportCommand1 ??= new RelayCommand(genereateSummarizeReport);
        public MainViewModel()
        {
            this.tourPlannerFactory = TourPlannerFactory.GetInstance();
            TourList = new ObservableCollection<Tour>();
            LogList = new ObservableCollection<TourLog>();
          
            SelectedViewModel = new ShowTourViewModel(currentTour);
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


        private void EditTourWindow(object commandParameter)
        {
            if (currentTour != null)
            {
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

            _logger.Info("Edit tour with id: 123.");
            // muss ich noch fertig schreiben 
        }

        
        private void LoadAllTours()
        {
            TourList.Clear();
            List<Tour> tourlist = this.tourPlannerFactory.ListAllTours();
            if (tourlist != null)
            {
                foreach (var tour in tourlist)
                {
                    TourList.Add(tour);
                }

                _logger.Info("All tours loaded.");
            }
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
                    MessageBox.Show("Log saved!");
                }
            }
            else
            {
                MessageBox.Show("Please choose a tour");
            }
        }


        private void DeleteTour(object commandParameter)
        {
            if(currentTour != null)
            {
                this.tourPlannerFactory.DeleteTour(currentTour.Id);
                currentTour = null;
                SelectedViewModel = new ShowTourViewModel(currentTour);
                LoadAllTours();

                _logger.Info("Delete tour with id: 123.");
                // todo
            }
            else
            {
                MessageBox.Show("Please choose a tour");
            }
        }


        private void DeleteLog(object commandParameter)
        {
            if(currentLog != null)
            {
                if (this.tourPlannerFactory.DeleteLog(currentLog.Id))
                {
                    LoadLogsCurrentTour();
                }

                _logger.Info("Deleted TourLog.");
            }
            else
            {
                MessageBox.Show("Please choose a log entry");
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
                MessageBox.Show("Please choose a log entry");
            }
        }

        private void genereateReport(object commandParameter)
        {
            if (currentTour != null)
            {
                this.tourPlannerFactory.GenerateTourReport(currentTour, LogList);
                MessageBox.Show("Report Generated");
            }
            else
            {
                MessageBox.Show("Please choose a tour");
            }
            
        }

        

        private void genereateSummarizeReport(object commandParameter)
        {
            if (currentTour != null)
            {
                this.tourPlannerFactory.GenerateSummarizeReport(currentTour, LogList);
                MessageBox.Show("Sumamrize Report Generated");
            }
            else
            {
                MessageBox.Show("Please choose a tour");
            }
        }
    }
}
