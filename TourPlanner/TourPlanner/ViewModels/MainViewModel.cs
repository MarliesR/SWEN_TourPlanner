﻿using System.Collections.Generic;
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
        public ObservableCollection<Tour> TourList { get; set; }
        public ObservableCollection<TourLog> LogList { get; set; }
        private Tour currentTour;
        private TourLog currentLog;
        private TourHandler handler = new TourHandler();

        //private static readonly log4net.ILog _logger = LoggingHandler.GetLogger();
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


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

        private RelayCommand genereateReportCommand1;
        public ICommand genereateReportCommand => genereateReportCommand1 ??= new RelayCommand(genereateReport);


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

            _logger.Info("Edit tour with id: 123.");
            // muss ich noch fertig schreiben 
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

                _logger.Info("All tours loaded.");
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

                _logger.Info("Loaded TourLog of current tour.");
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
                TourHandler handler = new TourHandler();
                bool done = handler.DeleteLog(currentLog.Id);
                if (done)
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
                editLogWindow.Show();

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
                TourHandler handler = new TourHandler();
                handler.GenerateTourReport(currentTour, LogList);
                MessageBox.Show("Report Generated");
            }
            else
            {
                MessageBox.Show("Please choose a tour");
            }
            
            
        }
    }
}
