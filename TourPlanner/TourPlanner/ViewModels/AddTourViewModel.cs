using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.ViewModels;

namespace TourPlanner.ViewModels
{
    public class AddTourViewModel : ViewModelBase
    {

        private Window currentWindow;
        private RelayCommand saveTourCommand;
        public ICommand SaveTourCommand => saveTourCommand ??= new RelayCommand(SaveTour);
  

        private string tourName;
        private string tourStart;
        private string tourDestination;
        private string tourDescription;
        private string tourTransportType = "fastest";
     
        public ObservableCollection<string> RouteTypes { get; set; }

        public AddTourViewModel(Window window)
        {
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
            TourHandler handler = new TourHandler();
            handler.AddTour(tourName, tourStart, tourDestination, tourTransportType, tourDescription);
            currentWindow.Close();
        }

        private RelayCommand clearInputCommand;
        public ICommand ClearInputCommand => clearInputCommand ??= new RelayCommand(ClearInput);

        private void ClearInput(object commandParameter)
        {
            TourName = string.Empty;
            TourStart = string.Empty;
            TourDestination = string.Empty;
            TourDescription = string.Empty;
        }




    }

}
