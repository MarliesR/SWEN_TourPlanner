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

namespace TourPlanner.ViewModels
{
    public class AddLogViewModel : ViewModelBase
    {
        private Window currentWindow;
        private string tourName;
        private string logDate = "2022-01-01";
        private string logTimeTotal = "00:00";
        private int logRating = 1;
        public ObservableCollection<int> RatingTypes { get; set; }
        private int logDifficulty = 1;
        public ObservableCollection<int> DifficultyTypes { get; set; }
        private string logComment;
        private int tourid;

        private static readonly log4net.ILog _logger = LoggingHandler.GetLogger();

        public AddLogViewModel(Window window, Tour tour)
        {
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



        private RelayCommand clearLogCommand;
        public ICommand ClearLogCommand => clearLogCommand ??= new RelayCommand(ClearLog);

        private void ClearLog(object commandParameter)
        {
            LogDate = "2022-01-01";
            LogComment = string.Empty;
            LogDifficulty = 1;
            LogRating = 1;
            LogTimeTotal = "00:00";
        }

        private RelayCommand saveLogCommand;
        public ICommand SaveLogCommand => saveLogCommand ??= new RelayCommand(SaveLog);

        private void SaveLog(object commandParameter)
        {
            TourLog log = new TourLog(tourid, logDate, logComment, logDifficulty, logTimeTotal, logRating);
            TourHandler handler = new TourHandler();
            handler.AddLog(log);
            currentWindow.Close();

            _logger.Info("Added new TourLog.");
        }
    }
}
