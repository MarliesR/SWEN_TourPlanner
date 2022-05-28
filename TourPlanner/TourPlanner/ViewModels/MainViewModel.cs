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
using System.Windows;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Tour> TourList { get; set; }
        public ObservableCollection<TourLog> LogList { get; set; }
        private Tour currentTour;
        private TourHandler handler = new TourHandler();

        public MainViewModel()
        {
            handler.InitialiseDB();
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
                        LoadLogsCurrentTour();
                        
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
            if (tourlist != null)
            {
                foreach (var tour in tourlist)
                {
                    TourList.Add(tour);
                }
            }
            
        }

        private void LoadLogsCurrentTour()
        {
            LogList.Clear();
            TourHandler handler = new TourHandler();
            List <TourLog> loglist = handler.ListAllLogsOfSingleTour(CurrentTour.Id);
            if (loglist != null)
            {
                foreach (var log in loglist)
                {
                    LogList.Add(log);
                }
            }
           

        }

        private RelayCommand showTourWindowCommand1;
        public ICommand showTourWindowCommand => showTourWindowCommand1 ??= new RelayCommand(showTourWindow);

        private void showTourWindow(object commandParameter)
        {
            AddTourView newTourWindow = new AddTourView();
            newTourWindow.Show();
        }

        private RelayCommand addLogPageCommand1;
        public ICommand addLogPageCommand => addLogPageCommand1 ??= new RelayCommand(addLogPage);

        private void addLogPage(object commandParameter)
        {
            if (currentTour != null)
            {
                AddLogView newLogWindow = new AddLogView(currentTour);
                newLogWindow.Show();
            }
            else
            {
                MessageBox.Show("Please choose a tour");
            }
        }

        private RelayCommand refreshToursCommand1;
        public ICommand refreshToursCommand => refreshToursCommand1 ??= new RelayCommand(refreshTours);

        private void refreshTours(object commandParameter)
        {
            LoadAllTours();
        }        //https://github.com/BernLeWal/fhtw-bif4-layeredarchitecture-cs 
        //viewmodelbase ist vorgegeben
        //MainViewModel muss im View auch hinterlegt werden 
        //xmlns:viewmodels="clr-namespace:TourPlanner.ViewModels" d:DataContext="{d:DesignInstance Type = viewmodels:MainViewModel}"
    }
}
