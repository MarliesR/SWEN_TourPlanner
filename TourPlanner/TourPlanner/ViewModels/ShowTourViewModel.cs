using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.ViewModels;
using TourPlanner.Library;

namespace TourPlanner.ViewModels
{
    public class ShowTourViewModel : ViewModelBase
    {
        private string tourName;
        private string tourStart;
        private string tourDestination;
        private string tourDescription;
        private string tourTransportType;
        private string tourDistance;
        private string tourDuration;
        private string tourImagePath;


        public ShowTourViewModel(Tour tour)
        {
            if(tour != null)
            {
                tourName = tour.Name;
                tourStart = tour.Start;
                tourDestination = tour.Destination;
                tourDescription = tour.Description;
                tourDistance = tour.Distance.ToString();
                tourDuration = tour.Duration.ToString();
                tourTransportType = tour.TransportType;
                tourImagePath = tour.Image;

            }
        }

        public String TourName 
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

        public String TourDistance 
        {
            get => tourDistance;
            set
            {
                if (tourDistance != value)
                {
                    tourDistance = value;
                    RaisePropertyChangedEvent(nameof(TourDistance));
                }
            }
        }

        public String TourDuration 
        {
            get => tourDuration;
            set
            {
                if (tourDuration != value)
                {
                    tourDuration = value;
                    RaisePropertyChangedEvent(nameof(TourDuration));
                }
            }
        }

        public String TourImage
        {
            get => tourImagePath;
            set
            {
                if (tourImagePath != value)
                {
                    tourImagePath = value;
                    RaisePropertyChangedEvent(nameof(TourImage));
                }
            }
        }


        //private RelayCommand editTourCommand;
        //public ICommand EditTourCommand => editTourCommand ??= new RelayCommand(EditTour);

        //private void EditTour(object commandParameter)
        //{

        //}
    }
}
