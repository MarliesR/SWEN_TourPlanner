using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL;
using TourPlanner.Library;
using log4net;

namespace TourPlanner.ViewModels
{
    public class EditLogViewModel : ViewModelBase
    {
        private ITourPlannerFactory tourPlannerFactory;
        private Window currentWindow;
        //private static readonly log4net.ILog _logger = LoggingHandler.GetLogger();
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string tourName;
        private string logDate;
        private string logTimeTotal;
        private int logRating;
        public ObservableCollection<int> RatingTypes { get; set; }
        private int logDifficulty;
        public ObservableCollection<int> DifficultyTypes { get; set; }
        private string logComment;
        private string defaultTourname;
        private TourLog baseLog;
        TimeSpan convertedTotalTime;

        private RelayCommand saveLogCommand;
        private RelayCommand resetLogCommand;
        public ICommand SaveLogCommand => saveLogCommand ??= new RelayCommand(SaveEditedLog);
        public ICommand ResetLogCommand => resetLogCommand ??= new RelayCommand(ResetLog);




        public EditLogViewModel(Window window, TourLog log, string tourname)
        {
            this.tourPlannerFactory = TourPlannerFactory.GetInstance();
            currentWindow = window;
            defaultTourname = tourname;
            baseLog = log;
            RatingTypes = new ObservableCollection<int>();
            DifficultyTypes = new ObservableCollection<int>();
            initializeRatingTypes();
            initializeDifficultyTypes();
            ResetDefaultLogValues();
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

        private void ResetDefaultLogValues()
        {
            TourName = defaultTourname;
            LogDate = baseLog.DateTime;
            LogTimeTotal = baseLog.TotalTime.ToString();
            LogRating = baseLog.Rating;
            LogDifficulty = baseLog.Difficulty;
            LogComment = baseLog.Comment;

            _logger.Info("Restet TourLog data from edit.");
        }


        private void SaveEditedLog(object commandParameter)
        {
            if (!ConvertTimeInput(LogTimeTotal))
            {
                _logger.Info("Added new TourLog failed.");
                return;
            }
            TourLog modifiedLog = new TourLog(baseLog.TourId, LogDate, LogComment, LogDifficulty, convertedTotalTime, LogRating);
            modifiedLog.Id = baseLog.Id;
            this.tourPlannerFactory.ModifyLogEntry(modifiedLog);
            currentWindow.DialogResult = true;
            currentWindow.Close();

            _logger.Info("Edited TourLog data.");
        }

        
        private void ResetLog(object commandParameter)
        {
            ResetDefaultLogValues();
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
