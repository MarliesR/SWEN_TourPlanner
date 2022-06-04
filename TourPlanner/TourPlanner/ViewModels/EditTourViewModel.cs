using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.Library;
using TourPlanner.Logger;
using log4net;

namespace TourPlanner.ViewModels
{
    public class EditTourViewModel : ViewModelBase
    {
        private Window currentWindow;
        private Tour baseTour; 
       
        private RelayCommand editTourCommand;
        public ICommand EditTourCommand => editTourCommand ??= new RelayCommand(EditTour);
        private RelayCommand clearInputCommand;
        public ICommand ClearInputCommand => clearInputCommand ??= new RelayCommand(ResetInput);

        //private static readonly log4net.ILog _logger = LoggingHandler.GetLogger();
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private string tourName;
        private string tourStart;
        private string tourDestination;
        private string tourDescription;
        private string tourTransportType;

        public EditTourViewModel(Window window, Tour tour)
        {
            currentWindow = window;
            baseTour = tour;
            TourName = tour.Name;
            TourStart = tour.Start;
            TourDestination = tour.Destination;
            TourDescription = tour.Description;
            TourTransportType = tour.TransportType;
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

        private void EditTour(object commandParameter)
        {
           
            if (string.IsNullOrEmpty(TourName))
            {
                MessageBox.Show("Name cannot be empty");
                resetUserInput();

            }
            else
            {
                TourHandler handler = new TourHandler();
                handler.ModifyTour(tourName, tourDescription, baseTour.Id);
                currentWindow.DialogResult = true;
                currentWindow.Close();

                _logger.Info("Edited Tour data");
            }
        }


        private void ResetInput(object commandParameter)
        {
            resetUserInput();
        }

        private void resetUserInput()
        {
            TourName = baseTour.Name;
            TourDescription = baseTour.Description;

            _logger.Info("Restet Tour data from edit.");
        }
    }
}
