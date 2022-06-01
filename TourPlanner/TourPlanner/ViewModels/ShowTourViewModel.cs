using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.ViewModels;
using TourPlanner.Library;
using TourPlanner.BL;

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
        private string tourPopularity;
        private string tourChildFriendlyness;
        private string tourImagePath = @"C:\SWEN_semesterproject_images\default.jpg";


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
                tourPopularity = ComputeTourPopularity(tour.Id); 
            }
        }

        private string ComputeTourPopularity(int id)
        {
            TourHandler handler = new TourHandler();
            int popularity = handler.GetTourPopularity(id);
            if (popularity == 0)
            {
                return "No logs have been added";
            }
            string popularityText = "Place Nr." + popularity.ToString();
            return popularityText;
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

        public String TourPopularity
        {
            get => tourPopularity;
            set
            {
                if (tourPopularity != value)
                {
                    tourPopularity = value;
                    RaisePropertyChangedEvent(nameof(TourPopularity));
                }
            }
        }

        public String TourChildFriendlyness
        {
            get => tourChildFriendlyness;
            set
            {
                if (tourChildFriendlyness != value)
                {
                    tourChildFriendlyness = value;
                    RaisePropertyChangedEvent(nameof(TourChildFriendlyness));
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


       
    }
}
