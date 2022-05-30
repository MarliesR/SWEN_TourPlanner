using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.Views;
using TourPlanner.Library;
using TourPlanner.BL;
using TourPlanner.Logger;
using System.Windows;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Tour> TourList { get; set; }
        public ObservableCollection<TourLog> LogList { get; set; }
        private Tour currentTour;
        private TourLog currentLog;
        private TourHandler handler = new TourHandler();

        private static readonly log4net.ILog _logger = LogHelper.GetLogger();

        private RelayCommand refreshToursCommand1;
        public ICommand refreshToursCommand => refreshToursCommand1 ??= new RelayCommand(RefreshTours);
        private RelayCommand editTourPageCommand1;
        public ICommand editTourPageCommand => editTourPageCommand1 ??= new RelayCommand(EditTourWindow);
        private RelayCommand showTourWindowCommand1;
        public ICommand showTourWindowCommand => showTourWindowCommand1 ??= new RelayCommand(ShowTourWindow);
        private RelayCommand addLogPageCommand1;
        public ICommand addLogPageCommand => addLogPageCommand1 ??= new RelayCommand(ShowNewLogWindow);
        private RelayCommand deleteTourCommand;
        public ICommand DeleteTourCommand => deleteTourCommand ??= new RelayCommand(DeleteTour);
        private RelayCommand deleteLogCommand;
        public ICommand DeleteLogCommand => deleteLogCommand ??= new RelayCommand(DeleteLog);

        private RelayCommand editLogCommand;
        public ICommand EditLogCommand => editLogCommand ??= new RelayCommand(EditLog);


        public MainViewModel()
        {
            handler.InitialiseDB();
            SelectedViewModel = new ShowTourViewModel(currentTour);
            TourList = new ObservableCollection<Tour>();
            LogList = new ObservableCollection<TourLog>();
            
            LoadAllTours();
        }

        public object selectedViewModel; //DATA binding mit dem MainWindow
        public object SelectedViewModel  //schaut ob sich die value des viewmodels ändert
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
            if(currentTour != null)
            {
                EditTourView newTourWindow = new EditTourView(currentTour);
                newTourWindow.Show();
            }
            else
            {
                MessageBox.Show("Please choose a tour");
            }


        }
        private void LoadAllTours()
        {
            TourList.Clear();
            TourHandler handler = new TourHandler();
            List <Tour> tourlist = handler.ListAllTours();
            if (tourlist != null)
            {
                foreach (var tour in tourlist)
                {
                    TourList.Add(tour);
                }
            }
        }

        private void LoadLogsCurrentTour()
        {
            LogList.Clear();
            TourHandler handler = new TourHandler();
            List <TourLog> loglist = handler.ListAllLogsOfSingleTour(CurrentTour.Id);
            if (loglist != null)
            {
                foreach (var log in loglist)
                {
                    LogList.Add(log);
                }
            }
        }


        private void ShowTourWindow(object commandParameter)
        {
            AddTourView newTourWindow = new AddTourView();
            newTourWindow.Show();
            currentTour = null;
        }

        private void ShowNewLogWindow(object commandParameter)
        {
            if (currentTour != null)
            {
                AddLogView newLogWindow = new AddLogView(currentTour);
                newLogWindow.Show();
            }
            else
            {
                MessageBox.Show("Please choose a tour");
            }
        }


        private void RefreshTours(object commandParameter)
        {
            LoadAllTours();
        }

        private void DeleteTour(object commandParameter)
        {
            if(currentTour != null)
            {
                TourHandler handler = new TourHandler();
                handler.DeleteTour(currentTour.Id);
                currentTour = null;
                SelectedViewModel = new ShowTourViewModel(currentTour);
                LoadAllTours();
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
                TourHandler handler = new TourHandler();
                bool done = handler.DeleteLog(currentLog.Id);
                if (done)
                {
                    LoadLogsCurrentTour();
                }
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
                editLogWindow.Show();
            }
            else
            {
                MessageBox.Show("Please choose a log entry");
            }
        }
    }
}
