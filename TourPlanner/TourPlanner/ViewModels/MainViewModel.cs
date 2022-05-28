using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.ViewModels;
using TourPlanner.Views;
using TourPlanner.Library;
using TourPlanner.BL;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Tour> TourList { get; set; }
        public ObservableCollection<TourLog> LogList { get; set; }
        private Tour currentTour;

        public MainViewModel()
        {
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
                        //LoadLogs(_currentTour);
                    }
                }
            }
        }


        private RelayCommand editTourPageCommand1;
        public ICommand editTourPageCommand => editTourPageCommand1 ??= new RelayCommand(editTourPage);

        private void editTourPage(object commandParameter)
        {
            SelectedViewModel = new EditTourViewModel();

            // ID oder Tourdaten übergeben
        }

        private void LoadAllTours()
        {
            TourList.Clear();
            TourHandler handler = new TourHandler();
            List <Tour> tourlist = handler.ListAllTours();
         
            foreach (var tour in tourlist)
            {
                TourList.Add(tour);
            }
        }

        private RelayCommand showTourWindowCommand1;
        public ICommand showTourWindowCommand => showTourWindowCommand1 ??= new RelayCommand(showTourWindow);

        private void showTourWindow(object commandParameter)
        {
            AddTourView newTourWindow = new AddTourView();
            newTourWindow.Show();
        }
        //https://github.com/BernLeWal/fhtw-bif4-layeredarchitecture-cs 
        //viewmodelbase ist vorgegeben
        //MainViewModel muss im View auch hinterlegt werden 
        //xmlns:viewmodels="clr-namespace:TourPlanner.ViewModels" d:DataContext="{d:DesignInstance Type = viewmodels:MainViewModel}"
    }
}
