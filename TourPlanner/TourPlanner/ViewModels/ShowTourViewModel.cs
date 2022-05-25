using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.ViewModels;

namespace TourPlanner.ViewModels
{
    public class ShowTourViewModel : ViewModelBase
    {

        private RelayCommand editTourCommand;
        public ICommand EditTourCommand => editTourCommand ??= new RelayCommand(EditTour);

        private void EditTour(object commandParameter)
        {

        }
    }
}
