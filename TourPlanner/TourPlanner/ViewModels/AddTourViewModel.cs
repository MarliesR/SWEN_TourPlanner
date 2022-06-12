using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using log4net;

namespace TourPlanner.ViewModels
{
    public class AddTourViewModel : ViewModelBase
    {
        
        private ITourPlannerFactory tourPlannerFactory;
        private Window currentWindow;
        //private static readonly log4net.ILog _logger = LoggingHandler.GetLogger();
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string tourName;
        private string tourStart;
        private string tourDestination;
        private string tourDescription;
        private string tourTransportType = "fastest";
        public ObservableCollection<string> RouteTypes { get; set; }

        private RelayCommand saveTourCommand;
        private RelayCommand clearInputCommand;
        public ICommand SaveTourCommand => saveTourCommand ??= new RelayCommand(SaveTour);
        public ICommand ClearInputCommand => clearInputCommand ??= new RelayCommand(ClearInput);


        public AddTourViewModel(Window window)
        {
            this.tourPlannerFactory = TourPlannerFactory.GetInstance();
            currentWindow = window;
            RouteTypes = new ObservableCollection<string>();
            InitialiseRouteTypes();

        }

        private void InitialiseRouteTypes()
        {
            RouteTypes.Clear();
            RouteTypes.Add("fastest");
            RouteTypes.Add("shortest");
            RouteTypes.Add("pedestrian");
            RouteTypes.Add("bicycle");
        }

    

        public String TourName //das ist der name der im binding angegeben ist im view von add tour
        {
            get => tourName;
            set
            {
                if (tourName != value)
                {
                    tourName = value;
                    RaisePropertyChangedEvent(nameof(TourName));
                }
            }
        }


        public String TourStart
        {
            get => tourStart;
            set
            {
                if (tourStart != value)
                {
                    tourStart = value;
                    RaisePropertyChangedEvent(nameof(TourStart));
                }
            }
        }


        public String TourDescription
        {
            get => tourDescription;
            set
            {
                if (tourDescription != value)
                {
                    tourDescription = value;
                    RaisePropertyChangedEvent(nameof(TourDescription));
                }
            }
        }

        public String TourDestination
        {
            get => tourDestination;
            set
            {
                if (tourDestination != value)
                {
                    tourDestination = value;
                    RaisePropertyChangedEvent(nameof(TourDestination));
                }
            }
        }


        public String TourTransportType
        {
            get => tourTransportType;
            set
            {
                if (tourTransportType != value)
                {
                    tourTransportType = value;
                    RaisePropertyChangedEvent(nameof(TourTransportType));
                }
            }
        }


        private void SaveTour(object commandParameter)
        {
            if(tourPlannerFactory.ValidAddTourCall(tourName, tourStart, tourDestination, tourDescription))
            {
                if (tourPlannerFactory.AddTour(tourName, tourStart, tourDestination, tourTransportType, tourDescription))
                {
                    currentWindow.DialogResult = true;
                    currentWindow.Close();
                    _logger.Info("Added new Tour.");
                }
                else
                {
                    MessageBox.Show("Tour saving failed, check for correct names of location, (pedestrian routes can be max. 200km long)");
                    _logger.Warn("Adding new tour failed.");
                }
            }
            else
            {
                MessageBox.Show("Tour saving failed, check for correct input");
                _logger.Warn("Adding new tour failed.");
            }

        }


        private void ClearInput(object commandParameter)
        {
            TourName = string.Empty;
            TourStart = string.Empty;
            TourDestination = string.Empty;
            TourDescription = string.Empty;
            _logger.Info("Clear Input.");
        }

    }

}
