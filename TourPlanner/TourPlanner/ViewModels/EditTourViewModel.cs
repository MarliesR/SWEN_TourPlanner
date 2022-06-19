using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.Library;
using log4net;

namespace TourPlanner.ViewModels
{
    public class EditTourViewModel : ViewModelBase
    {

        private ITourPlannerFactory tourPlannerFactory;
        private Window currentWindow;
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private Tour baseTour; 
        private string tourName;
        private string tourStart;
        private string tourDestination;
        private string tourDescription;
        private string tourTransportType;

        private RelayCommand editTourCommand;
        private RelayCommand clearInputCommand;
        public ICommand EditTourCommand => editTourCommand ??= new RelayCommand(EditTour);
        public ICommand ClearInputCommand => clearInputCommand ??= new RelayCommand(ResetInput);




        public EditTourViewModel(Window window, Tour tour)
        {
            this.tourPlannerFactory = TourPlannerFactory.GetInstance();
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
                _logger.Fatal("When editing a tour, name can not be empty.");
                MessageBox.Show("Name cannot be empty.");
                resetUserInput();

            }
            else
            {
                if (tourPlannerFactory.ValidEditTourCall(TourName, TourDescription))
                {
                    this.tourPlannerFactory.ModifyTour(tourName, tourDescription, baseTour.Id);
                    currentWindow.DialogResult = true;
                    currentWindow.Close();

                    MessageBox.Show("Tour saved successfully.");
                    _logger.Info("Tour data hase successfully been edited.");
                }
                else
                {
                    MessageBox.Show("Tour saving failed, check for correct input.");
                    _logger.Warn("Tour data input is invalid.");
                }
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
