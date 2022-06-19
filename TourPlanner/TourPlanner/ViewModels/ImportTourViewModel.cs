using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.Library;

namespace TourPlanner.ViewModels
{
    internal class ImportTourViewModel : ViewModelBase
    {
        private ITourPlannerFactory tourPlannerFactory;
        private ImportTourLogic importTourObject = new ImportTourLogic();
        private Window currentWindow;
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string filePath = "";

        private RelayCommand importTourCommand;
        private RelayCommand clearInputCommand;

        public ICommand ImportTourCommand => importTourCommand ??= new RelayCommand(ImportTour);
        public ICommand ClearInputCommand => clearInputCommand ??= new RelayCommand(ClearInput);

        public ImportTourViewModel(Window window)
        {
            this.tourPlannerFactory = TourPlannerFactory.GetInstance();
            currentWindow = window;
        }

        public String FilePath
        {
            get => filePath;
            set
            {
                if (filePath != value)
                {
                    filePath = value;
                    RaisePropertyChangedEvent(nameof(FilePath));
                }
            }
        }

        private void ImportTour(object commandParameter)
        {
            if (importTourObject.DoesFileExist(filePath))
            {
                Tour importedTour = importTourObject.ReadFile(filePath);
                if (importedTour != null)
                {
                    tourPlannerFactory.ImportTour(importedTour);
                    currentWindow.DialogResult = true;
                    currentWindow.Close();
                    _logger.Info("Imported Tour.");
                }
                else
                {
                    MessageBox.Show("To Do"); // =================================================================
                    _logger.Warn("Importing tour failed.");
                }
            }
            else
            {
                MessageBox.Show("Path is incorrect or files does not exist");
                _logger.Warn("Importing tour failed because of path.");
            }

            filePath = string.Empty;
            FilePath = string.Empty;
        }

        
        private void ClearInput(object commandParameter)
        {
            FilePath = string.Empty;
            _logger.Info("Clear input from import tour window.");
        }
    }
}
