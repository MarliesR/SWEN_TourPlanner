using System;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.ViewModels;

namespace TourPlanner.ViewModels
{
    public class AddTourViewModel : ViewModelBase
    {

        private RelayCommand saveTourCommand;
        public ICommand SaveTourCommand => saveTourCommand ??= new RelayCommand(SaveTour);

        private string tourName;
        private string tourStart;
        private string tourDestination;
        private string tourDescription;
        private string tourTransportType;



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

        //public ObservableCollection<MediaItem> Items {get; set;}

        public AddTourViewModel()
        {

        }



        private void SaveTour(object commandParameter)
        {
            TourHandler handler = new TourHandler();
            handler.AddTour(tourName, tourStart, tourDestination, tourTransportType, tourDescription);
            
        }

        private RelayCommand closeCommand1;
        public ICommand closeCommand => closeCommand1 ??= new RelayCommand(close);

        private void close(object commandParameter)
        {
        }
    }

}
