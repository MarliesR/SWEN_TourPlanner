using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Library;
using TourPlanner.BL;
using TourPlanner.Logger;
using log4net;

namespace TourPlanner.ViewModels
{
    public class AddLogViewModel : ViewModelBase
    {
        private ITourPlannerFactory tourPlannerFactory;
        private Window currentWindow;
        //private static readonly log4net.ILog _logger = LoggingHandler.GetLogger();
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string tourName;
        private string logDate;
        private string logTimeTotal = "hh:mm";
        private int logRating = 1;
        public ObservableCollection<int> RatingTypes { get; set; }
        private int logDifficulty = 1;
        public ObservableCollection<int> DifficultyTypes { get; set; }
        private string logComment;
        private int tourid;
        TimeSpan convertedTotalTime;

        private RelayCommand clearLogCommand;
        private RelayCommand saveLogCommand;
        public ICommand ClearLogCommand => clearLogCommand ??= new RelayCommand(ClearLog);
        public ICommand SaveLogCommand => saveLogCommand ??= new RelayCommand(SaveLog);


        public AddLogViewModel(Window window, Tour tour)
        {
            this.tourPlannerFactory = TourPlannerFactory.GetInstance();
            LogDate = GetCurrentTimestamp();
            currentWindow = window;
            tourName = tour.Name;
            tourid = tour.Id;
            RatingTypes = new ObservableCollection<int>();
            DifficultyTypes = new ObservableCollection<int>();
            initializeRatingTypes();
            initializeDifficultyTypes();

        }

        private void initializeRatingTypes()
        {
            RatingTypes.Add(1);
            RatingTypes.Add(2);
            RatingTypes.Add(3);
            RatingTypes.Add(4);
            RatingTypes.Add(5);
        }

        private void initializeDifficultyTypes()
        {
            DifficultyTypes.Add(1);
            DifficultyTypes.Add(2);
            DifficultyTypes.Add(3);
            DifficultyTypes.Add(4);
            DifficultyTypes.Add(5);
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
        public String LogDate
        {
            get => logDate;
            set
            {
                if (logDate != value)
                {
                    logDate = value;
                    RaisePropertyChangedEvent(nameof(LogDate));
                }
            }
        }

        public String LogTimeTotal
        {
            get => logTimeTotal;
            set
            {
                if (logTimeTotal != value)
                {
                    logTimeTotal = value;
                    RaisePropertyChangedEvent(nameof(LogTimeTotal));
                }
            }
        }

        public int LogRating
        {
            get => logRating;
            set
            {
                if (logRating != value)
                {
                    logRating = value;
                    RaisePropertyChangedEvent(nameof(LogRating));
                }
            }
        }

        public int LogDifficulty
        {
            get => logDifficulty;
            set
            {
                if (logDifficulty != value)
                {
                    logDifficulty = value;
                    RaisePropertyChangedEvent(nameof(LogDifficulty));
                }
            }
        }

        public String LogComment
        {
            get => logComment;
            set
            {
                if (logComment != value)
                {
                    logComment = value;
                    RaisePropertyChangedEvent(nameof(LogComment));
                }
            }
        }



        private void ClearLog(object commandParameter)
        {
            LogDate = GetCurrentTimestamp();
            LogComment = string.Empty;
            LogDifficulty = 1;
            LogRating = 1;
            LogTimeTotal = "hh:mm";
        }


        private void SaveLog(object commandParameter)
        {

           if (!ConvertTimeInput(logTimeTotal))
            {
                _logger.Info("Added new TourLog failed.");
                return;
            }
            TourLog log = new TourLog(tourid, logDate, logComment, logDifficulty, convertedTotalTime, logRating);
            tourPlannerFactory.AddLog(log);
            currentWindow.DialogResult = true;
            currentWindow.Close();

            _logger.Info("Added new TourLog.");
        }

        private string GetCurrentTimestamp()
        {
            DateTime today = DateTime.Now;
            return today.ToString(); 

        }

        private bool ConvertTimeInput(string logTimeTotal)
        {
            TimeSpan ts;
            try
            {
                 ts = TimeSpan.Parse(logTimeTotal);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }

            convertedTotalTime = ts;
            return true;

        }
    }
}
