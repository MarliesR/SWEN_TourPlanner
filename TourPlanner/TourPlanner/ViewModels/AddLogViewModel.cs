using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Library;

namespace TourPlanner.ViewModels
{
    public class AddLogViewModel : ViewModelBase
    {
        private Window currentWindow;
        private string tourName;

        public AddLogViewModel(Window window, Tour tour)
        {
            tourName = tour.Name;
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
    }
}
